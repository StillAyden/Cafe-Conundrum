using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    #region Variables
    public List<Table> tables = new List<Table>(); // List of all tables in the game

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

    #endregion
}
