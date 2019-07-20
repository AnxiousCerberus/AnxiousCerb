using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{

    Color defaultBubbleColor;
    Color defaultTextColor;

    public PortraitsManager portraitsData;
    List<Image> portraitDisplay = new List<Image>();

    [HideInInspector]
    public float closeCooldDown = 0;

    Image bubbleImage;
    TMPro.TMP_Text textDisplay;

    bool Visible = true;

    //Choice buttons
    [SerializeField] GameObject ChoiceButton;
    [SerializeField] Vector2 FirstChoiceButtonPosition;
    [SerializeField] float ChoiceButtonsSpace = 10f;
    List<GameObject> choiceButtonsList = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        //TODO : CLEAN THIS PORTRAIT MESS, OH GOD.
        portraitDisplay.Add (transform.Find("UI_SpeechPortrait").GetComponent<Image>());
        portraitDisplay.Add(transform.Find("Polaroid1").GetComponent<Image>());
        portraitDisplay.Add(transform.Find("Polaroid2").GetComponent<Image>());

        bubbleImage = this.GetComponent<Image>();
        textDisplay = this.GetComponentInChildren<TMPro.TMP_Text>();

        defaultBubbleColor = bubbleImage.color;
        defaultTextColor = textDisplay.color;

        foreach (Image image in portraitDisplay)
        {
            image.enabled = false;
        }

        DialogueManager.UIController = this;

        VisibilityOff();

        if (ChoiceButton == null)
            Debug.LogError("DialogueUI Manager is present, but no ChoiceButton prefab is set!");
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

        if (choiceButtonsList.Count > 0)
        {

        }
    }

    public void VisibilityOn()
    {
        Color targetColor = new Color(255, 255, 255, 255);
        bubbleImage.color = defaultBubbleColor;
        textDisplay.color = defaultTextColor;

        foreach (Image image in portraitDisplay)
        {
            image.enabled = true;
        }

        Visible = true;
    }

    public void VisibilityOff()
    {
        Color targetColor = new Color(255, 255, 255, 0);
        bubbleImage.color = targetColor;
        textDisplay.color = targetColor;

        foreach (Image image in portraitDisplay)
        {
            image.enabled = false;
        }

        Visible = false;
    }

    public void PortraitDisplay (string name)
    {
        foreach (PortraitsElements portrait in portraitsData.PortraitList)
        {
            if (name == portrait.portraitName)
            {
                Debug.Log("PORTRAIT MATCH!");
                portraitDisplay[0].sprite = portrait.portraitSprite;
            }
        }
    }

    public void InstantiateChoiceUI (string choiceString)
    {
        Vector3 buttonSpawnPosition = FirstChoiceButtonPosition;

        if (choiceButtonsList.Count > 0)
        {
            buttonSpawnPosition.y = choiceButtonsList[0].transform.position.y - ChoiceButtonsSpace * choiceButtonsList.Count;
        }

        GameObject justSpawnedButton = GameObject.Instantiate(ChoiceButton, buttonSpawnPosition, Quaternion.identity, this.transform);
        justSpawnedButton.GetComponent<Button>().onClick.AddListener(() => ChoiceClicked(choiceButtonsList.IndexOf(justSpawnedButton)));
        choiceButtonsList.Add(justSpawnedButton);

        if (choiceButtonsList.Count == 1)
            justSpawnedButton.GetComponent<Button>().Select();

        justSpawnedButton.transform.name += "_" + choiceButtonsList.Count;
        justSpawnedButton.GetComponentInChildren<TextMeshProUGUI>().text = choiceString;
    }

    public void DestroyAllChoices ()
    {
        foreach (GameObject choiceButton in choiceButtonsList)
        {
            Destroy(choiceButton);
        }

        choiceButtonsList.Clear();
    }

    void ChoiceClicked (int choiceIndex)
    {
        DialogueManager.ChoiceSelect(choiceIndex);
    }
}
