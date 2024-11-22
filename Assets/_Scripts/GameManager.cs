using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Data")]
    [SerializeField] public foodData food;
    [SerializeField] public drinkData drink;

    [Header("Customer Spawning")]
    CustomerSpawnManager spawnManager;
    TableManager tableManager;
    public bool startSpawningCustomers = false;

    [Header("Challenge Variables")]
    public bool isLoadshedding = false;
    public bool isWaterShortage = false;
    public bool isSewerageProblem = false;
    public bool hasGenerator = false;
    public bool hasWaterDispensor = false;
    public float roadDurabilty = 1f;
    public float refuseLevel = 0f;

    [Header("Conundrums")]
    [SerializeField] bool startConundrums = false;
    Coroutine ConundrumTimerCoroutine;
    float timePerConundrum = 60f;
    float timePerConundrumVariance = 10f;

    [Header("Fly Cam")]
    [SerializeField] Camera flyCam;

    private void Awake()
    {
        Instance = this;
        spawnManager = GetComponentInChildren<CustomerSpawnManager>();
        tableManager = GetComponentInChildren<TableManager>();
    }
    private IEnumerator Start()
    {
        
        //Enabling Ability to use cam mode
        InputManager.Instance.Inputs.Cheats.Enable();
        InputManager.Instance.Inputs.Cheats.PhotoMode.performed += x => TogglePhotoMode();


        UX_Fade.Instance?.FadeIn();
        yield return new WaitForSeconds(2f);
        TutorialManager.Instance?.StartDialogue();
        yield return new WaitForSeconds(2f);
        startSpawningCustomers = true;

        //Conundrums
        TriggerStartOfRoundConundrums();
        startConundrums = true;
    }

    private void Update()
    {
        if (startConundrums && ConundrumTimerCoroutine == null)
        {
            ConundrumTimerCoroutine = StartCoroutine(ConundrumTimer());
        }

        //End of Game Condition
        if (spawnManager.totalCustomers == 0)
        {
            int fullTables = 0;
            for (int i = 0; i < tableManager.tables.Count; i++)
            {
                if (tableManager.tables[i].currentCustomers > 0)
                fullTables++;
            }

            if (fullTables == 0)
            {
                UX_Fade.Instance.FadeOut("MainMenu");

            }
        }

    }

        

    void TriggerStartOfRoundConundrums()
    {
        //Sewerage, Road Maintenance and Refuse
        float chanceOfSewerage, chanceOfBadRoad, chanceOfRefuse;

        chanceOfSewerage = Random.Range(0, 100);
        chanceOfBadRoad = Random.Range(0, 100);  
        chanceOfRefuse = Random.Range(0, 100);   

        if (chanceOfSewerage > 80)  // 20% Chance
        {
            ConundrumManager.Instance.TriggerConundrum(ConundrumTypes.Sewerage);
        }
        
        if (chanceOfBadRoad > 75)   // 25% Chance
        {
            ConundrumManager.Instance.TriggerConundrum(ConundrumTypes.RoadMaintenance);
        }

        if (chanceOfRefuse > 60)    // 40% Chance
        {
            ConundrumManager.Instance.TriggerConundrum(ConundrumTypes.RefuseRemoval);
        }
    }

    IEnumerator ConundrumTimer()
    {
        float time = Random.Range(timePerConundrum - timePerConundrumVariance, timePerConundrum + timePerConundrumVariance);

        yield return new WaitForSeconds(time);

        //Loadshedding, Water Shortage and Crime
        int randomConundum = Random.Range(0, 3);

        switch(randomConundum)
        {
            case 0: ConundrumManager.Instance.TriggerConundrum(ConundrumTypes.LoadShedding);
                break;
            case 1: ConundrumManager.Instance.TriggerConundrum(ConundrumTypes.WaterShortage);
                break;
            case 2: ConundrumManager.Instance.TriggerConundrum(ConundrumTypes.Crime);
                break;
            default: break;
        }

        ConundrumTimerCoroutine = null;
    }

    void TogglePhotoMode()
    {
        if (flyCam)
        {
            if (flyCam.gameObject.activeSelf == false && InputManager.Instance.Inputs.Player.enabled)
            {
                Debug.Log("Enable Fly Cam");
                flyCam.gameObject.SetActive(true);
                InputManager.Instance.DisablePlayerMovement();
            }
            else if (flyCam.gameObject.activeSelf && InputManager.Instance.Inputs.Player.enabled == false)
            {
                Debug.Log("Disable Fly Cam");
                flyCam.gameObject.SetActive(false);
                InputManager.Instance.EnablePlayerMovement();
            }
        }
    }

}
