using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour, ISaveable
    {
       public int hasBeenTriggered = 0;

        public object CaptureState()
        {
            //print ("Saving " + hasBeenTriggered.ToString());
           return hasBeenTriggered.ToString();
        }

        public void RestoreState(object state)
        {
            String hasBeenTriggeredState = (string)state;
           // print("Restore! " + (object)state);
            hasBeenTriggered =   System.Convert.ToInt32(hasBeenTriggeredState);
          // print(hasBeenTriggered);
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player") && (hasBeenTriggered == 0))
            {
                hasBeenTriggered = 1;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}