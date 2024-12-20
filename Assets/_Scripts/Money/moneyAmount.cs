using UnityEngine;

public class moneyAmount : MonoBehaviour
{
    public int moneyAmout = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SoundManager.PlaySound(SoundType.CASH, SoundMode.VFX, this.transform.position);
            CurrencyManager.Instance?.AddCurrency(moneyAmout);
            Destroy(this.gameObject);

            if (TutorialEvents.Instance)
                TutorialEvents.Instance.collectTipTrigger = true;
        }
        else if(other.CompareTag("Thief"))
        {
            Destroy(this.gameObject);
        }
    }
}
