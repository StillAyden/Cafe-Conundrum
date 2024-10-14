using UnityEngine;

public class DrinkItem : Interactable
{
    #region Variables
    [Header("Drink Type")]
    public Drink drink = Drink.None;

    #endregion

    #region Functions

    private void ApplyDrinkItem()
    {
        //Remove drink child
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GameObject newChild = null;

        //Spawn the new food prefab as a child
    }

    #endregion

    #region GetSet
    public void SetDrinkType(Drink newDrinkType)
    {
        drink = newDrinkType;
        ApplyDrinkItem();
    }
    #endregion
}
