﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent = null;
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Canvas rootCanvas = null;

        void Update()
        {
            //  if (healthComponent.IsDead() || (Mathf.Approximately(healthComponent.GetHealthFraction(), 1)))
            if ((Mathf.Approximately(healthComponent.GetHealthFraction(), 0))
            || (Mathf.Approximately(healthComponent.GetHealthFraction(), 1)))
            {
                rootCanvas.enabled = false;
                return;
            }

            rootCanvas.enabled = true;
            foreground.localScale = new Vector3(healthComponent.GetHealthFraction(), 1, 1);
        }
    }
}