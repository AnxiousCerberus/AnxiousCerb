using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "PortraitsData", menuName = "ScriptableObjects/Portraits", order = 1)]
public class PortraitsManager : ScriptableObject
{
    public Texture2D IdunnojustTesting;
    //public List <PortraitsElements> PortraitList;
    public List<Sprite> OnlySprites;
    public List<String> OnlyStrings;
    public Dictionary<String, Sprite> SpriteList = new Dictionary<string, Sprite>();
}

public class SerializationCallbackScript : MonoBehaviour, ISerializationCallbackReceiver
{
    public List<int> _keys = new List<int> { 3, 4, 5 };
    public List<string> _values = new List<string> { "I", "Love", "Unity" };

    //Unity doesn't know how to serialize a Dictionary
    public Dictionary<int, string> _myDictionary = new Dictionary<int, string>();

    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();

        foreach (var kvp in _myDictionary)
        {
            _keys.Add(kvp.Key);
            _values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        _myDictionary = new Dictionary<int, string>();

        for (int i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
            _myDictionary.Add(_keys[i], _values[i]);
    }

    void OnGUI()
    {
        foreach (var kvp in _myDictionary)
            GUILayout.Label("Key: " + kvp.Key + " value: " + kvp.Value);
    }
}

[Serializable]
public class PortraitsElements
{
    public string portraitName;
    public Sprite portraitSprite;
}
