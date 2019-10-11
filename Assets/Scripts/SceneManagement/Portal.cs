using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using RPG.Control;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            PortalA, PortalB, PortalC, PortalD
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime =1;
        [SerializeField] float fadeInTime = 2;
        [SerializeField] float fadeWaitTime = 1;

        private void OnTriggerEnter(Collider other)
        {
           // print("Portal Triggered")
            if (other.CompareTag("Player"))
            {
                StartCoroutine(Transistion());
            }
        }


        private IEnumerator Transistion()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load is not set.");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            //Remove control
            PlayerController playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            playerController.enabled = false;

            yield return fader.FadeOut(fadeOutTime);

            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            PlayerController newplayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            newplayerController.enabled = false;

            savingWrapper.Load();

            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            savingWrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            fader.FadeIn(fadeInTime);

            //restore control
            newplayerController.enabled = true;
            
            // Destroy after loading new scene and setting loaded scene variables
            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach( Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination == destination)
                {
                    return portal;
                }
            }
            return null;
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
           player.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
