using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    #region Variables

    [SerializeField] public Order order;

    [SerializeField] private Image image;
    [SerializeField] private bool isDoneEating = false;

    //Temp Vars
    [SerializeField] private Color Chips;
    [SerializeField] private Color Burger;
    [SerializeField] private Color Pizza;

    #endregion

    private void Awake()
    {
        order = new Order();
    }

    #region GetSet

    public bool IsDoneEating
    {
        get { return isDoneEating; }
        set { isDoneEating = value; }
    }

    #endregion
}
