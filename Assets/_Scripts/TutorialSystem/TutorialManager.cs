using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    static TutorialManager Instance;

    [SerializeField] TutorialScript_SO[] allScripts;
    [Space]
    [SerializeField] TutorialScript_SO currentScript;

    [Header("Dialogue System")]
    [SerializeField] GameObject pnlDialogue;
    [SerializeField] Text txtName;
    [SerializeField] Text txtDialogue;
    int index;

    [Header("Tutorial System")]
    [SerializeField] Image imgTutorial;

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

    //Method to start a dialogue
    public void StartDialogue(TutorialScript_SO _script)
    {
        GetComponent<Canvas>().gameObject.SetActive(true);
        currentScript = _script;
        index = 0;

        //Update UI
        if (currentScript.tutorialNodes[index].type == TutorialNodeType.Dialogue)
        {
            pnlDialogue.gameObject.SetActive(true);
            imgTutorial.gameObject.SetActive(false);

            txtName.text = currentScript.tutorialNodes[index].actorName;
            txtDialogue.text = currentScript.tutorialNodes[index].actorDialogue;
        }
        else if (currentScript.tutorialNodes[index].type == TutorialNodeType.TutorialPanel)
        {
            //TODO Pause Gameplay
            pnlDialogue.gameObject.SetActive(false);
            imgTutorial.gameObject.SetActive(true);

        }
    }

    public void NextDialogue() 
    {
        if (index <= currentScript.tutorialNodes.Length)
        {
            index++;

            //Update UI
            if (currentScript.tutorialNodes[index].type == TutorialNodeType.Dialogue)
            {
                pnlDialogue.gameObject.SetActive(true);
                imgTutorial.gameObject.SetActive(false);

                txtName.text = currentScript.tutorialNodes[index].actorName;
                txtDialogue.text = currentScript.tutorialNodes[index].actorDialogue;
            }
            else if (currentScript.tutorialNodes[index].type == TutorialNodeType.TutorialPanel)
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
}
