using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Attributes;
using RPG.Stats;
using System;

namespace RPG.UI{
public class PlayerStatsUI : MonoBehaviour
{

        Health health;
        [SerializeField] Health healthComponent = null;
        [SerializeField] Text levelText = null;
        [SerializeField] GameObject player = null;
        [SerializeField] Image healthBar = null;

        BaseStats baseStats = null;

        //[SerializeField] Canvas rootCanvas = null;
     //   BaseStats baseStats;

     private void Awake() 
    {
        baseStats = player.GetComponent<BaseStats>();
    }

        // Update is called once per frame
        void Update()
    {
        UpdateHealthBar();
        UpdateManaBar();
        UpdateLevelUI();
    }

        private void UpdateManaBar()
        {
            
        }

        private void UpdateHealthBar()
        {
            healthBar.fillAmount = healthComponent.GetHealthFraction();
        }

        private void UpdateLevelUI()
        {
           if(baseStats != null && levelText != null)
           {
            levelText.text = baseStats.GetLevel().ToString();
            //String.Format("{0}", baseStats.GetLevel());
           }
        }
     }
    
}