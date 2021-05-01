using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class TagParser
{
    static DialogueUI dialogueUI;
    static ContextChoicesUI contextChoicesUI;

    static public void GetTag(string tag)
    {
        //TODO : C'est opti ça ???
        dialogueUI = DialogueUI.FindObjectOfType<DialogueUI>();


        //TODO : A terme, un switch ça vaudra sans doute mieux
        if(tag.Contains("FOCUS"))
            FocusCamera(tag);
        else if (tag.Contains("MOVE"))
            Move(tag);
        else if (tag.Contains("CHOICEACTIONS"))
            ChoiceActions(tag);
    }

    static void FocusCamera (string tag)
    {
        string[] splitted = tag.Split(':');
        string targetName = splitted[1];

        dialogueUI.DialogueCamera.GetComponent<CameraFocus>().target = GameObject.Find(targetName).transform.GetChild(0).transform;
    }

    static void Move (string tag)
    {
        string[] splitted = tag.Split('>');
        string characterName = splitted[0];
        string moveTargetName = splitted[1];

        //Retrieve Character
        characterName = characterName.Remove(0, 5);
        characterName = characterName.Trim();
        GameObject character = null;

        if (characterName == "Ananda")
        {
            character = GameObject.FindGameObjectWithTag("Player");
        }
        //TODO : Really not sure if this condition is working...
        else
        {
            StateTracker stateTracker = GameObject.FindObjectOfType<StateTracker>();

            for (int i = 0; i < stateTracker.NPCs.Length; i++)
            {
                if (stateTracker.NPCs[i].inkName == characterName)
                {
                    character = stateTracker.NPCs[i].gameObject;
                    break;
                }
            }
        }

        //Retrieve PathNode
        GameObject pathNode;
        moveTargetName = moveTargetName.Trim();

        pathNode = GameObject.Find(moveTargetName);

        //TODO : JUST TESTING
        character.transform.position = pathNode.transform.position;
    }
    static void ChoiceActions (string tag)
    {
        if (contextChoicesUI == null)
        {
            contextChoicesUI = GameObject.FindObjectOfType<ContextChoicesUI>();
        }

        ActionChoiceBase ActionList = contextChoicesUI.actionChoiceList.actionChoices[0].prefab as ActionChoiceBase;
        contextChoicesUI.populateChoices(ActionList);
        //TODO : THIS IS OF COURSE A TEST, PLEASE MAKE IT INSTANTIATE THE ACTION LIST DEMANDED.
    }
}
