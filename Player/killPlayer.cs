using UnityEngine;

public class killPlayer : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            FindObjectOfType<GameManager>().ResetGame();
        }
    }
}
