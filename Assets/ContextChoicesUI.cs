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

    public void populateChoices (GameObject spawnedActionList)
    {
        //TODO : GOOD LORD, FACTORIZE THIS.
        for (int i = 0; i < UITextChoices.Count; i++)
        {
            UITextChoices[i].text = currentChoiceObject.GetComponent<ActionChoiceBase>().choicesSetContent[i].choiceString;
        }

        currentActionList = spawnedActionList.GetComponent<ActionChoiceBase>();
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
            if (Input.GetKeyDown ("u"))
            {
                currentActionList.TriggerChoice(0);
            }
        }
    }
}
