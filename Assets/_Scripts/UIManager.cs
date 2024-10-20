using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public bool isOrderingInterfaceActive = false;
    public bool isUpgradesInterfaceActive = false;

    [Header("Canvases")]
    [SerializeField] Canvas OrderingInterface;
    [SerializeField] Canvas UpgradesInterface;


    private void Awake()
    {
        Instance = this;
    }

    #region Ordering Interface
    public void ShowOrderingInterface()
    {
        if (OrderingInterface)
        {
            OrderingInterface.gameObject.SetActive(true);
            isOrderingInterfaceActive = true;
        }
        else Debug.LogWarning("Could not find Ordering Interface Canvas");
    }

    public void HideOrderingInterface()
    {
        if (OrderingInterface)
        {
            OrderingInterface.gameObject.SetActive(false);
            isOrderingInterfaceActive = false;
        }
        else Debug.LogWarning("Could not find Ordering Interface Canvas");
    }
    #endregion

    #region Upgrades Interface
    public void ShowUpgradesInterface()
    {
        if (UpgradesInterface)
        {
            UpgradesInterface.gameObject.SetActive(true);
            isUpgradesInterfaceActive = true;
        }
        else Debug.LogWarning("Could not find Upgrades Canvas!");
    }

    public void HideUpgradesInterface()
    {
        if (UpgradesInterface)
        {
            UpgradesInterface.gameObject.SetActive(false);
            isOrderingInterfaceActive = false;
        }
        else Debug.LogWarning("Could not find Upgrades Canvas!");
    }
    #endregion
}
