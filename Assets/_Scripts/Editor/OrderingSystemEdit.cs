using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OrderingSystem))]

public class OrderingSystemEdit : Editor
{
    private string orderTexts = "";

    public override void OnInspectorGUI()
    {
        OrderingSystem order = (OrderingSystem)target;

        GUILayout.BeginVertical();

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();



        GUILayout.EndVertical();

        GUILayout.Space(10);

        GUILayout.BeginVertical(GUILayout.Width(200), GUILayout.Height(90));
        EditorGUILayout.TextArea(orderTexts, GUILayout.ExpandHeight(true));
        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        if (GUILayout.Button("ORDER", GUILayout.Height(30)))
        {
            order.PlaceOrder();
        }

        GUILayout.EndVertical();

        DrawDefaultInspector();
    }
}

