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

        if (GUILayout.Button("CHIPS", GUILayout.Height(30)))
        {
            order.AddChipsOrder();
            orderTexts += "Chips\n";
        }

        if (GUILayout.Button("BURGER", GUILayout.Height(30)))
        {
            order.AddBurgerOrder();
            orderTexts += "Burger\n";
        }

        if (GUILayout.Button("PIZZA", GUILayout.Height(30)))
        {
            order.AddPizzaOrder();
            orderTexts += "Pizza\n";
        }

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

