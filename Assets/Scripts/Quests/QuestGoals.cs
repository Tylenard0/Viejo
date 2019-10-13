using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoals
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;
    public string goalName;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }
}

public enum GoalType
{
    Kill,
    Collect

}
