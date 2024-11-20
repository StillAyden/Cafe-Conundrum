using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Data")]
    [SerializeField] public foodData food;
    [SerializeField] public drinkData drink;

    [Header("Customer Spawning")]
    public bool startSpawningCustomers = false;

    [Header("Challenge Variables")]
    public bool isLoadshedding = false;
    public bool isWaterShortage = false;
    public bool isSewerageProblem = false;
    public bool hasGenerator = false;
    public bool hasWaterDispensor = false;
    public float roadDurabilty = 1f;
    public float refuseLevel = 0f;

    private void Awake()
    {
        Instance = this;
    }
    private IEnumerator Start()
    {
        UX_Fade.Instance?.FadeIn();
        yield return new WaitForSeconds(2f);
        TutorialManager.Instance?.StartDialogue();
        yield return new WaitForSeconds(2f);
        startSpawningCustomers = true;
    }
}
