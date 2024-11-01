using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentationHardsCode : MonoBehaviour
{
    [SerializeField] Canvas upgrades;

    private void Start()
    {
        InputManager.Instance.Inputs.Cheats.Enable();

        InputManager.Instance.Inputs.Cheats.TriggerLoadshedding.performed += x => TriggerLoadShedding();
        InputManager.Instance.Inputs.Cheats.ActivateGenerator.performed += x => ActivateGenerator();
        InputManager.Instance.Inputs.Cheats.ShowUpgrades.performed += x => ShowUpgrades();
        InputManager.Instance.Inputs.Cheats.HideUpgrades.performed += x => HideUpgrades();
        InputManager.Instance.Inputs.Cheats.TriggerSpeedUpgrade.performed += x => TriggerSpeedUpgrade();
    }

    void TriggerLoadShedding()
    {
        Debug.Log("Triggered LoadShedding");
        SwitchLights.Instance.LightsOff();
    }

    void ActivateGenerator()
    {
        SwitchLights.Instance.LightsOn();
    }

    void ShowUpgrades()
    {
        upgrades.gameObject.SetActive(true);
    }

    void HideUpgrades()
    {
        upgrades.gameObject.SetActive(false);
    }

    void TriggerSpeedUpgrade()
    {
        GameManager.Instance.GetComponent<UpgradeSystem>().UpgradeShoes();
    }
}
