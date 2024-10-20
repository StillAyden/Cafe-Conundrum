using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationManager : MonoBehaviour
{
    #region Variables
    //Singleton
    public static ReputationManager Instance;

    //Currency
    [Header("Currency")]
    [SerializeField][Range(0, 100)] private float reputation = 100;

    // Decrease Variables
    [Header("Reputation Decrease Settings")]
    [Tooltip("Time interval in seconds")]
    [SerializeField][Range(0.1f, 10f)] private float decreaseInterval = 1f;

    [Tooltip("Amount to decrease per interval")]
    [SerializeField][Range(0.1f, 10f)] private float decreaseAmount = 1f;

    [Tooltip("Overall Multiplier")]
    [SerializeField][Range(1f, 10f)] private float decreaseMultiplier = 1f;

    private bool startDecreasing = false;
    private Coroutine decreaseRoutine = null;

    #endregion

    private void Awake()
    {
        Singleton();
    }

    #region Functions

    public void StartDecreasingReputation(bool value)
    {
        if (value)
        {
            startDecreasing = true;
            decreaseRoutine = StartCoroutine(DecreaseReputationOverTime());
        }
        else
        {
            startDecreasing = false;
            StopCoroutine(decreaseRoutine);
        }
    }

    private IEnumerator DecreaseReputationOverTime()
    {
        while (startDecreasing)
        {
            yield return new WaitForSeconds(decreaseInterval / decreaseMultiplier);
            float currentDecreaseAmount = decreaseAmount;

            if (reputation <= 5)
            {
                currentDecreaseAmount /= 2; // Halve the decrease amount when reputation smaller or = 5
            }

            RemoveReputation(currentDecreaseAmount);

            if (reputation <= 0) // Make sure that the reputation does not go below 0
            {
                reputation = 0;
                startDecreasing = false;
            }
        }
    }

    #endregion

    #region Get & Set & Add
    //Followers
    public float GetReputation() { return reputation; }
    public void SetReputation(float value) { reputation = value; }
    public void AddReputation(float reputationAdded)
    {
        reputation += reputationAdded;

        reputation = Mathf.Clamp(reputation, 0f, 100f);
    }
    public void RemoveReputation(float reputationRemoved)
    {
        reputation -= reputationRemoved;

        reputation = Mathf.Clamp(reputation, 0f, 100f);
    }
    #endregion

    #region Singleton

    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            Debug.LogWarning("Another instance of ReputationManager was destroyed on creation!");
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    #endregion
}
