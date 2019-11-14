using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionChoiceBase : MonoBehaviour
{
    public List<ChoiceSetContent> choicesSetContent = new List<ChoiceSetContent>(4);

    public void Start ()
    {
        Debug.Log(nameof(AlterInkVar));
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

    }

    public void TriggerAnim ()
    {

    }


}

[Serializable]
public class ChoiceSetContent
{
    public string choiceString;
    public string Method;
    public string parameter;
}