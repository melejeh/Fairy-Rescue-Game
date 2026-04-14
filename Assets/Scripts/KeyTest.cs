using UnityEngine;

//this code is to debug why the key isn't working 
public class KeyTest : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered by: " + other.name);
    }
}