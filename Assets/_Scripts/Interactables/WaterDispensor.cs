
using UnityEngine;

public class WaterDispenser : Interactable
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.hasWaterDispensor = false;
    }

    #region Functions

    public void RefillWater()
    {
        BaristaMachine baristaMachine = GameObject.FindFirstObjectByType<BaristaMachine>();

        baristaMachine.RefillWater();
    }

    #endregion

    #region Upgrade

    public void BuyWaterDispensor()
    {
        this.gameObject.SetActive(true);
        GameManager.Instance.hasWaterDispensor = true;
    }

    #endregion
}
