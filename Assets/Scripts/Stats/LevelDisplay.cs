﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats { 
public class LevelDisplay : MonoBehaviour
{
    BaseStats baseStats;


    private void Awake()
    {
            baseStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
    }

    private void Update()
    {
        GetComponent<Text>().text = String.Format("{0}", baseStats.GetLevel());
    }
}
}
