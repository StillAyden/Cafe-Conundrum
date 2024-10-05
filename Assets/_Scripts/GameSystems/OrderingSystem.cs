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
        ClearOrderDisplay();
    }

    #endregion

    #region Public Order Methods

    public void AddPizzaOrder()
    {
        orders.Add(Food.Pizza);
        UpdateOrderDisplay(Food.Pizza);
    }
    public void AddSandwichOrder()
    {
        orders.Add(Food.Sandwich);
        UpdateOrderDisplay(Food.Sandwich);
    }
    public void AddBreadStickOrder()
    {
        orders.Add(Food.BreadSticks);
        UpdateOrderDisplay(Food.BreadSticks);
    }
    public void AddBurittoOrder()
    {
        orders.Add(Food.Burrito);
        UpdateOrderDisplay(Food.Burrito);
    }
    public void AddMuffinOrder()
    {
        orders.Add(Food.Muffin);
        UpdateOrderDisplay(Food.Muffin);
    }
    public void AddCakeOrder()
    {
        orders.Add(Food.Cake);
        UpdateOrderDisplay(Food.Cake);
    }
    public void AddCookieOrder()
    {
        orders.Add(Food.Cookie);
        UpdateOrderDisplay(Food.Cookie);
    }

    public void CancelOrder()
    {
        orders.Clear();
        orderDisplay = "";
        ClearOrderDisplay();
    }

    public void PlaceOrder()
    {
        //Add order to Kitchen List
        kitchen.AddOrder(orders);
        orders.Clear();
        orderDisplay = "";
        ClearOrderDisplay();
    }

    #endregion

    #region Private Methods

    private void ClearOrderDisplay()
    {
        orderDisplay = "";

        // Update the UI Text if it's assigned
        if (orderTextDisplay != null)
        {
            orderTextDisplay.text = orderDisplay;
        }
    }

    private void UpdateOrderDisplay(Food food)
    {
         orderDisplay += food.ToString() + "\n";
        

        // Update the UI Text if it's assigned
        if (orderTextDisplay != null)
        {
            orderTextDisplay.text = orderDisplay;
        }
    }

    #endregion
}
