using System.Collections.Generic;
using UnityEngine.UI;

public class OrderingSystem : Interactable
{
    #region Variables
    //Classes
    private Kitchen kitchen;

    //Vars
    public List<Order> orders = new List<Order>();

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

    public void AddBurgerOrder()
    {
        Order newOrder = new Order();
        newOrder.SetFoodType(Food.Burger);
        orders.Add(newOrder);
        UpdateOrderDisplay();
    }

    public void AddPizzaOrder()
    {
        Order newOrder = new Order();
        newOrder.SetFoodType(Food.Pizza);
        orders.Add(newOrder);
        UpdateOrderDisplay();
    }

    public void AddChipsOrder()
    {
        Order newOrder = new Order();
        newOrder.SetFoodType(Food.Chips);
        orders.Add(newOrder);
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
        // Reset the order display string
        orderDisplay = "";

        // Loop through each order and format it into the orderDisplay string
        foreach (Order order in orders)
        {
            switch (order.GetFoodType())
            {
                case Food.Burger:
                    orderDisplay += "Burger\n";
                    break;
                case Food.Pizza:
                    orderDisplay += "Pizza\n";
                    break;
                case Food.Chips:
                    orderDisplay += "Chips\n";
                    break;
                default:
                    orderDisplay += "Unknown Item\n";
                    break;
            }
        }

        // Update the UI Text if it's assigned
        if (orderTextDisplay != null)
        {
            orderTextDisplay.text = orderDisplay;
        }
    }

    #endregion
}
