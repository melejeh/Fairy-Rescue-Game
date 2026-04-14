using UnityEngine;

public class Heart_Powerup_Behaviour : MonoBehaviour

{
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Fairy hit the wand power-up");
            
            // FairyController_Level0 is reference to the fairy controller script (whatever one you may be using for your given level/scene
            FairyMovementL2 player2 = collision.gameObject.GetComponent<FairyMovementL2>();

            if (player2 != null)
            {
                player2.ActivateHeartPower(); // call a function on the player
            }

            Destroy(gameObject);
        }


    }
}
