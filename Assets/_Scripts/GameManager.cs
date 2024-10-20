using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Data")]
    [SerializeField] public foodData food;
    [SerializeField] public drinkData drink;

    [Header("Customer Spawning")]
    public bool startSpawningCustomers = false;

    private void Awake()
    {
        Instance = this;
    }
    private IEnumerator Start()
    {
        //Debug.unityLogger.logEnabled = false;                 //Uncomment to disable all "Debug.Log()"

        UX_Fade.Instance?.FadeIn();
        yield return new WaitForSeconds(2f);
        TutorialManager.Instance?.StartDialogue();
        yield return new WaitForSeconds(2f);
        startSpawningCustomers = true;
    }
}
