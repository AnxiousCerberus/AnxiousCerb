using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PortraitsData", menuName = "ScriptableObjects/Portraits", order = 1)]
public class PortraitsManager : ScriptableObject
{
    public List <PortraitsElements> PortraitList;
}

[Serializable]
public class PortraitsElements
{
    public string portraitName;
    public Sprite portraitSprite;
}
