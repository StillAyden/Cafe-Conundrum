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

    #endregion

    #region Unity Methods
    private void Start()
    {
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
            if (cus != null && !cus.HasGottenFood)
            {
                return false; // Return false if any customer is not done eating
            }
        }

        // If all customers are done eating
        foreach (Customer cus in customers)
        {
            if (cus != null)
            {
                Destroy(cus.gameObject); // Destroy the customer GameObject
            }
        }

        // Clear the list of customers & Reset the customer count
        customers.Clear();
        currentCustomers = 0;
        orderTaken = false;

        SetTableSprite(false);
        SetCustomerSprites(false);

        return true; // Return true if the table was cleared
    }

    public void ForceClearTable()
    {
        foreach (Customer cus in customers)
        {
            if (cus != null)
            {
                Destroy(cus.gameObject); // Destroy the customer GameObject
            }
        }

        // Clear the list of customers & Reset the customer count
        customers.Clear();
        currentCustomers = 0;
        orderTaken = false;

        SetTableSprite(false);
        SetCustomerSprites(false);
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
            if (cus != null && !cus.HasGottenFood)
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

    private IEnumerator ClearTableAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);

        ClearTable();
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
            }
            else if (!orderTaken && currentCustomers > 0)
            {
                // Order has not been taken, show "Take Order" icon
                SetTableSprite(true);
                tableSprite.sprite = takeOrder; // Set to "Take Order" sprite
                SetCustomerSprites(false); // Hide customer sprites
            }
            else if (orderTaken && distance <= SpriteDistance)
            {
                // Order is taken and player is within spriteDistance; show customer sprites
                SetTableSprite(false); // Hide table sprite
                SetCustomerSprites(true); // Show customer sprites
            }
            else if (orderTaken && distance > SpriteDistance)
            {
                // Order is taken but player is outside spriteDistance; show "Waiting for Order" icon
                SetTableSprite(true);
                tableSprite.sprite = WaitingForOrder; // Set to "Waiting for Order" sprite
                SetCustomerSprites(false); // Hide customer sprites
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
                customer.SetShowSprite(show);
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
        }
    }
    #endregion
}
