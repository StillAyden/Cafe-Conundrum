using UnityEngine;

public class FoodItem : Interactable
{
    #region Variables
    [Header("Food Type")]
    public Food food = Food.None;

    #endregion

    #region Functions

    private void ApplyFoodItem()
    {
        //Remove food child
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GameObject newChild = null;

        //Spawn the new food prefab as a child
    }

    #endregion

    #region GetSet
    public void SetFoodType(Food newFoodType)
    {
        food = newFoodType;
        ApplyFoodItem();
    }
    #endregion
}
