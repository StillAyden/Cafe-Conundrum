using UnityEngine;
using UnityEngine.UI;

public class OrderingSystem : MonoBehaviour
{
    #region Variables

    public string orderText; 

    #endregion

    #region Unity Methods

    void Start()
    {
        orderText = "";
    }

    #endregion

    #region Public Methods

    public void AddChipsOrder()
    {
        AddToOrder("Chips");
    }

    public void AddBurgerOrder()
    {
        AddToOrder("Burger");
    }

    public void AddPizzaOrder()
    {
        AddToOrder("Pizza");
    }

    public void PlaceOrder()
    {
        Debug.Log("Order placed: " + orderText);
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
