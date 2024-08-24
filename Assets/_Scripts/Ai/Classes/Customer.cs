using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    #region Variables

    [SerializeField] public Order order;
    [SerializeField] private Table table = null;

    [SerializeField] private Image image;
    [SerializeField] private bool hasGottenFood = false;

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

    public bool HasGottenFood
    {
        get { return hasGottenFood; }
        set 
        {           
            hasGottenFood = value;

            if (value) 
            { 
                table.TableClearTimer(); 
                image.gameObject.SetActive(false);
            }
        }
    }

    public void SetTable(Table t) {table = t;}



    #endregion
}
