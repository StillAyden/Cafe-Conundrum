using System.Collections.Generic;
using UnityEngine.UI;

public class OrderingSystem : Interactable
{
    #region Variables
    //Classes
    private Kitchen kitchen;

    //Vars
    private List<Food> orders = new List<Food>();

    // String to represent the order on the POS machine
    private string orderDisplay = "";

    // Reference to UI Text to display the order on screen (if needed)
    public Text orderTextDisplay;

    #endregion

    #region Unity Methods

    void Start()
    {
        kitchen = FindObjectOfType<Kitchen>();
        UpdateOrderDisplay();
    }

    #endregion

    #region Public Methods

    public void AddOrder(Food food)
    {
        orders.Add(food);
        UpdateOrderDisplay();
    }

    public void CancelOrder()
    {
        orders.Clear();
        orderDisplay = "";
        UpdateOrderDisplay();
    }

    public void PlaceOrder()
    {
        //Add order to Kitchen List
        kitchen.AddOrder(orders);
        orders.Clear();
        orderDisplay = "";
        UpdateOrderDisplay();
    }

    #endregion

    #region Private Methods

    private void UpdateOrderDisplay()
    {

        // Loop through each order and format it into the orderDisplay string
        foreach (Food order in orders)
        {
            orderDisplay += order.ToString() + "\n"; 
        }

        // Update the UI Text if it's assigned
        if (orderTextDisplay != null)
        {
            orderTextDisplay.text = orderDisplay;
        }
    }

    #endregion
}
