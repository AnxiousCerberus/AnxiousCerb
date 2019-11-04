using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CutsceneStack", menuName = "ScriptableObjects/CutsceneStack", order = 1)]
public class CutSceneStack : ScriptableObject
{
    public List<CutsceneActions> CutsceneActionList;
}

[Serializable]
public class CutsceneActions
{
    public enum ActionType { Dialogue, WalkTo };
    public ActionType type = ActionType.Dialogue;

    public String DialogPath;
    public Transform PathNode;
}
