using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{

    Color defaultBubbleColor;
    Color defaultTextColor;

    public PortraitsManager portraitsData;
    Image portraitDisplay;

    [HideInInspector]
    public float closeCooldDown = 0;

    Image bubbleImage;
    TMPro.TMP_Text textDisplay;

    bool Visible = true;

    // Start is called before the first frame update
    void Awake()
    {
        portraitDisplay = transform.Find("UI_SpeechPortrait").GetComponent<Image>();
        bubbleImage = this.GetComponent<Image>();
        textDisplay = this.GetComponentInChildren<TMPro.TMP_Text>();

        defaultBubbleColor = bubbleImage.color;
        defaultTextColor = textDisplay.color;
        portraitDisplay.enabled = false;

        DialogueManager.UIController = this;

        VisibilityOff();
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
    }

    public void VisibilityOn()
    {
        Color targetColor = new Color(255, 255, 255, 255);
        bubbleImage.color = defaultBubbleColor;
        textDisplay.color = defaultTextColor;

        portraitDisplay.enabled = true;

        Visible = true;
    }

    public void VisibilityOff()
    {
        Color targetColor = new Color(255, 255, 255, 0);
        bubbleImage.color = targetColor;
        textDisplay.color = targetColor;

        portraitDisplay.enabled = false;

        Visible = false;
    }

    public void PortraitDisplay (string name)
    {
        foreach (PortraitsElements portrait in portraitsData.PortraitList)
        {
            if (name == portrait.portraitName)
            {
                Debug.Log("PORTRAIT MATCH!");
                portraitDisplay.sprite = portrait.portraitSprite;
            }
        }
    }
}
