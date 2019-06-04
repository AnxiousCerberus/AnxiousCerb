using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro.Examples;
using TMPro;

static class DialogueManager
{

    static public RollingTextFade fadeController;
    static public DialogueUI UIController;

    static public bool DialogueInProgress = false;

    static Story story;

    // Update is called once per frame
    static public void DialogueStart(TextAsset inkJSON)
    {
        story = new Story(inkJSON.text);
        DialogueInProgress = true;
        UIController.VisibilityOn();

        DialogueContinue();
    }

    static public void DialogueContinue()
    {
        if (fadeController.ready)
        {
            if (story.canContinue)
            {
                fadeController.GetComponent<TextMeshProUGUI>().text = story.Continue().Trim();
                fadeController.StartCoroutine(fadeController.AnimateVertexColors());
            }
            else
                DialogueEnd();
        }
    }

    static public void DialogueEnd()
    {
        Debug.Log("DIALOGUEMANAGER => Closing Dialogue");
        UIController.VisibilityOff();
    }
}
