using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("hi");
    }
}
