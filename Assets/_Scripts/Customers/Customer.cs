using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    #region Variables
    [SerializeField] public Order order;
    [SerializeField] private Table table = null;

    [Space]
    [Header("Sprite Settings")]
    [SerializeField] private Image image;
    [SerializeField] private bool hasGottenFood = false;

    //Temp Vars
    [SerializeField] private Sprite Chips;
    [SerializeField] private Sprite Burger;
    [SerializeField] private Sprite Pizza;

    #endregion

    private void Awake()
    {
        order = new Order();
        DisplayOrderSprite();
        hasGottenFood = false;
    }

    #region Functions

    private void DisplayOrderSprite()
    {
        switch (order.food)
        {
            //Chips
            case Food.Chips:
                image.sprite = Chips;
                break;
            //Burger
            case Food.Burger:
                image.sprite = Burger;
                break;
            //Pizza
            case Food.Pizza:
                image.sprite = Pizza;
                break;
            //None
            default:
                image.color = Color.red;
                break;
        }

    }

    #endregion

    #region GetSet

    public bool HasGottenFood
    {
        get { return hasGottenFood; }

        set 
        {           
            hasGottenFood = value;

            if (value) 
            { 
                table.TableClearTimer();
                SetShowSprite(value);
            }
        }
    }

    public void SetTable(Table t) {table = t;}

    public void SetShowSprite(bool show)
    {
        //Always hide the image if the customer has gotten food
        if (hasGottenFood)
        {
            image.gameObject.SetActive(false);
            order.food = Food.None; // Reset order to none
            return; //Exit to not shown the image
        }

        image.gameObject.SetActive(show); 
    }

    #endregion
}
