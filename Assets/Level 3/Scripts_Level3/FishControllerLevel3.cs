using UnityEngine;

public class FishControllerLevel3 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float minDirectionTime = 2f;
    public float maxDirectionTime = 3f;
    
    // setting values
    private Transform[] fish;
    private float[] timers;
    private int[] directions; // 1 = right, -1 = left

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // makes it apply to all children
        fish = new Transform[transform.childCount];
        timers = new float[transform.childCount];
        directions = new int[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++)
        {
            fish[i] = transform.GetChild(i);
            directions[i] = Random.value > 0.5f ? 1 : -1; // picks starting direction
            timers[i] = Random.Range(minDirectionTime, maxDirectionTime); // randomizes times
            
            // makes sure fish are facing movement direction
            if (directions[i] == -1)
            {
                Vector3 scale = fish[i].localScale;
                scale.x *= -1;
                fish[i].localScale = scale;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < fish.Length; i++)
        {
            // move fish
            float step = directions[i] * moveSpeed * Time.deltaTime;
            fish[i].position += new Vector3(step, 0f, 0f);

            timers[i] -= Time.deltaTime;
            if (timers[i] <= 0f)
            {
                FlipFish(i); // flips fish when timer runs out
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D other) // runs is fish is hit
    {
        // check which fish left the boundary
        for (int i = 0; i < fish.Length; i++)
        {
            if (other.transform == fish[i]) // flips fish if it hits boundary
            {
                FlipFish(i);
            }
        }
    }
    
    void FlipFish(int i)
    {
        directions[i] *= -1;

        Vector3 scale = fish[i].localScale;
        scale.x *= -1;
        fish[i].localScale = scale;

        timers[i] = Random.Range(minDirectionTime, maxDirectionTime);
    }
}
