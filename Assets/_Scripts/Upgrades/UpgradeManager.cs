using UnityEngine;

public class UpgradeSystem : Interactable
{
    public static UpgradeSystem Instance;

    public bool shoesUpgraded = false;
    public bool chefUpgraded = false;
    public bool baristaUpgraded = false;
    public bool sodaMachineUpgraded = false;
    public bool generatorUpgraded = false;
    public bool waterDispensorUpgraded = false;
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
        if (!shoesUpgraded)
        {
            shoesUpgraded = true;

            //Increase movement speed
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().moveSpeed = 250f;
        }
    }

    public void UpgradeChef()
    {
        if (!chefUpgraded)
        {
            chefUpgraded = true;

            //Faster food preparation
            Kitchen.Instance.foodPrepTime /= 2;
        }
    }

    public void UpgradeBaristaMachine()
    {
        if(!baristaUpgraded)
        {
            baristaUpgraded= true;

            BaristaMachine baristaMachine = GameObject.FindFirstObjectByType<BaristaMachine>();

            baristaMachine.UpgradeBarista();
        }
    }

    public void UpgradeSodaMachine()
    {
        if (!sodaMachineUpgraded)
        {
            sodaMachineUpgraded = true;

            MiniFridge miniFridge = GameObject.FindFirstObjectByType<MiniFridge>();

            miniFridge.UpgradeMiniFridge();
        }
    }
    public void PurchaseGenerator()
    {
        if (!generatorUpgraded)
        {
            generatorUpgraded = true;

            Generator generator = GameObject.FindFirstObjectByType<Generator>();

            generator.BuyGenerator();
        }
    }

    public void PurchaseWaterDispenser()
    {
        if (!waterDispensorUpgraded)
        {
            waterDispensorUpgraded = true;

            WaterDispensor waterDispensor = GameObject.FindFirstObjectByType<WaterDispensor>();

            waterDispensor.BuyWaterDispensor();
        }
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
