using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeSystem : Interactable
{
    public static UpgradeSystem Instance;

    public bool shoesUpgraded = false;
    public bool chefUpgraded = false;
    public bool baristaUpgraded = false;
    public bool sodaMachineUpgraded = false;
    public bool generatorUpgraded = false;
    public bool bodyguardHired = false;
    //public bool sewerageFixed = false;
    //public bool roadFixed = false;
    //public bool refuseRemoved = false;
    private void Awake()
    {
        Instance = this;
    }
    public void UpgradeShoes()
    {
        if (shoesUpgraded == false)
        {
            shoesUpgraded = true;

            //Increase movement speed
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().moveSpeed = 250f;
        }
    }

    public void UpgradeChef()
    {
        if (chefUpgraded == false)
        {
            chefUpgraded = true;

            //Faster food preparation
            Kitchen.Instance.foodPrepTime /= 2;
        }
    }

    public void UpgradeBaristaMachine()
    {
        //Faster Hot Drinks Preparation (and Larger water capacity?)
    }

    public void UpgradeSodaMachine()
    {
        //Faster Cold Drinks Preparation (and Larger water capacity?)
    }

    public void PurchaseGenerator()
    {
        //Allows power when loadshedding occurs
    }

    public void PurchaseWaterDispenser()
    {
        //Allows water when there is a water shortage
    }

    public void HireBodyguard()
    {
        //Stops thieves
    }

    public void FixSewerage()
    {
        //Re-enables water supply
    }

    public void FixRoad()
    {
        //Customers come faster
    }

    public void RemoveRefuse()
    {
        //Slower patience draining in customers
    }

}

public enum Upgrade
{
    UpgradedShoes,
    ProChef,
    UpgradedBaristaMachine,
    UpgradedSodaMachine,
    Generator,
    WaterDispenser,
    Bodyguard,
    SewerageFix,
    RoadMaintenance,
    RefuseRemoval
}
