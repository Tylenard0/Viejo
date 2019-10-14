using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Control;

namespace  RPG.UI
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] string characterName = null;
        [SerializeField] float typingSpeed = .02f;
        public TextMeshProUGUI dialogueText;
        public string[] sentences;
        private int index;
        private Quest quest;
        private bool hasQuest = false;
        private AudioSource continueSound = null;
        private Animator uiAnimator = null;
        private QuestManager questManager = null;


       private void Awake() 
       {
            continueSound = GetComponentInChildren<AudioSource>();
            uiAnimator = GetComponentInChildren<Animator>();
            questManager = FindObjectOfType<QuestManager>();
       }

        void Start()
        {
         //   uiAnimator.SetBool("OpenDialogueBox", true);
         //   StartCoroutine(Type());
          //  ActivateDialogue();
        } 

        public void ActivateDialogue()
        {
            PlayerController newplayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            newplayerController.enabled = false;
            uiAnimator.SetBool("OpenDialogueBox", true);
            StartCoroutine(Type());
        }

        private void AddDialogueToManager(Dialogue dialogue)
        {
            characterName = dialogue.characterName;
            sentences = dialogue.sentences;
            if(dialogue.quest != null)
            {
                hasQuest = true;
                quest = dialogue.quest;
            }
            
        }

        public void CallActivateDialogue(Dialogue dialogue)
        {
            PlayerController newplayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            newplayerController.enabled = false;
            uiAnimator.SetBool("OpenDialogueBox", true);
            AddDialogueToManager(dialogue);
            StartCoroutine(Type());
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
            }else Debug.Log("no sound");

            if (index < sentences.Length - 1)
            {
                index++;
                dialogueText.text = "";
                StartCoroutine(Type());
            }
            else
            {
                EndDialogue();
            }
        }

        private void EndDialogue()
        {
            uiAnimator.SetBool("OpenDialogueBox", false);
            dialogueText.text = "";

            if(hasQuest)
            {
                questManager.DisplayQuest(quest);
                print("Has Quest " + quest.name + ".  All you have to is " +  quest.questDescription + " Rewards of " + quest.experienceReward + " experience, " + quest.goldReward + " gold!");
                if(quest.itemReward != null){print(" There is also a " + quest.itemReward.name + " in it for ya!");}
            }

            PlayerController newplayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            newplayerController.enabled = true;
        }
    }
}
