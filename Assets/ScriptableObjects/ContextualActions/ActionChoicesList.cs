using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ActionChoicesList", menuName = "ScriptableObjects/ActionChoicesList", order = 3)]
public class ActionChoicesList : ScriptableObject
{
    public List<ActionChoices> actionChoices;
}

[Serializable]
public class ActionChoices
{
    public string InkName;
    public ScriptableObject prefab;
}
