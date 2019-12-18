using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ActionChoiceBase", menuName = "ScriptableObjects/ActionChoiceBase", order = 4)]
public class ActionChoiceBase : ScriptableObject
{
    public List<ChoiceSetContent> choicesSetContent = new List<ChoiceSetContent>(4);

    public void TriggerChoice (int choiceIndex)
    {
        foreach (string methodName in choicesSetContent[choiceIndex].Method)
        if (choicesSetContent[choiceIndex].parameter != "")
            CallMethodChoice(methodName, choicesSetContent[choiceIndex].parameter);
        else
            CallMethodChoice(methodName);
    }

    public void CallMethodChoice (string MethodName, string Parameter)
    {
        Type thisType = this.GetType();
        MethodInfo methodToCall = thisType.GetMethod(MethodName);

        methodToCall.Invoke(this, null /*TODO : PARAMETERS!*/);
    }

    public void CallMethodChoice (string MethodName)
    {
        CallMethodChoice(MethodName, "");
    }

    public void AlterInkVar ()
    {
        Debug.Log("Altering an ink var...");
    }

    public void TriggerAnim ()
    {
        Debug.Log("An anim was triggered");
    }


}

[Serializable]
public class ChoiceSetContent
{
    public string choiceString;
    public List<string> Method = new List<string>();
    public string parameter;
}