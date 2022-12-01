using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake()
    {
        //if there is no other instance make this one the instance
        if(instance == null)
        {
            instance = this;
        }
        else //if there is another destroy it
        {
            Destroy(gameObject);
            return;
        }
        //make the gamemanager consistent in any scene
        DontDestroyOnLoad(gameObject);
    }

    //reset
    public void ResetGame()
    {
        string scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }
}
