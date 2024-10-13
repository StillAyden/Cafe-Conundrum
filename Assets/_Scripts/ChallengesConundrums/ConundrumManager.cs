using System;
using UnityEngine;

public class ConundrumManager : MonoBehaviour
{
    public static ConundrumManager Instance;

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
        Debug.LogWarning("Power will turn OFF in x minutes: Pre-prepare food and hot drinks");
        throw new NotImplementedException();
    }

    private void StartWaterShortage()
    {
        //Trigger "BREAKING NEWS: WATER SHORTAGE IN ** AREA"
        Debug.LogWarning("No Water: Water Dispensir must be purchased");
        throw new NotImplementedException();
    }

    private void StartSewerageProblems()
    {
        //Trigger "BREAKING NEWS: SEWERAGE LINE BURST IN ** AREA"
        Debug.LogWarning("Sewerage Not in working order: Customer Satisfaction Reduced Dramatically!");
        throw new NotImplementedException();
    }

    private void StartRoadMainenance()
    {
        Debug.LogWarning("Road not up to standard: Less customers will begin to arrive!");
        throw new NotImplementedException();
    }

    private void StartCrime()
    {
        //Trigger "CRIMINAL AUDIO CUE"
        Debug.LogWarning("Criminal in the area! Watch your tips!!");
        throw new NotImplementedException();
    }

    private void StartRefuseFull()
    {

        Debug.LogWarning("Waste Content High: Customer Patience Reduced Dramatically!");
        throw new NotImplementedException();
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
