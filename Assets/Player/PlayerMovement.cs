using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
  


    [SerializeField] float StopRadius = 0.2f;
    [SerializeField] float RangedAttackRadius = 5.0f;
    [SerializeField] GameObject gimbal;
    //[SerializeField] GameObject player;
    ThirdPersonCharacter Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination;
    Vector3 clickPoint;
    bool isInDirectMode = false; //TODO consider static?

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        Character = GetComponent<ThirdPersonCharacter>();
        currentDestination = transform.position;
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        if (Input.GetMouseButton(2))
        {
            gimbal.transform.Rotate(0, 2*Input.GetAxis("Mouse X"), 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            gimbal.transform.Rotate(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            gimbal.transform.Rotate(0, 1, 0);
        }
        if (Input.GetKeyDown(KeyCode.G)) //G for gamepad toggle
        {
            isInDirectMode = !isInDirectMode; //toggleMode
            currentDestination = transform.position; // clear the click target
        }

        if (isInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            ProcessMouseMovement();
        }
    }

    private void ProcessDirectMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 CamForward = Vector3.Scale(gimbal.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 Move = v * CamForward + h * gimbal.transform.right;

        Character.Move(Move, false, false);
    }

    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            clickPoint = cameraRaycaster.hit.point;

            switch (cameraRaycaster.currentLayerHit)
            {
                case Layer.Walkable:
                    currentDestination = ShortDestination(clickPoint, StopRadius);
                    break;
                case Layer.Enemy:
                    currentDestination = ShortDestination(clickPoint, RangedAttackRadius);
                    break;
                case Layer.RaycastEndStop:
                    break;
                default:
                    print("SHOULD NOT BE HERE");
                    return;
            }
        }
        WalkToDestination();
    }

    private void WalkToDestination()
    {
        var playerToClickPoint = currentDestination - transform.position;
        if (playerToClickPoint.magnitude > StopRadius)
        {
            Character.Move(playerToClickPoint, false, false);
        }
        else
        {
            Character.Move(Vector3.zero, false, false);
        }
    }

    Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, currentDestination);
        Gizmos.DrawSphere(currentDestination, 0.1f);
        Gizmos.DrawSphere(clickPoint, 0.15f);

        // draw attack sphere
        Gizmos.color = new Color(255f, 0f, 0f);
        Gizmos.DrawWireSphere(transform.position, RangedAttackRadius);
    }
}

