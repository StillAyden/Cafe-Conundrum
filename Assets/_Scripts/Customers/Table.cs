using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : Interactable
{
    #region Variables
    [Header("Table Settings")]
    [SerializeField] private List<GameObject> chairs = new List<GameObject>(); // List of chairs of this table
    [SerializeField] private int maxCapacity = 4; // Maximum number of people the table can hold
    [SerializeField] private int currentCustomers = 0; // Showes the current amount of customers
    [SerializeField] public List<Customer> customers = new List<Customer>(); // List of customers at the table
    [SerializeField] private bool orderTaken = false;

    [Space]
    [Header("Timer Settings")]
    [SerializeField][Range(1, 10)] private float clearTimerDuration = 5f; // Duration for the clear timer
    private Coroutine clearTimerCoroutine;

    [Space]
    [Header("Sprite Settings")]
    [SerializeField] private Image tableSprite;
    [SerializeField] private Sprite takeOrder;
    [SerializeField] private Sprite WaitingForOrder;
    [SerializeField][Range(0f, 10f)] private float SpriteDistance = 2f;    //Distance at which the table sprite is shown
    [SerializeField][Range(0f, 20f)] private float maxSpriteDistance = 4f; //Maximum distance after the sprite is hidden

    private bool showTableSprite = true;
    private Transform playerTransform;

    [Space]
    [Header("Patience Timer Settings")]
    [SerializeField] private Image patienceProgressBar;
    private float patienceTimer;
    private float maxPatienceTimer;

    [Space]
    [Header("Progress Bar Colors")]
    [SerializeField] private Color greenColor = Color.green;
    [SerializeField] private Color orangeColor = new Color(1f, 0.64f, 0f); // Orange color
    [SerializeField] private Color redColor = Color.red;
    [SerializeField][Range(0f, 1f)] private float orangeThreshold = 0.5f; // 50%
    [SerializeField][Range(0f, 1f)] private float redThreshold = 0.2f; // 20%

    [Space]
    [Header("Extra Time Settings")]
    [SerializeField][Range(0f, 10f)] private float extraTimePerSecond = 1f; // Makes last seconds last longer
    [SerializeField][Range(1f, 5f)] private float extraTimeStartThreshold = 2f; // Start extra time when remaining time is less than this

    [Space]
    [Header("Reputation and Currency Penalties")]
    [SerializeField][Range(1f, 10f)] private int reputationPenaltyMin = 1;
    [SerializeField][Range(1f, 10f)] private int reputationPenaltyMax = 5;

    [Space]
    [Header("Reputation and Currency Rewards")]
    [SerializeField][Range(1f, 10f)] private int reputationRewardMin = 1;
    [SerializeField][Range(1f, 10f)] private int reputationRewardMax = 5;
    [SerializeField][Range(1f, 100f)] private int currencyRewardMin = 10;
    [SerializeField][Range(1f, 100f)] private int currencyRewardMax = 50;

    private bool isPatienceTimerRunning = false;
    private TableManager tableManager;

    #endregion

    #region Unity Methods
    private void Start()
    {
        //Script
        tableManager = FindObjectOfType<TableManager>();

        //Chairs 
        InitializeChairs();

        //Sprits
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(CheckDistanceCoroutine());
    }

    private void OnValidate()
    {
        InitializeChairs();
    }

    #endregion

    #region Functions

    public void InitializeChairs() //Add chairs auto to make easier
    {
        chairs.Clear();
        maxCapacity = 0;

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Chair"))
            {
                chairs.Add(child.gameObject);
                maxCapacity++;
            }
        }
    }

    public bool IsFull() // Function to check if the table is full
    {
        return currentCustomers >= 1 ? true : false;
    }

    public bool ClearTable()
    {
        // Check if all customers are done eating
        foreach (Customer cus in customers)
        {
            if (cus != null && (!cus.HasGottenFood || !cus.HasGottenDrink))
            {
                return false; // Return false if any customer is not done eating
            }
        }

        int customerCount = customers.Count;

        // If all customers are done eating
        foreach (Customer cus in customers)
        {
            if (cus != null)
            {
                Destroy(cus.gameObject); // Destroy the customer GameObject
            }
        }

        // Calculate rewards
        int reputationReward = Random.Range(reputationRewardMin, reputationRewardMax + 1) * customerCount;
        int currencyReward = Random.Range(currencyRewardMin, currencyRewardMax + 1) * customerCount;

        // Add rewards
        ReputationManager.Instance.AddReputation(reputationReward);
        CurrencyManager.Instance.AddCurrency(currencyReward);

        // Clear the list of customers & Reset the customer count
        customers.Clear();
        currentCustomers = 0;
        orderTaken = false;

        SetTableSprite(false);
        SetCustomerSprites(false);

        // Stop the patience timer
        isPatienceTimerRunning = false;
        patienceProgressBar.gameObject.SetActive(false);

        return true; // Return true if the table was cleared
    }

    public void ForceClearTable()
    {
        int customerCount = customers.Count;

        foreach (Customer cus in customers)
        {
            if (cus != null)
            {
                Destroy(cus.gameObject); // Destroy the customer GameObject
            }
        }

        // Calculate penalties
        int reputationPenalty = Random.Range(reputationPenaltyMin, reputationPenaltyMax + 1) * customerCount;

        // Apply penalties
        ReputationManager.Instance.RemoveReputation(reputationPenalty);


        // Clear the list of customers & Reset the customer count
        customers.Clear();
        currentCustomers = 0;
        orderTaken = false;

        SetTableSprite(false);
        SetCustomerSprites(false);

        // Stop the patience timer
        isPatienceTimerRunning = false;
        patienceProgressBar.gameObject.SetActive(false);

    }

    public void AddCustomers(GameObject customerPrefab, int numberOfCustomers)
    {
        for (int i = 0; i < numberOfCustomers && currentCustomers < maxCapacity; i++)
        {
            //Get chair
            GameObject chair = chairs[currentCustomers];

            //Spawn customer at chair
            GameObject obj = Instantiate(customerPrefab, chair.transform.position, Quaternion.identity, this.transform);

            //Add customer to customers List
            Customer cus = obj.GetComponent<Customer>();
            customers.Add(cus);

            //Add C=table to customer.cs var's
            cus.SetTable(this.gameObject.GetComponent<Table>());

            //Increase num
            currentCustomers++;
        }

    }

    public void TableClearTimer()
    {
        foreach (Customer cus in customers) //Will break loop if one customer hasGottenFood
        {
            if (cus != null && (!cus.HasGottenFood || !cus.HasGottenDrink))
            {
                return;
            }
        }


        //If loop is done, that means all cus has food
        if (clearTimerCoroutine != null)
        {
            StopCoroutine(clearTimerCoroutine); // Stop any previous timer
        }
        clearTimerCoroutine = StartCoroutine(ClearTableAfterTime(clearTimerDuration));
    }

    private void StartPatienceTimer()
    {
        // Get random patience time from TableManager
        float minTime = tableManager.GetMinPatienceTime();
        float maxTime = tableManager.GetMaxPatienceTime();
        maxPatienceTimer = Random.Range(minTime, maxTime);
        patienceTimer = maxPatienceTimer;
        isPatienceTimerRunning = true;
        patienceProgressBar.gameObject.SetActive(true); // Show progress bar
        StartCoroutine(PatienceTimerCoroutine());
    }

    private void UpdateProgressBar()
    {
        float fillAmount = patienceTimer / maxPatienceTimer;
        patienceProgressBar.fillAmount = fillAmount;

        // Change color based on thresholds
        if (fillAmount > orangeThreshold)
        {
            patienceProgressBar.color = greenColor;
        }
        else if (fillAmount > redThreshold)
        {
            patienceProgressBar.color = orangeColor;
        }
        else
        {
            patienceProgressBar.color = redColor;
        }
    }

    private IEnumerator ClearTableAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        ClearTable();
    }

    private IEnumerator PatienceTimerCoroutine()
    {
        while (isPatienceTimerRunning && patienceTimer > 0f)
        {
            UpdateProgressBar();

            // Check if we need to apply extra time
            if (patienceTimer <= extraTimeStartThreshold)
            {
                patienceTimer -= Time.deltaTime / extraTimePerSecond;
            }
            else
            {
                patienceTimer -= Time.deltaTime;
            }

            yield return null;
        }

        if (patienceTimer <= 0f)
        {
            // Timer ran out
            isPatienceTimerRunning = false;
            ForceClearTable();

            // Apply reputation penalty
            int penalty = Random.Range(reputationPenaltyMin, reputationPenaltyMax + 1);
            ReputationManager.Instance.RemoveReputation(penalty);
        }
    }

    #endregion

    #region Sprites

    private IEnumerator CheckDistanceCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            float distance = Vector3.Distance(transform.position, playerTransform.position);

            if (distance > maxSpriteDistance)
            {
                // Player is too far; hide all sprites
                SetTableSprite(false);
                SetCustomerSprites(false);
                patienceProgressBar.gameObject.SetActive(false);
            }
            else if (!orderTaken && currentCustomers > 0)
            {
                // Order has not been taken, show "Take Order" icon
                SetTableSprite(true);
                tableSprite.sprite = takeOrder; // Set to "Take Order" sprite
                SetCustomerSprites(false); // Hide customer sprites
                patienceProgressBar.gameObject.SetActive(false);
            }
            else if (orderTaken && distance <= SpriteDistance)
            {
                // Order is taken and player is within spriteDistance; show customer sprites
                SetTableSprite(false); // Hide table sprite
                SetCustomerSprites(true); // Show customer sprites
                patienceProgressBar.gameObject.SetActive(true);
            }
            else if (orderTaken && distance > SpriteDistance)
            {
                // Order is taken but player is outside spriteDistance; show "Waiting for Order" icon
                SetTableSprite(true);
                tableSprite.sprite = WaitingForOrder; // Set to "Waiting for Order" sprite
                SetCustomerSprites(false); // Hide customer sprites
                patienceProgressBar.gameObject.SetActive(true);
            }
        }
    }

    private void SetTableSprite(bool show)
    {
        showTableSprite = show;
        tableSprite.gameObject.SetActive(show);
    }

    private void SetCustomerSprites(bool show)
    {
        foreach (Customer customer in customers)
        {
            if (customer != null)
            {
                customer.SetShowFoodSprite(show);
                customer.SetShowDrinkSprite(show);
            }
        }
    }

    #endregion

    #region GetSet

    public int GetMaxCapacity() { return maxCapacity; }

    public void SetSpriteDistance(float dis) { SpriteDistance = dis; }

    public bool IsOrderTaken
    {
        get { return orderTaken; }
        set
        {
            orderTaken = value;
            tableSprite.sprite = orderTaken ? WaitingForOrder : takeOrder; // Change sprite based on order status
            SetCustomerSprites(orderTaken); // Update customer sprites visibility }
            StartPatienceTimer();
        }
    }
    #endregion
}
