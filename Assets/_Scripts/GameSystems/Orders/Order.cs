using System.Collections.Generic;
using UnityEngine;

public class Order  
{
    public Food food = Food.None;

    public Order()
    {
        food = GenerateRandomOrder();
    }

    Food GenerateRandomOrder()
    {
        int num = Random.Range(1, System.Enum.GetValues(typeof(Food)).Length);

        switch (num) //Select Random Food Item
        {
            case 1: 
                return Food.Chips;
                
            case 2: 
                return Food.Burger;
                
            case 3: 
                return Food.Pizza;

            default: return Food.None;
        }
    }

    #region GetSet

    public void SetFoodType(Food type)
    {
        food = type;
    }

    public Food GetFoodType()
    {
        return food; 
    }

    #endregion
}

public enum Food
{
    None,
    Chips,
    Burger,
    Pizza
}
