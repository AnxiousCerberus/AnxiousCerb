using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{

    Color defaultBubbleColor;
    Color defaultTextColor;

    Image bubbleImage;
    TMPro.TMP_Text textDisplay;

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
        
    }

    public void VisibilityOn()
    {
        Color targetColor = new Color(255, 255, 255, 255);
        bubbleImage.color = defaultBubbleColor;
        textDisplay.color = defaultTextColor;
    }

    public void VisibilityOff()
    {
        Color targetColor = new Color(255, 255, 255, 0);
        bubbleImage.color = targetColor;

        textDisplay.color = targetColor;
    }
}
