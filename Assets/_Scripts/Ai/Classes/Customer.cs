using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    #region Variables

    [SerializeField] private Food orders = Food.None;
    public enum Food
    {
        None,
        Chips,
        Burger,
        Pizza
    }

    [SerializeField] private Image image;
    [SerializeField] private bool isDoneEating = false;

    //Temp Vars
    [SerializeField] private Color Chips;
    [SerializeField] private Color Burger;
    [SerializeField] private Color Pizza;

    #endregion

    private void Awake()
    {
        int num = Random.Range(1, System.Enum.GetValues(typeof(Food)).Length);

        switch (num) //Select Random Food Item
        {
            case 1:
                orders = Food.Chips;
                image.color = Chips;
            break;

            case 2:
                orders = Food.Burger;
                image.color = Burger;
            break;

            case 3:
                orders = Food.Pizza;
                image.color = Pizza;
            break;

        }
    }

    #region GetSet

    public bool GetIsDoneEating() { return isDoneEating; }

    #endregion
}
