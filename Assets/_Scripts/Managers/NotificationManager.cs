using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager Instance;

    [SerializeField] Canvas NotificationCanvas;
    [SerializeField] Text NotificationText;
    Coroutine notificationTimer = null;

    private void Awake()
    {
        Instance = this; 
    }

    public void ShowNotification(string text)
    {
        if (notificationTimer == null)
        {
            notificationTimer = StartCoroutine(ShowNotificationTimed(text, 5f));
        }
    }

    IEnumerator ShowNotificationTimed(string text, float time)
    {
        NotificationText.text = text;
        NotificationCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        NotificationCanvas.gameObject.SetActive(false);
        notificationTimer = null;
    }
}
