using UnityEngine;

public class SettingNav : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private GameObject graphicsPanel;
    [SerializeField] private GameObject audiosPanel;


    public void GamePlay()
    {
        HideAll();
        gamePlayPanel.SetActive(true);
    }

    public void Graphics()
    {
        HideAll();
        graphicsPanel.SetActive(true);
    }

    public void Audios() 
    {
        HideAll();
        audiosPanel.SetActive(true);
    }

    private void HideAll()
    {
        gamePlayPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        audiosPanel.SetActive(false);
    }
}
