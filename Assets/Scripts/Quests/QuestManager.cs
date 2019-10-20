using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.UI
{
public class QuestManager : MonoBehaviour
{
        // Quest Menu UI Elements:
        [SerializeField] TextMeshProUGUI questNameUI;
        [SerializeField] TextMeshProUGUI questDescriptionUI;
        [SerializeField] TextMeshProUGUI questGoalsUI;
        [SerializeField] TextMeshProUGUI questEXPRewardUI;
        [SerializeField] TextMeshProUGUI questGoldRewardUI;
    
        public bool isQuestActive;
        public bool isQuestComplete = false;

        public string questName;
        public string questDescription;
        public QuestGoals goals;
        public float experienceReward;
        public float goldReward;
        public GameObject itemReward;


        private void AddQuestToManager(Quest quest)
        {
            questNameUI.text = quest.questName;
            questDescriptionUI.text = quest.questDescription;
            questGoalsUI.text = ("Goals: " + quest.goals.ToString());
            questEXPRewardUI.text = ("XP: " + quest.experienceReward.ToString());
            questGoldRewardUI.text = ("Gold: " + quest.goldReward.ToString());


        }

        public void DisplayQuest(Quest quest)
        {
            // PlayerController newplayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            // newplayerController.enabled = false;
            //   continueSound = GetComponentInChildren<AudioSource>();
            Animator uiAnimator = GetComponentInChildren<Animator>();
            if(uiAnimator != null) 
            {uiAnimator.SetBool("OpenUIBox", true);}
            else print("No uiAnimator found by Quest MGR");
            AddQuestToManager(quest);
        }

        public void AcceptQuest()
        {
            // Add Quest to Player 
            HideQuestMenuUI();
        }

        private void HideQuestMenuUI()
        {
            Animator uiAnimator = GetComponentInChildren<Animator>();
            if (uiAnimator != null) { uiAnimator.SetBool("OpenUIBox", false); }
        }

    }
}