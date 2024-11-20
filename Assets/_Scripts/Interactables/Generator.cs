using UnityEngine;

public class Generator : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        GameManager.Instance.hasGenerator = false;
    }

    #region Upgrade

    public void BuyGenerator()
    {
        this.gameObject.SetActive(true);
        GameManager.Instance.hasGenerator = true;
    }

    #endregion
}
