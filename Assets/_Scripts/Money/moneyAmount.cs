using UnityEngine;

public class moneyAmount : MonoBehaviour
{
    public int moneyAmout = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CurrencyManager.Instance?.AddCurrency(moneyAmout);
            Destroy(this.gameObject);
        }
        else if(other.CompareTag("Thief"))
        {
            Destroy(this.gameObject);
        }
    }
}
