using UnityEngine;

public class questItem : MonoBehaviour
{
    public Transform player;
    public Transform itemPosition;

    public float pickupRadius;

    public QuestManager instance;

    void Update()
    {
        float itemDistance = Vector3.Distance(player.position, itemPosition.position);
        if(itemDistance <= pickupRadius && instance.questStarted == true && gameObject.CompareTag("doubleJItem"))
        {
            FindObjectOfType<AudioController>().Play("pick up");
            FindObjectOfType<QuestManager>().ItemPickup();
            Destroy(gameObject);
        }
        if(itemDistance <= pickupRadius && instance.questStarted == true && gameObject.CompareTag("dashItem"))
        {
            FindObjectOfType<AudioController>().Play("pick up");
            FindObjectOfType<QuestManager>().ItemPickup();
            Destroy(gameObject);
        }
    }

    //draw radius on screen
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(itemPosition.position, pickupRadius);
    }
}
