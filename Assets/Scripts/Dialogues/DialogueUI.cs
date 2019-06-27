using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{

    Color defaultBubbleColor;
    Color defaultTextColor;

    public PortraitsManager portraits;
    public float closeCooldDown = 0;

    Image bubbleImage;
    TMPro.TMP_Text textDisplay;

    bool Visible = true;

    // Start is called before the first frame update
    void Awake()
    {
        bubbleImage = this.GetComponent<Image>();
        textDisplay = this.GetComponentInChildren<TMPro.TMP_Text>();

        defaultBubbleColor = bubbleImage.color;
        defaultTextColor = textDisplay.color;

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

        Visible = true;
    }

    public void VisibilityOff()
    {
        Color targetColor = new Color(255, 255, 255, 0);
        bubbleImage.color = targetColor;

        textDisplay.color = targetColor;

        Visible = false;
    }
}
