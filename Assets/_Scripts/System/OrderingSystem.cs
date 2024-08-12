using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderingSystem : MonoBehaviour
{
    #region Variables
    //Classes
    private Kitchen kitchen;

    //Vars
    public string orderText;
    public List<Order> orders = new List<Order>();

    #endregion

    #region Unity Methods

    void Start()
    {
        orderText = "";
        kitchen = FindObjectOfType<Kitchen>();
    }

    #endregion

    #region Public Methods

    public void AddChipsOrder()
    {
        AddToOrder("Chips");
        orders.Add(new Order());
    }

    public void AddBurgerOrder()
    {
        AddToOrder("Burger");
        orders.Add(new Order());
    }

    public void AddPizzaOrder()
    {
        AddToOrder("Pizza");
        orders.Add(new Order());
    }

    public void PlaceOrder()
    {
        Debug.Log("Order placed: " + orderText);
        kitchen.AddOrder(orders);
        orders.Clear();
    }

    #endregion

    #region Private Methods

    private void AddToOrder(string orderItem)
    {
        if (!string.IsNullOrEmpty(orderText))
        {
            orderText += "\n";
        }

        orderText += orderItem;

    }

    #endregion
}
