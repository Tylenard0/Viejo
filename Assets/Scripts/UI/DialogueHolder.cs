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

    private void ActivateDialogue(Dialogue dialogue)
    {
            DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            dialogueManager.CallActivateDialogue(dialogue);
    }

    private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                ActivateDialogue(dialogue);
            }
        }

    public bool HandleRaycast(PlayerController callingController)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 newDestination = this.transform.position;
            newDestination = newDestination + (this.transform.forward * 2);
            callingController.GetComponent<Mover>().StartMoveAction(newDestination,  6);
            //ActivateDialogue(dialogue);
        }
        return true;
    }

    public CursorType GetCursorType()
    {
        return CursorType.Dialogue;
    }

}
}