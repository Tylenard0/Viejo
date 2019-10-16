using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

public class QuestLog : MonoBehaviour, ISaveable
{
    List<Quest> questList = new List<Quest>();

    private void Awake() 
    {
    // questLog 

    }

    public void AddQuestToLog(Quest quest)
    {
        questList.Add(quest);
        print("Adding quest " + quest.name + " to Quest Log");
    }

    public object CaptureState()
    {
      foreach (Quest quest in questList)
      {
          print(quest.name);
          return quest.name;
      }
        return true;
    }

    public void RestoreState(object state)
    {
        throw new System.NotImplementedException();
    }
}
