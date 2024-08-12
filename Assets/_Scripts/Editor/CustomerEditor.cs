using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Customer))]

public class CustomerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Customer cus = (Customer)target;

        if (GUILayout.Button("GetOrder", GUILayout.Height(30)))
        {
            cus.HasGottenFood = true;
        }

        DrawDefaultInspector();
    }
}
