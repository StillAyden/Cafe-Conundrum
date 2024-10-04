using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class TutorialScript_SO : ScriptableObject
{
    public TutorialNode[] node;
}

public struct TutorialNode
{
    public TutorialNodeType type;
    public string actorName;
    [TextArea] public string actorDialogue;

    public Image tutorialImage;

    public bool hasEventTriggererd;
    public string taskText;
}

public enum TutorialNodeType
{
    None = -1,
    Dialogue,
    TutorialPanel,
    Event
}
