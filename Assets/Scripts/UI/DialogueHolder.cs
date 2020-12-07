using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.Movement;

namespace RPG.UI{
public class DialogueHolder : MonoBehaviour, IRaycastable
{

    [SerializeField] Dialogue dialogue;
    [SerializeField] bool remotelyTriggeredDialogue = false;
    [SerializeField] bool questAccepted = false;

    private void ActivateDialogue(Dialogue dialogue)
    {
            if(questAccepted == false)
            {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.CallActivateDialogue(dialogue);
            questAccepted = true;
            }
    }

    private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && remotelyTriggeredDialogue == true)
            {
                ActivateDialogue(dialogue);
            }
        }

    public bool HandleRaycast(PlayerController callingController)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 newDestination = this.transform.position;
            newDestination = newDestination + (Vector3.forward * -2f);
            callingController.GetComponent<Mover>().StartMoveAction(newDestination,  2);
            ActivateDialogue(dialogue);
            
        }
        return true;
    }

    public CursorType GetCursorType()
    {
        return CursorType.Dialogue;
    }

}
}