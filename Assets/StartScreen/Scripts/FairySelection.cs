using UnityEngine;
using UnityEngine.InputSystem;

public class FairySelection : MonoBehaviour
{
    Animator anim;
    Camera mainCamera;
    
    [SerializeField] private string fairyName;  // set in the Inspector for each
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
        
        if (mainCamera == null)
        {
            enabled = false;  // disables this script if no camera is found
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // checks mouse position when click happens
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            // if hit the object
            if (hit && hit.gameObject == gameObject)
            {
                anim.SetTrigger("Selected"); // runs animation
                GameManager.instance.selectedFairy = fairyName; // sets static field to specific name
            }
            
        }
    }
}
