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
    static public bool displayingChoices = false;

    static Story story = null;

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
                //Debug.Log("BEFORE talkedOnce = " + story.variablesState["talkedOnce"]);

                fadeController.GetComponent<TextMeshProUGUI>().text = story.Continue().Trim();
                Debug.Log("DIALOGUEMANAGER => Story continues => " + story.currentText);
                fadeController.StartCoroutine(fadeController.AnimateVertexColors());
                //Debug.Log("Path visited = " + story.state.VisitCountAtPathString("TEST_SUBSCENE.Talk_first"));

                //Parsing if current text is attached to a speaker
                foreach (string tag in story.currentTags)
                {
                    SpeakerDetection(tag);
                }
            }
            else if (story.currentChoices.Count > 0)
            {

                Debug.Log(story.currentChoices.Count + " choice(s) exists.");
                int choiceNumber = 0;

                foreach (Choice choice in story.currentChoices)
                {
                    choiceNumber++;

                    UIController.InstantiateChoiceUI(choice.text);
                }

                displayingChoices = true; //Prevent from spamming "Interact" input
            }
            else
                DialogueEnd();
        }
        else
            Debug.Log("Fade Controller not ready");
    }

    static public void DialogueEnd()
    {
        //Debug.Log("AFTER talkedOnce = " + story.variablesState["talkedOnce"]);
        UIController.VisibilityOff();
        Debug.Log("DIALOGUEMANAGER => Closing Dialogue");
        //DialogueInProgress is set to false in DialogueUI, in order to have a little cool down period.
    }

    static public void SpeakerDetection (string tag)
    {
        Debug.Log("Current tags = " + tag);

        if (tag.Contains("Speaker:"))
        {
            //Cutting some extra characters to only keep the speaker's name
            string SpeakerName = tag.Remove(0,8);
            SpeakerName = SpeakerName.Trim();
            Debug.Log("Detected a Speaker! It's:" + SpeakerName);

            UIController.PortraitDisplay(SpeakerName);
        }
    }

    static public void ChoiceSelect (int choiceIndex)
    {
        Debug.Log("Dialogue choice made. No= " + choiceIndex);
        story.ChooseChoiceIndex(choiceIndex);
        DialogueContinue();

        UIController.DestroyAllChoices();

        displayingChoices = false;
    }
}
