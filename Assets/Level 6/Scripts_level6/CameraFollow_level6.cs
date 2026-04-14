using UnityEngine;

public class CameraFollow_level6 : MonoBehaviour
{
    //Field to allow for the camera to follow the player smoothly 
    [SerializeField] private float smoothSpeed = 5f;
    
    //Fields to set the max movement the camera can do in the Y direction - prevents the player from setting outside the scene
    [SerializeField] private float minY = 11.35f;
    [SerializeField] private float maxY = 26.7f;

    //Fields to set the max movement the camera can do in the X direction
    [SerializeField] private float minX = 10.47f;
    [SerializeField] private float maxX = 62.76f;

    private Transform player;
    
    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
    }

    void LateUpdate()
    {
        if (player == null) return;
        
        //I used an orthographic camera rather than perspective so I could properly set the bounds of its movement - could adapt this, but it's so much harder using perspective
        float camHalfHeight = Camera.main.orthographicSize;

        //set/clamp the position of the camera based on the bounds and player position
        float targetX = Mathf.Clamp(player.position.x, minX + camHalfHeight, maxX - camHalfHeight);
        float targetY = Mathf.Clamp(player.position.y, minY + camHalfHeight, maxY - camHalfHeight);

        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
