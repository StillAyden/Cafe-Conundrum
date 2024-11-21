using System;
using UnityEngine;

public class ConundrumManager : MonoBehaviour
{
    public static ConundrumManager Instance;

    #region Bools
    public bool isLoadshedding = false;
    public bool isWaterShortage = false;
    public bool isSewerageProblems = false;
    public bool isRoadBad = false;
    public bool isCrimeTriggered = false;
    public bool isRefuseFull = false;
    #endregion

    private void Awake()
    {
        Instance = this;
    }
    public void TriggerConundrum(ConundrumTypes type)
    {
        switch (type)
        {
            case ConundrumTypes.LoadShedding:
                {
                    StartLoadshedding();
                    break;
                }
            case ConundrumTypes.WaterShortage:
                {
                    StartWaterShortage();
                    break;
                }
            case ConundrumTypes.Sewerage:
                {
                    StartSewerageProblems();
                    break;
                }
            case ConundrumTypes.RoadMaintenance:
                {
                    StartRoadMainenance();
                    break;
                }
            case ConundrumTypes.Crime:
                {
                    StartCrime();
                    break;
                }
            case ConundrumTypes.RefuseRemoval:
                {
                    StartRefuseFull();
                    break;
                }
        }
    }





    private void StartLoadshedding()
    {
        isLoadshedding = true;
        SwitchLights.Instance.LightsOff();
        NotificationManager.Instance.ShowNotification("Loadshedding is Currently Active!");
        Debug.LogWarning("Power will turn OFF in x minutes: Pre-prepare food and hot drinks");
        //Trigger UI Indication
    }

    private void StartWaterShortage()
    {
        isWaterShortage = true;
        NotificationManager.Instance.ShowNotification("There is currently a water shortage!");
        Debug.LogWarning("No Water: Water Dispenser must be purchased");
        //Trigger "BREAKING NEWS: WATER SHORTAGE IN ** AREA" (Trigger UI Indication)
    }

    private void StartSewerageProblems()
    {
        isSewerageProblems = true;
        NotificationManager.Instance.ShowNotification("Sewerage Not in working order: Customer Satisfaction Reduced Dramatically!");
        Debug.LogWarning("Sewerage Not in working order: Customer Satisfaction Reduced Dramatically!");
        //Trigger "BREAKING NEWS: SEWERAGE LINE BURST IN ** AREA"(Trigger UI Indication)
    }

    private void StartRoadMainenance()
    {
        isRoadBad = true;
        NotificationManager.Instance.ShowNotification("Road not up to standard: Less customers will begin to arrive!");
        Debug.LogWarning("Road not up to standard: Less customers will begin to arrive!");
        //Trigger UI Indication
    }

    private void StartCrime()
    {
        isCrimeTriggered = true;
        NotificationManager.Instance.ShowNotification("Criminal in the area! Watch your tips!!");
        Debug.LogWarning("Criminal in the area! Watch your tips!!");
        //Trigger "CRIMINAL AUDIO CUE" (Trigger UI Indication)
    }

    private void StartRefuseFull()
    {
        isRefuseFull = true;
        NotificationManager.Instance.ShowNotification("Waste Content High: Customer Patience Reduced Dramatically!");
        Debug.LogWarning("Waste Content High: Customer Patience Reduced Dramatically!");
        //Trigger UI Indication
    }

}

public enum ConundrumTypes
{
    LoadShedding,
    WaterShortage,
    Sewerage,
    RoadMaintenance,
    Crime,
    RefuseRemoval
}
