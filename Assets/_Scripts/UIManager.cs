using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public bool isOrderingInterfaceActive = false;
    public bool isUpgradesInterfaceActive = false;
    public bool isPauseInterfaceActive = false;

    [Header("Canvases")]
    [SerializeField] Canvas OrderingInterface;
    [SerializeField] Canvas UpgradesInterface;
    [SerializeField] Canvas PauseInterface;

    [Header("Reputation")]
    [SerializeField] Image imgRepBar;

    [Header("Money")]
    [SerializeField] public Text[] allTextMoney;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateReputationBar();

        for (int i = 0; i < allTextMoney.Length; i++)
        {
            allTextMoney[i].text = CurrencyManager.Instance.GetCurrency().ToString();
        }
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
            Debug.LogWarning("ShowUpgradesInterface");
        }
        else Debug.LogWarning("Could not find Upgrades Canvas!");
    }

    public void HideUpgradesInterface()
    {
        if (UpgradesInterface)
        {
            UpgradesInterface.gameObject.SetActive(false);
            isUpgradesInterfaceActive = false;
            Debug.LogWarning("HideUpgradesInterface!");
        }
        else Debug.LogWarning("Could not find Upgrades Canvas!");
    }
    #endregion

    #region Reputation
    void UpdateReputationBar()
    {
        imgRepBar.fillAmount = ReputationManager.Instance.GetReputation() / 100;
    }
    #endregion



    public void ShowPauseInterface()
    {
        if (PauseInterface)
        {
            PauseInterface.gameObject.SetActive(true);
            isPauseInterfaceActive = true;
        }
        else Debug.LogWarning("Could not find Pause Interface Canvas");
    }

    public void HidePauseInterface()
    {
        if (PauseInterface)
        {
            PauseInterface.gameObject.SetActive(false);
            isPauseInterfaceActive = false;
        }
        else Debug.LogWarning("Could not find Ordering Interface Canvas");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
