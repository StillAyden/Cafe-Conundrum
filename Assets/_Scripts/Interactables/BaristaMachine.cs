using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaristaMachine : Interactable
{
    [SerializeField] Canvas prepTimer;
    [Space]
    //float waterLevel = 100f;
    [SerializeField] float prepTime = 10f;
    [SerializeField] bool canCollect = false;
    [SerializeField] bool isPreparing = false;

    public GameObject Interact()
    {
        if (!canCollect && !isPreparing) 
        {
            StartCoroutine(StartDrinkPrep());
            return null;
        }
        else if(canCollect && !isPreparing)
        {
            return GameManager.Instance.drink.items[(int)Drink.Coffee].prefab;
        }

        return null;
    }

    IEnumerator StartDrinkPrep()
    {
        isPreparing = true;
        prepTimer.gameObject.SetActive(true);
        yield return new WaitForSeconds(prepTime);
        isPreparing = false;
        prepTimer.gameObject.SetActive(false);

        canCollect = true;
    }
}
