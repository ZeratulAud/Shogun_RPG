﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class Enemy : MonoBehaviour {

    [SerializeField] float maxHealthPoints = 100;

    float currentHealthPoints = 100f;

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / maxHealthPoints;
        }
    }
}
