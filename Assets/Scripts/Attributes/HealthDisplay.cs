using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {

        Health health;


        private void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            //  display  as %  ::pas oop0-9or0c-ertu0oiujkkuiuiokjjhgggjjjjkjkkkkjkjjjnnnnb
            //GetComponent<Text>().text = String.Format("{0:0.0}%", health.GetHealthPercentage());
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());

        }

    }
}