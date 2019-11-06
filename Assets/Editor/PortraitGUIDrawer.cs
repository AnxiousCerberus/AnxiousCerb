using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(PortraitsElements))]
public class PortraitGUIDrawer : PropertyDrawer
{
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        contentPosition.width *= .25f;
        EditorGUI.indentLevel = 0;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("portraitName"), GUIContent.none);

        contentPosition.width *= 1.3f;
        contentPosition.x += contentPosition.width;
        EditorGUIUtility.labelWidth = 40f;

        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("portraitSprite"), new GUIContent("Sprite"));
        contentPosition.x += contentPosition.width;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("prefab"), new GUIContent("Prefab"));
        EditorGUI.EndProperty();
    }
}
