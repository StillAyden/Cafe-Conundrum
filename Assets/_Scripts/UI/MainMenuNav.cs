using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNav : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject credits;

    public void MainMenu()
    {
        SoundManager.PlaySound(SoundType.BUTTON_CLICKING, SoundMode.VFX, this.transform.position);
        HideAll();
        mainMenu.SetActive(true);
    }

    public void Settings()
    {
        SoundManager.PlaySound(SoundType.BUTTON_CLICKING, SoundMode.VFX, this.transform.position);
        HideAll();
        settings.SetActive(true);
    }

    public void Credits()
    {
        SoundManager.PlaySound(SoundType.BUTTON_CLICKING, SoundMode.VFX, this.transform.position);
        HideAll();
        credits.SetActive(true);
    }

    public void StartGame()
    {
        SoundManager.PlaySound(SoundType.BUTTON_CLICKING, SoundMode.VFX, this.transform.position);
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGame()
    {
        SoundManager.PlaySound(SoundType.BUTTON_CLICKING, SoundMode.VFX, this.transform.position);
        Application.Quit();
    }

    public void HideAll()
    {
        mainMenu.SetActive(false);
        settings.SetActive(false);
        credits.SetActive(false);
    }
}
