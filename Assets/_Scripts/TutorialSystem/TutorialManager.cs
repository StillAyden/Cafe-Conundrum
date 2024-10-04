using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    static TutorialManager Instance;

    [SerializeField] TutorialScript_SO currentScript;

    int index;

    [Header("Dialogue Components")]
    [SerializeField] GameObject pnlDialogue;
    [SerializeField] GameObject pnlTutorialPrompt;
    [SerializeField] GameObject pnlTaskList;

    [Header("Dialogue")]
    [SerializeField] Text txtName;
    [SerializeField] Text txtDialogue;

    [Header("TutorialPrompt")]
    [SerializeField] Image imgTutorial;

    //[Header("Task")]

    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (Instance != this)
        {
            Debug.LogWarning("There are multiple instances of the " + this.name + " class. Please ensure there is only one!");
        }
    }


    public void StartDialogue(TutorialScript_SO _script)
    {
        GetComponent<Canvas>().gameObject.SetActive(true);
        currentScript = _script;
        index = 0;
        
        if (currentScript.node[index].type == TutorialNodeType.Dialogue)
        {
            ShowPanel(TutorialNodeType.Dialogue);

            txtName.text = currentScript.node[index].actorName;
            txtDialogue.text = currentScript.node[index].actorDialogue;
        }
        else if (currentScript.node[index].type == TutorialNodeType.TutorialPanel)
        {
            ShowPanel(TutorialNodeType.TutorialPanel);

            imgTutorial = currentScript.node[index].tutorialImage;
        }
        else if (currentScript.node[index].type == TutorialNodeType.Event)
        {
            ShowPanel(TutorialNodeType.Event);
        }



        ////Update UI
        //if (currentScript.node[index].type == TutorialNodeType.Dialogue)
        //{
        //    pnlDialogue.gameObject.SetActive(true);
        //    imgTutorial.gameObject.SetActive(false);

        //    txtName.text = currentScript.node[index].actorName;
        //    txtDialogue.text = currentScript.node[index].actorDialogue;
        //}
        //else if (currentScript.node[index].type == TutorialNodeType.TutorialPanel)
        //{
        //    //TODO Pause Gameplay
        //    pnlDialogue.gameObject.SetActive(false);
        //    imgTutorial.gameObject.SetActive(true);

        //}
    }

    public void NextDialogue() 
    {
        if (index <= currentScript.node.Length)
        {
            index++;

            //Update UI
            if (currentScript.node[index].type == TutorialNodeType.Dialogue)
            {
                pnlDialogue.gameObject.SetActive(true);
                imgTutorial.gameObject.SetActive(false);

                txtName.text = currentScript.node[index].actorName;
                txtDialogue.text = currentScript.node[index].actorDialogue;
            }
            else if (currentScript.node[index].type == TutorialNodeType.TutorialPanel)
            {
                //TODO Pause Gameplay
                pnlDialogue.gameObject.SetActive(false);
                imgTutorial.gameObject.SetActive(true);

            }
        }
        else
        {
            GetComponent<Canvas>().gameObject.SetActive(false);
            pnlDialogue.gameObject.SetActive(false);
            imgTutorial.gameObject.SetActive(false);
            //TODO Unpause Gameplay
        }

    }

    void ShowPanel(TutorialNodeType type)
    {
        switch (type)
        {
            case TutorialNodeType.Dialogue:
                {
                    pnlDialogue.SetActive(true);
                    pnlTutorialPrompt.SetActive(false);
                    pnlTaskList.SetActive(false);
                    break;
                }
            case TutorialNodeType.TutorialPanel:
                {
                    pnlDialogue.SetActive(false);
                    pnlTutorialPrompt.SetActive(true);
                    pnlTaskList.SetActive(false);
                    break;
                }
            case TutorialNodeType.Event:
                {
                    pnlDialogue.SetActive(false);
                    pnlTutorialPrompt.SetActive(false);
                    pnlTaskList.SetActive(true);
                    break;
                }
        }   
    }
}
