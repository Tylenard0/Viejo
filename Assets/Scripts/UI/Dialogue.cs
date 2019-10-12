using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace  RPG.UI
{
    public class Dialogue : MonoBehaviour
    {
        [SerializeField] string characterName = null;
        [SerializeField] float typingSpeed = .02f;
        public TextMeshProUGUI dialogueText;
        public string[] sentences;
        private int index;
        private AudioSource continueSound = null;

       private void Awake() 
       {
         //  continueSound = GetComponent<AudioSource>();
       }

        void Start()
        {
            StartCoroutine(Type());
            continueSound = GetComponentInChildren<AudioSource>();
        } 

        IEnumerator Type()
        {
            dialogueText.text = characterName + ": ";

            foreach( char letter in sentences[index].ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        public void progressDialogue()
        {
    //        if (dialogueText.text == (characterName + ": " + sentences.Length)) {print("HIDE DIALOGUE BOX");}
            if(dialogueText.text == (characterName + ": " + sentences[index]))
            {
                NextSentence();
            }
        }

        private void NextSentence()
        {
            if(continueSound != null)
            {
            continueSound.Play();
            Debug.Log("sound");
            }else Debug.Log("no sound");

            if (index < sentences.Length - 1)
            {
                index++;
                dialogueText.text = "";
                StartCoroutine(Type());
            }
            else
            {
                dialogueText.text = "";
            }
        }

    }
}
