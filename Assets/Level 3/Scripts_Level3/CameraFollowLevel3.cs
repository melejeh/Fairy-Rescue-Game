using UnityEngine;
public class CameraFollowLevel3 : MonoBehaviour
{
    [SerializeField] private float smoothSpeed = 5f; // How smooth the camera follows



    [Header("Bounds")]

    [SerializeField] private float minX = -40; // Left limit

    [SerializeField] private float maxX = 147; // Right limit
    


    private Transform player;



    void Start()

    {

        // Try to find player automatically at start

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");



        if (playerObj != null)

        {

            player = playerObj.transform;

        }

    }



    // This lets the spawner manually assign the player (more reliable)

    public void SetTarget(Transform target)

    {

        player = target;

    }



    void LateUpdate()

    {

        // If player doesn't exist yet, do nothing

        if (player == null) return;



        // Clamp player position within camera bounds

        float targetX = Mathf.Clamp(player.position.x, minX, maxX); // only X 
        



        // Create the final camera position

        Vector3 targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);



        // Smoothly move camera toward target

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}