using UnityEngine;

public class CustomerOutSideWalk : MonoBehaviour
{
    [Header("Customer Prefab")]
    [SerializeField] public GameObject customer;

    [Header("Paths Prefab")]
    [SerializeField] public Transform[] customerPath1;
    [SerializeField] public Transform[] customerPath2;
}
