using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro.Examples;
using TMPro;
using System.ComponentModel;
using System;

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
        Debug.Log("DIALOGUEMANAGER => Starting Dialogue");
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
            if (story.canContinue && story.currentChoices.Count <= 0)
            {
                string currentLine = story.Continue().Trim();

                //Parsing if any tag is currently active
                foreach (string tag in story.currentTags)
                {
                    TagDetection(tag);
                }


                //TODO : Call a text animation function here instead of just displaying the text as-is
                UIController.textDisplay.text = currentLine;
            }
            else if (story.currentChoices.Count > 0)
            {
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

    static public void DialogueEnd()
    {
        //Debug.Log("AFTER talkedOnce = " + story.variablesState["talkedOnce"]);
        UIController.VisibilityOff();
        Debug.Log("DIALOGUEMANAGER => Closing Dialogue");
        //DialogueInProgress is set to false in DialogueUI, in order to have a little cool down period.
    }

    static public void TagDetection (string tag)
    {
        Debug.Log("Current tags = " + tag);

        if (tag.Contains("POS"))
        {
            //Cutting some extra characters to only keep the speaker's name
            string[] splitTag = tag.Split(':');
            int posNumber;
            Int32.TryParse (splitTag[0].Remove(0,3), out posNumber);

            string SpeakerName = tag.Split(':')[1];
            SpeakerName = SpeakerName.Trim();
            Debug.Log("Detected a Speaker! It's: " + SpeakerName + " and they will be displayed at pos " + posNumber);

            UIController.PortraitDisplay(SpeakerName, posNumber);
        }
        else if (tag.Contains("SPEAKER"))
        {
            string SpeakerName = tag.Split(':')[1];
            SpeakerName = SpeakerName.Trim();

            if (SpeakerName == "none" | SpeakerName == "None")
            {
                UIController.SpeakerNameDisplay.text = " ";
            }
            else
                UIController.SpeakerNameDisplay.text = SpeakerName;
        }
        else
        {
            Debug.Log("Encountered Tag = " + tag);
            TagParser.GetTag(tag);
        }
    }

    static public void ChoiceSelect (int choiceIndex)
    {
        Debug.Log("Dialogue choice made. No= " + choiceIndex);
        story.ChooseChoiceIndex(choiceIndex);
        DialogueContinue();

        UIController.ClearAllChoices();

        displayingChoices = false;
    }
}
