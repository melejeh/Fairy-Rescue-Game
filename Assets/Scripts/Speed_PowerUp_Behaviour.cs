using UnityEngine;

public class Speed_PowerUp_Behaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("Fairy hit the speed power-up");

        FairyControllerLevel5 player5 = collision.GetComponentInParent<FairyControllerLevel5>();
        FairyController_level6 player6 = collision.GetComponentInParent<FairyController_level6>();

        bool activated = false;

        if (player5 != null)
        {
            player5.ActivateSpeedPower();
            activated = true;
        }

        if (player6 != null)
        {
            player6.ActivateSpeedPower();
            activated = true;
        }

        if (!activated)
        {
            Debug.LogWarning("No compatible player script found for speed power-up.");
            return;
        }

        Destroy(gameObject);
    }
}