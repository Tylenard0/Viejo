using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement{

public class SceneLoader : MonoBehaviour
{
    [SerializeField] const string OpeningScenename = "Sandbox";
    [SerializeField] const string defaultSaveFile = "save";
    SavingWrapper savingWrapper = null;
    SavingSystem savingSystem = null;
    

    // Start is called before the first frame update
    void Start()
    {
            //   savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper = GetComponent<SavingWrapper>();
            savingSystem = FindObjectOfType<SavingSystem>();
            if(savingWrapper = null) {Debug.LogError("No Saving Wrapper");}
    }

    public void LoadSavedGame()
    {
        if (savingSystem != null)
        {           
           StartCoroutine(savingSystem.LoadLastScene(defaultSaveFile));
       }
            else Debug.LogError("No saving system found.");

    
    }

    public void LoadNewGame()
    {
            SceneManager.LoadSceneAsync(OpeningScenename);
    }

    public void ExitGame()
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