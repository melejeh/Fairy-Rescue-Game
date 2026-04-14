using UnityEngine;

public class HideUIAfterTime : MonoBehaviour
{
    [SerializeField] private GameObject objectToHide;
    [SerializeField] private float timeToHide = 8f;

    void Start()
    {
        // Schedule object to be hidden after a delay
        Invoke(nameof(HideObject), timeToHide);
    }

    void HideObject()
    {
        // Disable the target object if assigned
        if (objectToHide != null)
        {
            objectToHide.SetActive(false);
        }
    }
}