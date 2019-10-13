using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Make New Quest", order = 0)]
public class Quest : ScriptableObject
{
    public bool isQuestActive;
    public bool isQuestComplete = false;

    public string questName;
    public string questDescription;
    public QuestGoals goals;
    public float experienceReward;
    public float goldReward;
    public GameObject itemReward;

}
