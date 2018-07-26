using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;

 class Enemy : MonoBehaviour {

    [SerializeField] float maxHealthPoints = 100;
    [SerializeField] float attackRadius = 4f;

    float currentHealthPoints = 100f;
    GameObject player = null;
    AICharacterControl aiCharacterControl = null;

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacterControl = GetComponent<AICharacterControl>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= attackRadius)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else
        {
            aiCharacterControl.SetTarget(transform);
        }
    }
}
