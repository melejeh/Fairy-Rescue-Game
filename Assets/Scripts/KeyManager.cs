using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public GameObject[] keys = new GameObject[4];

    private int currentKeyIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i] != null)
            {
                keys[i].SetActive(false);
            }
        }

        ShowNextKey();
    }

    public void ShowNextKey()
    {
        if (currentKeyIndex < keys.Length)
        {
            if (keys[currentKeyIndex] != null)
            {
                keys[currentKeyIndex].SetActive(true);
                currentKeyIndex++;
                Debug.Log("Key " + currentKeyIndex + " is now visible.");
            }
        }
        else
        {
            Debug.Log("All 4 keys collected! Quest Complete.");
        }
    }

}
