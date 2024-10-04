using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSystem : Interactable
{
    #region Variables
    
    #endregion

    public void UpgradeShoes()
    {
        //Increase movement speed
    }

    public void UpgradeChef()
    {
        //Faster food preparation
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
