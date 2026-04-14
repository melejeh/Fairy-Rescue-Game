using TMPro;
using UnityEngine;

public class NPCDialogue_level6 : MonoBehaviour
{
    public TMP_Text bubbleText;
    public TMP_Text objectiveText;
    
    [TextArea] public string[] introLines;

    [TextArea] public string finalObjective;

    [SerializeField] private float lineDuration = 4f;

    [SerializeField] private float delayBetweenLines = 0.5f;
}
