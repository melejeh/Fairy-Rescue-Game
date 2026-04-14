using UnityEngine;

public class GroundfishControllerLevel3 : MonoBehaviour // moves fish up and down
{
    [Header("Movement Settings")]
    public float moveDistance = 1f;   // how far up it goes
    public float moveSpeed = 0.5f;      // how fast it moves

    private Vector3 startPosition;
    private Vector3 topPosition;
    private bool movingUp = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position; // tracks start poition
        topPosition = startPosition + new Vector3(0f, moveDistance, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, topPosition, moveSpeed * Time.deltaTime);

            if (transform.position == topPosition)
                movingUp = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

            if (transform.position == startPosition)
                movingUp = true;
        }
    }
}
