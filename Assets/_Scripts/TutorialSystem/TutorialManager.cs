using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    static TutorialManager Instance;

    [SerializeField] TutorialScript_SO currentScript;

    [SerializeField] int index;

    [Header("Dialogue Components")]
    [SerializeField] GameObject pnlDialogue;
    [SerializeField] GameObject pnlTutorialPrompt;
    [SerializeField] GameObject pnlTaskList;

    [Header("Dialogue")]
    [SerializeField] Text txtName;
    [SerializeField] Text txtDialogue;

    [Header("TutorialPrompt")]
    [SerializeField] Text txtTask;
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

        StartDialogue(currentScript);

    }


    public void StartDialogue(TutorialScript_SO _script)
    {
        EnableUIInput();
        InputManager.Instance.Selected += NextDialogue;

        GetComponent<Canvas>().gameObject.SetActive(true);
        currentScript = _script;
        index = 0;
        
        if (currentScript.node[index].type == TutorialNodeType.Dialogue)
        {
            EnableUIInput();
            //TODO: Pause Gameplay
            ShowPanel(TutorialNodeType.Dialogue);
            UpdateTutorialNode(TutorialNodeType.Dialogue);
        }
        else if (currentScript.node[index].type == TutorialNodeType.TutorialPanel)
        {
            EnableUIInput();
            //TODO: Pause Gameplay
            ShowPanel(TutorialNodeType.TutorialPanel);
            UpdateTutorialNode(TutorialNodeType.Dialogue);
        }
        else if (currentScript.node[index].type == TutorialNodeType.Event)
        {
            DisableUIInput();
            //TODO: Unpause Game Here
            ShowPanel(TutorialNodeType.Event);
            UpdateTutorialNode(TutorialNodeType.Dialogue);
        }
    }

    public void NextDialogue() 
    {
        if (index < currentScript.node.Length - 1)
        {
            index++;
            //Update UI
            if (currentScript.node[index].type == TutorialNodeType.Dialogue)
            {
                EnableUIInput();
                //TODO: Pause Gameplay
                ShowPanel(TutorialNodeType.Dialogue);
                UpdateTutorialNode(TutorialNodeType.Dialogue);
            }
            else if (currentScript.node[index].type == TutorialNodeType.TutorialPanel)
            {
                EnableUIInput();
                //TODO: Pause Gameplay
                ShowPanel(TutorialNodeType.TutorialPanel);
                UpdateTutorialNode(TutorialNodeType.Dialogue);
            }
            else if (currentScript.node[index].type == TutorialNodeType.Event)
            {
                DisableUIInput();               //Call NextDialogue() when a task is complete
                //TODO: Unpause Game Here
                ShowPanel(TutorialNodeType.Event);
                UpdateTutorialNode(TutorialNodeType.Dialogue);
            }
        }
        else
        {
            GetComponent<Canvas>().gameObject.SetActive(false);
            ShowPanel(TutorialNodeType.None);

            //TODO Unpause Gameplay
            InputManager.Instance.Selected -= NextDialogue;
            DisableUIInput();
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
            default:
                {
                    pnlDialogue.SetActive(false);
                    pnlTutorialPrompt.SetActive(false);
                    pnlTaskList.SetActive(false);
                    break;
                }
        }   
    }

    void UpdateTutorialNode(TutorialNodeType _type)
    {
        if (_type == TutorialNodeType.Dialogue)
        {
            txtName.text = currentScript.node[index].actorName;
            txtDialogue.text = currentScript.node[index].actorDialogue;
        }
        else if (_type == TutorialNodeType.TutorialPanel)
        {
            imgTutorial.sprite = currentScript.node[index].tutorialImage.sprite;
        }
        else if (_type == TutorialNodeType.Event)
        {
            txtTask.text = currentScript.node[index].taskText;
        }
        else Debug.LogWarning("Tutorial Node Type not found");
    }

    void EnableUIInput()
    {
        InputManager.Instance.Inputs.Player.Disable();
        InputManager.Instance.Inputs.UI.Enable();
        //InputManager.Instance.Selected += NextDialogue;
    }

    void DisableUIInput()
    {
        //InputManager.Instance.Selected -= NextDialogue;
        InputManager.Instance.Inputs.UI.Disable();
        InputManager.Instance.Inputs.Player.Enable();
    }
}
