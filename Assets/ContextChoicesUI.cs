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

    public void populateChoices ()
    {
        //TODO : GOOD LORD, FACTORIZE THIS.
        for (int i = 0; i < UITextChoices.Count; i++)
        {
            UITextChoices[i].text = currentChoiceObject.GetComponent<ActionChoiceBase>().choicesSetContent[i].choiceString;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
