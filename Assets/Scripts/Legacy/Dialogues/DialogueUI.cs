using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{

    [SerializeField] TextAsset test_dialogueJSON = null;

    Color defaultBubbleColor;
    public Color defaultTextColor;

    public PortraitsManager portraitsData;
    GameObject ActorParent; 

    [HideInInspector]
    public float closeCooldDown = 0;

    Image bubbleImage;
    TMPro.TMP_Text textDisplay;

    bool Visible = true;

    //Choice buttons
    [SerializeField] GameObject ChoiceButtons;
    [SerializeField] float ChoiceButtonsSpace = 10f;
    List<GameObject> choiceButtonsList = new List<GameObject>();
    int currentChoiceCount;

    float LineHeight = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        //bubbleImage = this.GetComponent<Image>();
        textDisplay = this.GetComponentInChildren<TMPro.TMP_Text>();

        //defaultBubbleColor = bubbleImage.color;
        //defaultTextColor = textDisplay.color;

        ActorParent = GameObject.Find("Actors");

        DialogueManager.UIController = this;

        if (portraitsData == null)
            UnityEngine.Debug.LogError("WARNING! " + transform.name + " is missing a Portraits Manager scripted object! Please check this gameObject.", this.gameObject);

        VisibilityOff();
    }

    private void Start()
    {
        //DEBUG ONLY!
        if (test_dialogueJSON != null)
            DialogueManager.DialogueStart(test_dialogueJSON);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Visible && DialogueManager.DialogueInProgress)
        {
            closeCooldDown += Time.deltaTime;

            if (closeCooldDown > 1f)
            {
                DialogueManager.DialogueInProgress = false;
                closeCooldDown = 0f;
            }
        }

        //Dialogue Stuff
        if (DialogueManager.DialogueInProgress && !DialogueManager.displayingChoices && Input.GetButtonDown("Interact"))
        {
            DialogueManager.DialogueContinue();
        }

        if (choiceButtonsList.Count > 0)
        {

        }
    }

    public void VisibilityOn()
    {
        Color targetColor = new Color(255, 255, 255, 255);
        //bubbleImage.color = defaultBubbleColor;
        //textDisplay.color = defaultTextColor;

        Visible = true;
        
    }

    public void VisibilityOff()
    {
        Color targetColor = new Color(255, 255, 255, 0);
        //bubbleImage.color = targetColor;
        //textDisplay.color = targetColor;

        ClearAllChoices();

        Visible = false;
    }

    public void PortraitDisplay (string name, int pos)
    {
        if (string.Equals(name, "none", System.StringComparison.OrdinalIgnoreCase))
            ActorParent.transform.GetChild(pos).GetComponent<SpriteRenderer>().sprite = null;
        else
        {
            foreach (PortraitsElements portrait in portraitsData.PortraitList)
            {
                if (name == portrait.portraitName)
                {
                    Debug.Log("PORTRAIT MATCH!");
                    ActorParent.transform.GetChild(pos).GetComponent<SpriteRenderer>().sprite = portrait.portraitSprite;
                }
            }
        }
    }

    public void InstantiateChoiceUI (string choiceString)
    {
        //"Instantiating" is more of a "populating" choices as we retrieve the current choices objects and put the texts displayed in place
        Transform currentChoice;
        currentChoice = ChoiceButtons.transform.GetChild(currentChoiceCount);
        currentChoice.gameObject.SetActive(true);

        currentChoice.GetComponent<Button>().onClick.AddListener(() => ChoiceClicked(currentChoice.transform.GetSiblingIndex()));
        currentChoice.GetComponentInChildren<TextMeshProUGUI>().text = choiceString;

        if (currentChoiceCount == 0)
            currentChoice.GetComponent<Button>().Select();

        currentChoiceCount++;
    }

    public void ClearAllChoices ()
    {
        //This does NOT destroy objects, it just deactivates listeners and disable objects, then reset choice count
        foreach (Transform choice in ChoiceButtons.transform)
        {
            choice.GetComponent<Button>().onClick.RemoveAllListeners();
            choice.gameObject.SetActive(false);
        }

        currentChoiceCount = 0;
        choiceButtonsList.Clear();
    }

    void ChoiceClicked (int choiceIndex)
    {
        DialogueManager.ChoiceSelect(choiceIndex);
    }
}
