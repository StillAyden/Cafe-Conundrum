using UnityEngine;

public class SettingNav : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private GameObject graphicsPanel;
    [SerializeField] private GameObject audiosPanel;


    public void GamePlay()
    {
        SoundManager.PlaySound(SoundType.BUTTON_CLICKING, SoundMode.VFX, this.transform.position);
        HideAll();
        gamePlayPanel.SetActive(true);
    }

    public void Graphics()
    {
        SoundManager.PlaySound(SoundType.BUTTON_CLICKING, SoundMode.VFX, this.transform.position);
        HideAll();
        graphicsPanel.SetActive(true);
    }

    public void Audios() 
    {
        SoundManager.PlaySound(SoundType.BUTTON_CLICKING, SoundMode.VFX, this.transform.position);
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
