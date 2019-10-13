using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (fileName = "Dialogue", menuName = "Make New Dialogue", order = 0) ]
public class Dialogue : ScriptableObject
{
  public string characterName = null;
    public string[] sentences;
    private int index;
    private bool hasSpoken = false;
    private bool hasQuest = false;
    public Quest quest;

    public string GetCharacterName (){return characterName;}
}
