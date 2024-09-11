using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    #region Variables
    public List<Table> tables = new List<Table>(); // List of all tables in the game

    [SerializeField][Range(0f,10f)] private float SpriteDistance = 2;

    #endregion

    #region Unity Methods

    private void Start()
    {
        AddAllTablesToList();
    }

    private void OnValidate()
    {
        foreach (Table table in tables)
        {
            table.SetSpriteDistance(SpriteDistance);
        }
    }

    #endregion

    #region Functions

    public Table FindAvailableTable() // Function to find an available table
    {
        foreach (Table table in tables)
        {
            if (!table.IsFull())
            {
                return table;
            }
        }
        return null; // No available table found
    }

    private void AddAllTablesToList()
    {
        // Find all GameObjects with the tag "Table"
        GameObject[] tableObjects = GameObject.FindGameObjectsWithTag("Table");

        // Loop through each GameObject
        foreach (GameObject tableObject in tableObjects)
        {
            // Check if the GameObject is on the "Table" layer
            if (tableObject.layer == LayerMask.NameToLayer("Table"))
            {
                // Get the Table component and add it to the list
                Table tableComponent = tableObject.GetComponent<Table>();
                if (tableComponent != null)
                {
                    tables.Add(tableComponent);
                }
            }
        }
    }

    #endregion
}
