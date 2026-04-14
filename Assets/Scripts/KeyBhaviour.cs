using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyBehaviour : MonoBehaviour
{
    public string keyID;
    public string nextSceneName;
    public AudioClip keyCollectSound;

    [SerializeField] private float sceneLoadDelay = 0.5f;

    private bool collected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (collected) return;

        collected = true;

        GameManager.instance.CollectKey(keyID);
        Debug.Log("Key " + keyID + " collected!");

        if (keyCollectSound != null)
        {
            AudioSource.PlayClipAtPoint(keyCollectSound, transform.position);
        }

        gameObject.SetActive(false);

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Invoke(nameof(LoadNextScene), sceneLoadDelay);
        }
    }

    private void LoadNextScene()
    {
        Debug.Log("Loading next scene: " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}