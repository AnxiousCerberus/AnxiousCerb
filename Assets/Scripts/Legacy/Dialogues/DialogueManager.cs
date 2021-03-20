﻿using System.Collections;
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
    //static public TagParser tagParser;
    static public DialogueUI UIController;
    static public Transform currentSpeaker;
    static public bool DialogueInProgress = false;
    static public bool displayingChoices = false;

    static Story story = null;

    static public void DialogueStart(TextAsset inkJSON, string subScene)
    {
        if (fadeController == null)
            fadeController = GameObject.FindObjectOfType<RollingTextFade>().GetComponent<RollingTextFade>();

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
        if (fadeController == null)
            fadeController = GameObject.FindObjectOfType<RollingTextFade>().GetComponent<RollingTextFade>();

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
            if (story.canContinue && story.currentChoices.Count <= 0)
            {
                string currentLine = story.Continue().Trim();

                //Parsing if any tag is currently active
                foreach (string tag in story.currentTags)
                {
                    TagDetection(tag);
                }

                if (currentLine.Length > 0) //Make sure the line contains at least one char, else the fadeController will get stuck
                {
                    fadeController.GetComponent<TextMeshProUGUI>().text = currentLine;
                    fadeController.StartCoroutine(fadeController.AnimateVertexColors());
                }
                DialogueContinue(); //If no char is to be found, let's proceed immediately to next line without the player's input
            }
            else if (story.currentChoices.Count > 0)
            {
                int choiceNumber = 0;

                foreach (Choice choice in story.currentChoices)
                {
                    choiceNumber++;

                    UIController.InstantiateChoiceUI(choice.text);
                }

                fadeController.GetComponent<TextMeshProUGUI>().text = ""; //Stop displaying text for now
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

    static public void TagDetection (string tag)
    {
        Debug.Log("Current tags = " + tag);

        if (tag.Contains("Pos"))
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