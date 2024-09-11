using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TableManager))]
public class TableManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        TableManager tb = (TableManager)target;

        base.OnInspectorGUI();
    }

}
