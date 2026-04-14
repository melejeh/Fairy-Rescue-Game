using UnityEngine;
using TMPro;
using System.Collections;

public class NPCDialogue_Level0 : MonoBehaviour
{
    public TMP_Text bubbleText;

    public TMP_Text objectiveText;

    [TextArea] public string[] introLines;

    [TextArea] public string finalObjective;

    [SerializeField] private float lineDuration = 4f;

    [SerializeField] private float delayBetweenLines = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
