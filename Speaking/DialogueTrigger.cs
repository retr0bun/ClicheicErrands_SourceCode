using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogues")]
    //this stuff was planned for something else i did not have the time for
    //hence the names
    public Dialogue dialogueJump;
    public Dialogue dialogueDash;
    public Dialogue dialogueStrawberries;

    public void TriggerDialogue()
    {
        //check the tag of the npc and play the right dialogue
        if(gameObject.CompareTag("doubleJ"));
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueJump);
        }
        if(gameObject.CompareTag("dash"))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueDash);
        }
        if(gameObject.CompareTag("hp+"))
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogueStrawberries);
        }
    }
    
}
