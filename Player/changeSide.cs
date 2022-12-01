using UnityEngine;

public class changeSide : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        //change the scale to match which way youre going
        if(Input.GetKeyDown(KeyCode.A)) player.transform.localScale = new Vector3(-0.4f, 0.4f, 0.4f);
        if(Input.GetKeyDown(KeyCode.D)) player.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }
}
