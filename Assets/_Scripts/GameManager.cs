using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        //Debug.unityLogger.logEnabled = false;                 //Uncomment to disable all "Debug.Log()"

        UX_Fade.Instance?.FadeIn();
    }
}
