using UnityEngine;

public class TutorialEvents : MonoBehaviour
{
    public static TutorialEvents Instance;

    public bool goToTableTrigger = false;
    public bool goToPOSSystemTrigger = false;
    public bool getCustomerDrinkTrigger = false;
    public bool serveDrinkTrigger = false;
    public bool getOrderTrigger = false;
    public bool deliverOrderTrigger = false;
    public bool finishServingTrigger = false;
    public bool collectTipTrigger = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (TutorialManager.Instance.currentScript.node[TutorialManager.Instance.index].type == TutorialNodeType.Event)
        {
            if (goToTableTrigger && TutorialManager.Instance.index == 4)
            {
                TutorialManager.Instance.NextDialogue();
                goToTableTrigger = false;
            }

            if (goToPOSSystemTrigger && TutorialManager.Instance.index == 7)
            {
                TutorialManager.Instance.NextDialogue();
                goToPOSSystemTrigger = false;
            }

            if (getCustomerDrinkTrigger && TutorialManager.Instance.index == 10)
            {
                TutorialManager.Instance.NextDialogue();
                getCustomerDrinkTrigger = false;
            }

            if (serveDrinkTrigger && TutorialManager.Instance.index == 11)
            {
                TutorialManager.Instance.NextDialogue();
                serveDrinkTrigger = false;
            }
            if (getOrderTrigger && TutorialManager.Instance.index == 13)
            {
                TutorialManager.Instance.NextDialogue();
                getOrderTrigger = false;
            }
            if (deliverOrderTrigger && TutorialManager.Instance.index == 14)
            {
                TutorialManager.Instance.NextDialogue();
                deliverOrderTrigger = false;
            }
            if (finishServingTrigger && TutorialManager.Instance.index == 15)
            {
                TutorialManager.Instance.NextDialogue();
                finishServingTrigger = false;
            }
            if (collectTipTrigger && TutorialManager.Instance.index == 18)
            {
                TutorialManager.Instance.NextDialogue();
                collectTipTrigger = false;
            }
        }
        
    }
}
