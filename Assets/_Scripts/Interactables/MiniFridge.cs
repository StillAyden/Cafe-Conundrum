using UnityEngine;

public class MiniFridge : Interactable
{
    [SerializeField] int Maxdrinks = 20; 
    [SerializeField] int drinksLeft = 20; 

    public GameObject Interact()
    {
        bool hasPower = !GameManager.Instance.isLoadshedding || GameManager.Instance.hasGenerator;
        if (!hasPower)
        {
            // No power, cannot interact
            Debug.Log("No power available.");
            return null;
        }

        if (drinksLeft > 0)
        {
            drinksLeft--;
            return GameManager.Instance.drink.items[(int)Drink.Soda].prefab;
        }

        return null;
    }

    #region Upgrade

    public void UpgradeMiniFridge()
    {
        Maxdrinks *= 2;
    }

    #endregion
}
