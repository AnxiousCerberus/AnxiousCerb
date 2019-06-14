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
    static public Transform currentSpeaker;
    static public bool DialogueInProgress = false;

    static Story story = null;

    // Update is called once per frame
    static public void DialogueStart(TextAsset inkJSON, string subScene)
    {
        //Debug.Log("DIALOGUEMANAGER => Starting Dialogue");
        if (story == null)
            story = new Story(inkJSON.text);

        DialogueInProgress = true;
        UIController.VisibilityOn();

        story.ChoosePathString(subScene);

        DialogueContinue();
    }

    static public void DialogueStart(TextAsset inkJSON)
    {
        Debug.Log("DIALOGUEMANAGER => Starting Dialogue");
        if (story == null)
            story = new Story(inkJSON.text);

        DialogueInProgress = true;
        UIController.VisibilityOn();

        DialogueContinue();
    }

    static public void DialogueContinue()
    {
        if (fadeController.ready)
        {
            //Debug.Log("DIALOGUEMANAGER => Fade Controller is ready");
            if (story.canContinue)
            {
                Debug.Log("BEFORE talkedOnce = " + story.variablesState["talkedOnce"]);
                //Debug.Log("DIALOGUEMANAGER => Story continues");
                fadeController.GetComponent<TextMeshProUGUI>().text = story.Continue().Trim();
                fadeController.StartCoroutine(fadeController.AnimateVertexColors());
                //Debug.Log("Path visited = " + story.state.VisitCountAtPathString("TEST_SUBSCENE.Talk_first"));

                //Parsing if current text is attached to a speaker
                foreach (string tag in story.currentTags)
                {
                    Debug.Log("Current tags = " + tag);

                    if (tag.Contains("Speaker:"))
                        Debug.Log("Detected a Speaker!");
                }
            }
            else
                DialogueEnd();
        }
    }

    static public void DialogueEnd()
    {
        Debug.Log("AFTER talkedOnce = " + story.variablesState["talkedOnce"]);
        UIController.VisibilityOff();
        //Debug.Log("DIALOGUEMANAGER => Closing Dialogue");
        //DialogueInProgress is set to false in DialogueUI, in order to have a little cool down period.
    }
}
