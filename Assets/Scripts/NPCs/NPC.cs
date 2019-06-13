using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    bool playerNear = false;
    public bool PlayerCanTriggerTalk = true;
    [SerializeField] TextAsset dialogueJSON = null;
    public string SubScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNear & Input.GetButtonDown("Interact") & !DialogueManager.DialogueInProgress)
        {
            if (SubScene != null && SubScene != "")
                DialogueManager.DialogueStart(dialogueJSON, SubScene);
            else
                DialogueManager.DialogueStart(dialogueJSON);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}
