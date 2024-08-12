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
                //image.color = Chips;
                return Food.Chips;
                
            case 2: 
                //image.color = Burger;
                return Food.Burger;
                
            case 3: 
                //image.color = Pizza;
                return Food.Pizza;

            default: return Food.None;
        }
    }
}

public enum Food
{
    None,
    Chips,
    Burger,
    Pizza
}
