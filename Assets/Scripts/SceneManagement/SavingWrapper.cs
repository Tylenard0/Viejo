using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";

        [SerializeField] float fadeInTime = 1f;

        private void Awake()
        {
           // StartCoroutine(LoadLastScene());
        }

        private IEnumerator LoadLastScene()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediately();
            yield return fader.FadeIn(fadeInTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
            }

            if (Input.GetKeyDown(KeyCode.Q) ||  Input.GetKeyDown(KeyCode.Escape))
            {
                ExitGame();
            }
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSaveFile);
        }

        public void ExitGame()
        {
           {
            #if UNITY_EDITOR
                            // Application.Quit() does not work in the editor so
                            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                            UnityEditor.EditorApplication.isPlaying = false;
            #else
                            Application.Quit();
            #endif
         }
        }
        
    }
}