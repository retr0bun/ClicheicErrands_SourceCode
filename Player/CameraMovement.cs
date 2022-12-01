using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform camPos;
    public Transform offset;

    void Update()
    {
        //a vector that gets the empty cameraPosition object and keeps y at a desired place rather than follow the player
        Vector3 position = new Vector3(camPos.transform.position.x, offset.transform.position.y ,camPos.transform.position.z);
        //assign the camera position to the vector
        this.gameObject.transform.position = position;
    }
}