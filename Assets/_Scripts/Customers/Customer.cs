using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    #region Variables
    [SerializeField] public Food foodOrder = Food.None;
    [SerializeField] public Drink drinkOrder = Drink.None;
    [SerializeField] private Table table = null;

    [Space]
    [Header("Sprite Settings")]
    [SerializeField] private Image imageFood;
    [SerializeField] private Image imageDrink;
    [SerializeField] private bool hasGottenFood = false;
    [SerializeField] private bool hasGottenDrink = false;

    //Food Data
    [SerializeField] private foodData food;
    [SerializeField] private drinkData drink;
    
    #endregion

    private void Awake()
    {
        foodOrder = GetRandomFoodOrder();
        drinkOrder = GetRandomDrinkOrder();
        DisplayOrderSprite();
    }

    #region Functions

    private Food GetRandomFoodOrder()
    {
        int randomIndex = Random.Range(0, food.items.Length);
        return food.items[randomIndex].type;
    }

    private Drink GetRandomDrinkOrder()
    {
        int randomIndex = Random.Range(-1, drink.items.Length);
        return drink.items[randomIndex].type;
    }

    private void DisplayOrderSprite() 
    {
        //Food
        foreach (foodItems foodItem in food.items)
        {
            if (foodItem.type == foodOrder)
            {
                imageFood.sprite = foodItem.image;
                return;
            }
        }

        //Drink
        if (drinkOrder == Drink.None)
        {
            imageDrink.gameObject.SetActive(false); // Hide the drink image
            hasGottenDrink = true; 
        }
        else
        {
            foreach (drinkItems drinkItem in drink.items)
            {
                if (drinkItem.type == drinkOrder)
                {
                    imageDrink.sprite = drinkItem.image;
                    imageDrink.gameObject.SetActive(true); // Show drink image
                    hasGottenDrink = false;
                    break;
                }
            }
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

            if (HasGottenFood && HasGottenDrink) 
            { 
                table.TableClearTimer();               
            }

            SetShowFoodSprite(value);
        }
    }

    public bool HasGottenDrink
    {
        get { return hasGottenDrink; }
        set
        {
            hasGottenDrink = value;

            if (HasGottenFood && HasGottenDrink)
            {
                table.TableClearTimer();                
            }

            SetShowDrinkSprite(value);
        }
    }

    public void SetTable(Table t) {table = t;}

    public void SetShowFoodSprite(bool show)
    {
        //Always hide the image if the customer has gotten food
        if (hasGottenFood)
        {
            imageFood.gameObject.SetActive(false);
            foodOrder = Food.None; // Reset order to none
            return; //Exit to not shown the image
        }

        imageFood.gameObject.SetActive(show); 
    }

    public void SetShowDrinkSprite(bool show)
    {
        //Always hide the image if the customer has gotten drink
        if (hasGottenDrink)
        {
            imageDrink.gameObject.SetActive(false);
            drinkOrder = Drink.None; // Reset order to none
            return; //Exit to not shown the image
        }

        imageDrink.gameObject.SetActive(show);
    }

    public void SetShowSprite(bool show)
    {
        // If the customer has already gotten their food, hide the food sprite
        if (hasGottenFood)
        {
            imageFood.gameObject.SetActive(false);
        }
        else
        {
            imageFood.gameObject.SetActive(show); // Show or hide based on the parameter
        }

        // If the customer has already gotten their drink, hide the drink sprite
        if (hasGottenDrink)
        {
            imageDrink.gameObject.SetActive(false);
        }
        else
        {
            imageDrink.gameObject.SetActive(show); // Show or hide based on the parameter
        }
    }

    #endregion
}
