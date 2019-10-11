using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Weapon : MonoBehaviour
    {

        [SerializeField] UnityEvent onHit;

        public void OnHit()
        {
           // print("It's a hit, Johnny! " + gameObject.name + " has hit");
           onHit.Invoke();
        }
    }
}