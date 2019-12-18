using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContextChoicesUI : MonoBehaviour
{
    public ActionChoicesList actionChoiceList; 
    public bool allowInput = false;
    public GameObject currentChoiceObject = null;
    public List<TextMeshProUGUI> UITextChoices;

    public ActionChoiceBase currentActionList;

    public void populateChoices (ActionChoiceBase ActionList)
    {
        //TODO : GOOD LORD, FACTORIZE THIS.
        for (int i = 0; i < UITextChoices.Count; i++)
        {
            UITextChoices[i].text = ActionList.choicesSetContent[i].choiceString;
        }

        currentActionList = ActionList;
        allowInput = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (allowInput)
        {
            //TODO : USE THE REAL INPUT MANAGER PLEASE
            if (Input.GetKeyDown ("1"))
            {
                currentActionList.TriggerChoice(0);
            }
            else if (Input.GetKeyDown("2"))
            {
                currentActionList.TriggerChoice(1);
            }
            else if (Input.GetKeyDown("3"))
            {
                currentActionList.TriggerChoice(2);
            }
            else if (Input.GetKeyDown("4"))
            {
                currentActionList.TriggerChoice(3);
            }
        }
    }
}
