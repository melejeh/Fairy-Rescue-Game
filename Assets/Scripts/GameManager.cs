using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static event Action OnProgressChanged;

    public List<string> keysCollected = new List<string>();
    public List<string> keysDeposited = new List<string>();

    [Header("Lives")]
    public float startingLives = 5f;
    public float playerLives;

    public int coinsCollected = 0;
    public int coinsPerLife = 10;

    public string selectedFairy = "FairyA";

    [Header("Hub Progress")]
    public bool hasTalkedToNPC = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            playerLives = startingLives;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void MarkNPCTutorialComplete()
    {
        if (!hasTalkedToNPC)
        {
            hasTalkedToNPC = true;
            NotifyProgressChanged();
            Debug.Log("NPC tutorial complete. Portal 1 objective unlocked.");
        }
    }

    public void CollectKey(string keyID)
    {
        if (!keysCollected.Contains(keyID) && !keysDeposited.Contains(keyID))
        {
            keysCollected.Add(keyID);
            Debug.Log("Key collected: " + keyID);
            NotifyProgressChanged();
        }
    }

    public void DepositKey(string keyID)
    {
        if (keysCollected.Contains(keyID))
        {
            keysCollected.Remove(keyID);
            keysDeposited.Add(keyID);

            Debug.Log("Key deposited: " + keyID);
            NotifyProgressChanged();
        }
    }

    public int KeysDepositedCount()
    {
        return keysDeposited.Count;
    }

    public int GetUnlockedPortalCount()
    {
        int deposited = KeysDepositedCount();

        if (deposited >= 4) return 3; // portal 3 unlocked
        if (deposited >= 2) return 2; // portal 2 unlocked
        return 1;                     // portal 1 available in hub progression
    }

    public string GetHubObjectiveText()
    {
        int deposited = KeysDepositedCount();
        bool holdingKey = keysCollected.Count > 0;

        if (!hasTalkedToNPC)
            return "Welcome to Fairy Rescue! A scary wizard has taken over the land. Go talk to Gertrude the bear (press SPACE) to begin your mission.";
        if (holdingKey)
            return "Deposit the key in the chest.";

        switch (deposited)
        {
            case 0:
                return "To begin your mission, go to Portal 1.";
            case 1:
                return "Congrats on your first mission. But i think there's another key in Portal 1. Go back to it.";
            case 2:
                return "The tropical island needs your help! There's another key in Portal 2.";
            case 3:
                return "I think theres one more key on the beach! Go back into portal 2.";
            case 4:
                return "You saved the Enchanted Forest and the Tropical island. Go into Portal 3 to defeat the wizard.";
            default:
                return "All hub objectives complete.";
        }
    }

    public bool CanUsePortal(int portalNumber)
    {
        if (!hasTalkedToNPC)
            return false;

        int deposited = KeysDepositedCount();

        if (deposited == 0 && portalNumber == 1) return true; // Level 1
        if (deposited == 1 && portalNumber == 1) return true; // Level 2
        if (deposited == 2 && portalNumber == 2) return true; // Level 3
        if (deposited == 3 && portalNumber == 2) return true; // Level 4
        if (deposited == 4 && portalNumber == 3) return true; // Level 5

        return false;
    }

    public string GetNextSceneForPortal(int portalNumber)
    {
        int deposited = KeysDepositedCount();

        //this line decides whats first
        if (deposited == 0 && portalNumber == 1) return "Level1";
        if (deposited == 1 && portalNumber == 1) return "Level2";
        if (deposited == 2 && portalNumber == 2) return "Level3";
        if (deposited == 3 && portalNumber == 2) return "Level4";
        if (deposited == 4 && portalNumber == 3) return "Level5";

        return "";
    }

    public string GetGameOverScene()
    {
        return "GameOver";
    }

    public void LoseLife(float amount = 0.5f)
    {
        playerLives -= amount;

        if (playerLives < 0f)
            playerLives = 0f;

        LivesUI.instance?.UpdateLives(playerLives);

        Debug.Log("Player lost life! Lives remaining: " + playerLives);

        if (playerLives <= 0f)
        {
            Debug.Log("Player has died!");
            Time.timeScale = 1f;

            string scene = GetGameOverScene();

            if (!string.IsNullOrEmpty(scene))
            {
                SceneManager.LoadScene(scene);
            }
        }
    }

    public void GainLife(float amount = 1f)
    {
        playerLives += amount;
        LivesUI.instance?.UpdateLives(playerLives);

        Debug.Log("Player gained life! Lives: " + playerLives);
    }

    public void CollectCoin()
    {
        coinsCollected++;
        CoinsUI.instance?.UpdateCoins(coinsCollected);

        if (coinsCollected >= coinsPerLife)
        {
            coinsCollected = 0;
            CoinsUI.instance?.UpdateCoins(coinsCollected);
            GainLife(1f);
            Debug.Log("10 coins collected! Extra life granted.");
        }
    }

    private void NotifyProgressChanged()
    {
        LevelUI.instance?.UpdateLevelUI();
        OnProgressChanged?.Invoke();
    }
}