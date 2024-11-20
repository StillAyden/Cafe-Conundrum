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
        Debug.LogWarning("Power will turn OFF in x minutes: Pre-prepare food and hot drinks");
        throw new NotImplementedException();
    }

    private void StartWaterShortage()
    {
        isWaterShortage = true;
        //Trigger "BREAKING NEWS: WATER SHORTAGE IN ** AREA"
        Debug.LogWarning("No Water: Water Dispenser must be purchased");
    }

    private void StartSewerageProblems()
    {
        isSewerageProblems = true;
        //Trigger "BREAKING NEWS: SEWERAGE LINE BURST IN ** AREA"
        Debug.LogWarning("Sewerage Not in working order: Customer Satisfaction Reduced Dramatically!");
    }

    private void StartRoadMainenance()
    {
        isRoadBad = true;
        Debug.LogWarning("Road not up to standard: Less customers will begin to arrive!");
    }

    private void StartCrime()
    {
        isCrimeTriggered = true;
        //Trigger "CRIMINAL AUDIO CUE"
        Debug.LogWarning("Criminal in the area! Watch your tips!!");
    }

    private void StartRefuseFull()
    {
        isRefuseFull = true;
        Debug.LogWarning("Waste Content High: Customer Patience Reduced Dramatically!");
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
