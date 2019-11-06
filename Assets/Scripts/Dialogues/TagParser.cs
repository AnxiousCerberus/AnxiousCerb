using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class TagParser
{

    static DialogueUI dialogueUI;

    static public void GetTag(string tag)
    {
        //TODO : C'est opti ça ???
        dialogueUI = DialogueUI.FindObjectOfType<DialogueUI>();

        if (tag.Contains("MOVE"))
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
    }
}
