using UnityEngine;

public class Wings_Powerup_Behvaiour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Debug.Log("Fairy hit the wings power-up");

        FairyMovement_Level4V2 player4 = collision.GetComponentInParent<FairyMovement_Level4V2>();
        FairyControllerLevel5 player5 = collision.GetComponentInParent<FairyControllerLevel5>();
        FairyController_level6 player6 = collision.GetComponentInParent<FairyController_level6>();

        bool activated = false;

        if (player4 != null)
        {
            player4.ActivateWingPower();
            activated = true;
        }

        if (player5 != null)
        {
            player5.ActivateWingPower();
            activated = true;
        }

        if (player6 != null)
        {
            player6.ActivateWingPower();
            activated = true;
        }

        if (!activated)
        {
            Debug.LogWarning("No compatible player script found for wings power-up.");
            return;
        }

        Destroy(gameObject);
    }
}