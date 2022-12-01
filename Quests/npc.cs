using UnityEngine;

public class npc : MonoBehaviour
{
    //player 
    public Transform player;
    public Transform radius;
    public onPlayerScript instance;

    public float interactRadius;

    void Update()
    {
        float distance = Vector3.Distance(player.position, radius.position);
        if(distance <= interactRadius && Input.GetKeyDown(KeyCode.E))
        {
            if(gameObject.CompareTag("doubleJ"))
            {
                instance.currentQuest = instance.doubleJump;
            }
            FindObjectOfType<DialogueTrigger>().TriggerDialogue();
        }
    }

    //draw radius on screen
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(radius.position, interactRadius);
    }
}
