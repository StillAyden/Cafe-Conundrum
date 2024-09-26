using UnityEngine;

[CreateAssetMenu]
public class TutorialScript_SO : ScriptableObject
{
    public TutorialNode[] tutorialNodes;
}

public struct TutorialNode
{
    public TutorialNodeType type;
    public string actorName;
    [TextArea] public string actorDialogue;
}

public enum TutorialNodeType
{
    Dialogue,
    TutorialPanel
}
