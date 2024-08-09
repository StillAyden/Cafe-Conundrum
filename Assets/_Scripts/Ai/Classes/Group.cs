using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    #region Variables

    private int noOfCustomers = 0;
    private List<Customer> customers = new List<Customer>();

    #endregion
    private void Awake()
    {
        //Generate n num between 1 to 4
        noOfCustomers = Random.Range(1, 5);

        //Create each Customer Class
        for (int i = 0; i < noOfCustomers; i++)
        {
            customers.Add(new Customer());
        }
    }

    #region GetSet

    public int GetSize() { return noOfCustomers; }
    public List<Customer> GetCustomers() {  return customers; }

    #endregion
}
