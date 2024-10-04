using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    #region Variables
    [SerializeField] public Food foodOrder = Food.None;
    [SerializeField] private Table table = null;

    [Space]
    [Header("Sprite Settings")]
    [SerializeField] private Image image;
    [SerializeField] private bool hasGottenFood = false;

    //Temp Vars
    [SerializeField] private Sprite Chips;
    [SerializeField] private Sprite Burger;
    [SerializeField] private Sprite Pizza;

    //Food Data
    [SerializeField] private foodData food;
    
    #endregion

    private void Awake()
    {
        foodOrder = GetRandomOrder();
        DisplayOrderSprite();
        hasGottenFood = false;
    }

    #region Functions

    private Food GetRandomOrder()
    {
        int randomIndex = Random.Range(0, food.items.Length);
        return food.items[randomIndex].type;
    }

    private void DisplayOrderSprite()
    {

        foreach (foodItems foodItem in food.items)
        {
            if (foodItem.type == foodOrder)
            {
                image.sprite = foodItem.image;
                return;
            }
        }

        // if no food sprite is found
        image.color = Color.red;
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
            foodOrder = Food.None; // Reset order to none
            return; //Exit to not shown the image
        }

        image.gameObject.SetActive(show); 
    }

    #endregion
}
