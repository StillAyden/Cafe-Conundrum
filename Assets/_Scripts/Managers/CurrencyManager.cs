using UnityEngine;

//Please Read KAT!
//This script main purpoes is to handle currency transactions and operations, the script is a Singleton and can be called in any script by using Instance.CurrencyManager.(function/procedure) 
//Important (1-3)
//-1- Use the Buy function to remove curreny
//-2- Use the GetCurrency for UI
//-3- Use AddCurrency for adding 

public class CurrencyManager : MonoBehaviour
{
    #region Variables
    //Singleton
    public static CurrencyManager Instance;

    //Currency
    [Header("Currency")]
    [SerializeField] private float currency = 0;

    #endregion

    private void Awake()
    {
        Singleton();
    }

    #region Functions

    public bool Purchase(float price)
    {
        if(currency>= price)//Enough money
        {
            RemoveCurrency(price);
            return true;
        }
        else
        { 
            return false;
        }
    }

    #endregion

    #region Get & Set & Add
    //Followers
    public float GetCurrency() { return currency; }
    private void SetCurrency(float value) { currency = value; }
    public void AddCurrency(float currencyAdded) { currency += currencyAdded; }
    private void RemoveCurrency(float currencyRemoved) { currency -= currencyRemoved; }
    #endregion

    #region Singleton

    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("Another instance of CurrencyManager was destroyed on creation!");
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion
}
