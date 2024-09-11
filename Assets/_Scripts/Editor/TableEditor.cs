using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Table))]

public class TableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Table table = (Table)target;

        if (GUILayout.Button("Update ChairList"))
        {
            table.InitializeChairs();
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Clear Table"))
        {
            table.ClearTable();
        }

        if (GUILayout.Button("Take Order"))
        {
            table.IsOrderTaken = true;
        }
        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }

}
