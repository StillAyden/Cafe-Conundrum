using UnityEngine;

public class Generator : Interactable
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.hasGenerator = false;
    }

    public void Interact()
    {
        ConundrumManager.Instance.isLoadshedding = false;
        SwitchLights.Instance.LightsOn();
    }

    #region Upgrade

    public void BuyGenerator()
    {
        this.gameObject.SetActive(true);
        GameManager.Instance.hasGenerator = true;
    }

    #endregion
}
