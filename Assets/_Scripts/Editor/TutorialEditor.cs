using Codice.Client.BaseCommands.Merge.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(TutorialScript_SO))]
public class TutorialEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TutorialScript_SO script = (TutorialScript_SO)target;

        int newSize = EditorGUILayout.IntField("Dialogue Lines", script.node.Length);
        if (newSize != script.node.Length && newSize > 0)
        {
            // Resize the array if the new size is different
            System.Array.Resize(ref script.node, newSize);
        }

        for (int i = 0; i < script.node.Length; i++)
        {
            EditorGUILayout.Space(10); 
            EditorGUILayout.LabelField($"Item {i + 1}", EditorStyles.boldLabel);

            // Display the item name field
            script.node[i].type = (TutorialNodeType)EditorGUILayout.EnumPopup("Type", script.node[i].type);

            // Dynamically show fields based on the selected enum value
            switch (script.node[i].type)
            {
                case TutorialNodeType.Dialogue:
                    script.node[i].actorName = EditorGUILayout.TextField("Actor Name", script.node[i].actorName);
                    script.node[i].actorDialogue = EditorGUILayout.TextField("Actor Dialogue", script.node[i].actorDialogue);
                    break;

                case TutorialNodeType.TutorialPanel:
                    script.node[i].tutorialImage = (Sprite)EditorGUILayout.ObjectField("Tutorial Image", script.node[i].tutorialImage, typeof(Sprite), false);
                    break;

                case TutorialNodeType.Event:
                    script.node[i].taskText = EditorGUILayout.TextField("Task Text", script.node[i].taskText);
                    script.node[i].hasEventTriggererd = EditorGUILayout.Toggle("Has Triggered", script.node[i].hasEventTriggererd);
                    break;
            }
        }

        // Apply changes to the serialized object
        if (GUI.changed)
        {
            // Mark the object as dirty to ensure changes are saved
            EditorUtility.SetDirty(script);
        }
    }
}
