using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelKeyPickup : MonoBehaviour
{
    public string keyID = "Key1";
    public string returnSceneName = "MainHubV2";

    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (collected) return;

        collected = true;

        Debug.Log("Player touched key");
        GameManager.instance.CollectKey(keyID);
        Debug.Log("Collected level key: " + keyID);
        Debug.Log("About to load scene: " + returnSceneName);

        SceneManager.LoadScene(returnSceneName);
    }
}