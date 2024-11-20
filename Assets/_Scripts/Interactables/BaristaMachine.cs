using System.Collections;
using UnityEngine;

public class BaristaMachine : Interactable
{
    #region Variables

    [Header("UI")]
    [SerializeField] Canvas prepTimer;
    [Space]

    [Header("Prepair Settings")]

    [Tooltip("The total amount of time to make a drink.")]
    [SerializeField] float prepTime = 10f;
    private bool canCollect = false;
    private bool isPreparing = false;

    [Header("Water Settings")]
    [Tooltip("Maximum capacity of the water tank.")]
    public float maxWaterLevel = 100.0f;

    [Tooltip("Current water level in the tank.")]
    public float currentWaterLevel = 20.0f;

    [Tooltip("Amount of water used per drink.")]
    public float waterPerDrink = 20.0f;

    // Cost Variables
    [Header("Sound")]
    public AudioClip soundEffect;

    #endregion

    #region Drinks

    public GameObject Interact()
    {
        // Determine if we have power
        bool hasPower = !GameManager.Instance.isLoadshedding || GameManager.Instance.hasGenerator;
        if (!hasPower)
        {
            // No power, cannot interact
            Debug.Log("No power available.");
            return null;
        }

        // Checks if we need to check water levels
        bool needWater = GameManager.Instance.isWaterShortage;

        // Check if the machine is ready to prepare a drink
        if (!canCollect && !isPreparing)
        {
            if (needWater)
            {
                // Check if there is enough water
                if (!HasEnoughWater())
                {
                    Debug.Log("Not enough water to prepare the drink.");
                    return null;
                }
            }

            // Start preparing the drink
            StartCoroutine(StartDrinkPrep(needWater));
            return null;
        }
        // Check if the drink is ready to collect
        else if (canCollect && !isPreparing)
        {
            canCollect = false;
            return GameManager.Instance.drink.items[(int)Drink.Coffee].prefab;
        }

        // If none of the above conditions are met
        return null;
    }

    IEnumerator StartDrinkPrep(bool deductWater)
    {
        if (deductWater)
        {
            currentWaterLevel -= waterPerDrink;
            currentWaterLevel = Mathf.Max(currentWaterLevel, 0); // Prevent negative water level
        }

        isPreparing = true;
        prepTimer.gameObject.SetActive(true);
        yield return new WaitForSeconds(prepTime);
        isPreparing = false;
        prepTimer.gameObject.SetActive(false);

        canCollect = true;
    }

    #endregion

    #region Function

    bool HasEnoughWater()
    {
        return currentWaterLevel >= waterPerDrink;
    }

    public void RefillWater()
    {
        currentWaterLevel = maxWaterLevel;
    }


    #endregion

    #region Upgrade

    public void UpgradeBarista()
    {
        // Upgrade Speed
        prepTime *=  0.5f;

        // Upgrade Water level
        maxWaterLevel = maxWaterLevel * 2; 
    }

    #endregion
}
