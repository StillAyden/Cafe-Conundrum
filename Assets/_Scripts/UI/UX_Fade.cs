using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UX_Fade : MonoBehaviour
{
    public static UX_Fade Instance;

    Image imgBlack;
    [SerializeField][Range(0, 0.1f)] float fadeSpeed;
    float fadeDuration;

    private void Awake()
    {
        Instance = this;
        imgBlack = GetComponentInChildren<Image>();
    }

    public void FadeIn()
    {
        StartCoroutine(IFadeIn());
    }

    public void FadeOut(string sceneName)
    {
        StartCoroutine(IFadeOut(sceneName));
    }


    IEnumerator IFadeIn()
    {
        imgBlack.color = Color.black;
        fadeDuration = 1;

        while (fadeDuration > 0)
        {
            fadeDuration -= fadeSpeed;
            imgBlack.color = new Color(0, 0, 0, fadeDuration);
            yield return new WaitForSeconds(0.0000001f);
        }
    }

    IEnumerator IFadeOut(string scene)
    {
        imgBlack.color = Color.black;
        fadeDuration = 0;

        while (fadeDuration < 1)
        {
            fadeDuration += fadeSpeed;
            imgBlack.color = new Color(0, 0, 0, fadeDuration);
            yield return new WaitForSeconds(0.0000001f);

            
        }

        yield return new WaitForSeconds(1.5f);

        try
        {
            SceneManager.LoadScene(scene);
        }
        catch
        {

        }

    }
}
