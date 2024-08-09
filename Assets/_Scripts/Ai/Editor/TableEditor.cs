using UnityEngine;
using UnityEditor;

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

        if (GUILayout.Button("FORCE Clear Table"))
        {
            table.ForceClearTable();
        }
        EditorGUILayout.EndHorizontal();

        base.OnInspectorGUI();
    }

}
