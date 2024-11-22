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

    [SerializeField] Generator generator;
    [SerializeField] WaterDispenser waterDispenser;
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
            
            UIManager.Instance.HideUpgradesInterface();
        }

    }

    public void UpgradeChef()
    {
        if (!chefUpgraded && CurrencyManager.Instance.GetCurrency() > 450)
        {
            CurrencyManager.Instance.RemoveCurrency(450);

            chefUpgraded = true;

            //Faster food preparation
            Kitchen.Instance.foodPrepTime /= 2;

            UIManager.Instance.HideUpgradesInterface();
        }


    }

    public void UpgradeBaristaMachine()
    {
        if(!baristaUpgraded && CurrencyManager.Instance.GetCurrency() > 600)
        {
            CurrencyManager.Instance.RemoveCurrency(600);

            baristaUpgraded = true;
            BaristaMachine baristaMachine = GameObject.FindFirstObjectByType<BaristaMachine>();
            baristaMachine.UpgradeBarista();

            UIManager.Instance.HideUpgradesInterface();
        }
    }

    public void UpgradeSodaMachine()
    {
        if (!sodaMachineUpgraded && CurrencyManager.Instance.GetCurrency() > 550)
        {
            CurrencyManager.Instance.RemoveCurrency(550);

            sodaMachineUpgraded = true;
            MiniFridge miniFridge = GameObject.FindFirstObjectByType<MiniFridge>();
            miniFridge.UpgradeMiniFridge();

            UIManager.Instance.HideUpgradesInterface();
        }
    }
    public void PurchaseGenerator()
    {
        if (!generatorUpgraded && CurrencyManager.Instance.GetCurrency() > 1200)
        {
            CurrencyManager.Instance.RemoveCurrency(1200);

            generatorUpgraded = true;
            //Generator generator = GameObject.FindFirstObjectByType<Generator>();
            generator.BuyGenerator();

            UIManager.Instance.HideUpgradesInterface();
        }
    }

    public void PurchaseWaterDispenser()
    {
        if (!waterDispensorUpgraded && CurrencyManager.Instance.GetCurrency() > 1100)
        {
            CurrencyManager.Instance.RemoveCurrency(1100);

            waterDispensorUpgraded = true;
            //WaterDispensor waterDispensor = GameObject.FindFirstObjectByType<WaterDispensor>();
            waterDispenser.BuyWaterDispensor();

            UIManager.Instance.HideUpgradesInterface();
        }
    }

    public void HireBodyguard()
    {
        if (CurrencyManager.Instance.GetCurrency() > 2500)
        {
            CurrencyManager.Instance.RemoveCurrency(2500);

            ConundrumManager.Instance.isCrimeTriggered = false;
            //Stops thieves
            UIManager.Instance.HideUpgradesInterface();

        }
    }

    public void FixSewerage()
    {
        if (ConundrumManager.Instance.isSewerageProblems == true && CurrencyManager.Instance.GetCurrency() > 150)
        {
            CurrencyManager.Instance.RemoveCurrency(150);

            ConundrumManager.Instance.isSewerageProblems = false;
            //Re-enables water supply
            UIManager.Instance.HideUpgradesInterface();
        }
    }

    public void FixRoad()
    {
        if (ConundrumManager.Instance.isRoadBad == true && CurrencyManager.Instance.GetCurrency() > 1000)
        {
            CurrencyManager.Instance.RemoveCurrency(1000);

            ConundrumManager.Instance.isRoadBad = false;
            //Customers come faster
            UIManager.Instance.HideUpgradesInterface();
        }
    }

    public void RemoveRefuse()
    {
        if (ConundrumManager.Instance.isRefuseFull == true && CurrencyManager.Instance.GetCurrency() > 300) 
        {
            CurrencyManager.Instance.RemoveCurrency(300);

            ConundrumManager.Instance.isRefuseFull = false;
            //Slower patience draining in customers
            UIManager.Instance.HideUpgradesInterface();
        }
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
