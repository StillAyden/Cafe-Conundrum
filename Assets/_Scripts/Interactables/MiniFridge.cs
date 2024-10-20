using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniFridge : Interactable
{
    [SerializeField] int drinksLeft = 50; 

    public GameObject Interact()
    {
        if (drinksLeft > 0)
        {
            drinksLeft--;
            return GameManager.Instance.drink.items[(int)Drink.Soda].prefab;
        }

        return null;
    }
}
