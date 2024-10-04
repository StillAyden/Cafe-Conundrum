using UnityEngine;

public class FoodItem : Interactable
{
    #region Variables
    [Header("Food Type")]
    public Food food = Food.None;

    [Space]
    [Header("Food Prefabs")]
    [SerializeField] private GameObject burger;
    [SerializeField] private GameObject pizza;
    [SerializeField] private GameObject chips;
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
        switch (food)
        { 
            ////Burger
            //case Food.Burger:
            //    newChild = Instantiate(burger, transform.position,Quaternion.identity, transform);
            //    break;
            ////Pizza
            //case Food.Pizza:
            //    newChild = Instantiate(pizza, transform.position, Quaternion.identity, transform);
            //    break;
            ////Chips
            //case Food.Chips:
            //    newChild = Instantiate(chips, transform.position, Quaternion.identity, transform);
            //    break;
            ////None
            //default:
            //    Debug.LogWarning("Invalid food type selected.");
            //    break;
        }
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
