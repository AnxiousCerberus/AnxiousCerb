using UnityEditor;
using UnityEditor.IMGUI;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

[CustomEditor(typeof(PortraitsManager))]
public class VisualSpriteManager : Editor
{

    PortraitsManager manager;
    string oldString = "DefaultOld";
    string spriteLabel = "DefaultNew";

    string oldModifiedLabel = "ModifiedOld";
    string newModifiedLabel = "ModifiedNew";

    public void OnEnable()
    {
        manager = (PortraitsManager)target;
    }

    void AddToList(string s, Sprite sprite)
    {
        manager.OnlySprites.Add(sprite);
        manager.OnlyStrings.Add(s);
    }

    void RemoveFromList (int index)
    {

    }



    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        spriteLabel = EditorGUILayout.TextField("Label", oldString);
        oldString = spriteLabel;

        if (GUILayout.Button("Add"))
        {
            Texture2D blankTexture = new Texture2D (0,0);
            //manager.SpriteList.Add(spriteLabel, Sprite.Create(blankTexture, new Rect (0,0,blankTexture.width, blankTexture.height), new Vector2 (.5f, .5f)));
            AddToList(spriteLabel, Sprite.Create(blankTexture, new Rect(0, 0, blankTexture.width, blankTexture.height), new Vector2(.5f, .5f)));
            Debug.Log("It's alive: " + target.name + " Dictionary Count is " + manager.OnlyStrings.Count + " " + manager.OnlySprites.Count);
        }

        for (int i = 0; i < manager.OnlyStrings.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            manager.OnlySprites[i] = (Sprite)EditorGUILayout.ObjectField(manager.OnlySprites[i], typeof(Sprite), false, GUILayout.Width(95f), GUILayout.Height(120f));
            
            newModifiedLabel = EditorGUILayout.DelayedTextField("Label", manager.OnlyStrings[i]);
            if (newModifiedLabel != manager.OnlyStrings[i])
            {
                manager.OnlyStrings[i] = newModifiedLabel;
            }

            if (GUILayout.Button("Remove"))
            {
                manager.OnlyStrings.RemoveAt(i);
                manager.OnlySprites.RemoveAt(i);
            }

            EditorGUILayout.EndHorizontal();
        }



        EditorUtility.SetDirty(target);
        serializedObject.ApplyModifiedProperties();
        /*//MAP DEFAULT INFORMATION
        comp.mapName = EditorGUILayout.TextField("Name", comp.mapName);

        //WIDTH - HEIGHT
        int width = EditorGUILayout.IntField("Map Sprite Width", comp.mapSprites.GetLength(0));
        int height = EditorGUILayout.IntField("Map Sprite Height", comp.mapSprites.GetLength(1));

        if (width != comp.mapSprites.GetLength(0) || height != comp.mapSprites.GetLength(1))
        {
            comp.mapSprites = new Sprite[width, height];
        }

        showTileEditor = EditorGUILayout.Foldout(showTileEditor, "Tile Editor");

        if (showTileEditor)
        {
        for (int h = 0; h < 10; h++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int w = 0; w < 2; w++)
                {
                    manager.mapSprites[w, h] = 
                }
                EditorGUILayout.EndHorizontal();
            }
        }*/
    }

}