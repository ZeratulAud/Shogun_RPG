using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D cantWalkCursor = null;
    [SerializeField] Texture2D attackCursor = null;
    [SerializeField] Texture2D unknownCursor = null;
    [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);

    [SerializeField] const int walkableLayerNumber = 8;
    [SerializeField] const int enemyLayerNumber = 9;


    CameraRaycaster cameraRaycaster;

    // Use this for initialization
    void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyLayerChangeObservers += OnLayerChanged;
    }

    // Update is called once per frame
    void OnLayerChanged(int newLayer)
    {
        switch (newLayer)
        {
            case walkableLayerNumber:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
            case enemyLayerNumber:
                Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
                Debug.LogError("Showing Unknown Cursor");
                return;
        }
        //print(cameraRaycaster.layerHit);
    }


    /*{
        case Layer.NotWalkable:
            Cursor.SetCursor(cantWalkCursor, cursorHotspot, CursorMode.Auto);
            break;
        case Layer.Water:
            Cursor.SetCursor(cantWalkCursor, cursorHotspot, CursorMode.Auto);
            break;
        case Layer.Walkable:
            Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
            break;
        case Layer.Enemy:
            Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
            break;
        case Layer.RaycastEndStop:
            Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
            break;
        default:
            Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
            Debug.LogError("Showing Unknown Cursor");
            return;
    }
    //print(cameraRaycaster.layerHit);
}*/
}

// TODO think about de-register OnLayerChaanged