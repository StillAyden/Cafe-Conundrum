
public class WaterDispensor : Interactable
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.hasWaterDispensor = false;
    }

    #region Upgrade

    public void BuyWaterDispensor()
    {
        this.gameObject.SetActive(true);
        GameManager.Instance.hasWaterDispensor = true;
    }

    #endregion
}
