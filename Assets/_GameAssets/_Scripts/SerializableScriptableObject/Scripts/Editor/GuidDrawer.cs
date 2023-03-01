using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Guid))]
public class GuidDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var value = property.FindPropertyRelative(Guid.VALUE_FIELDNAME);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        EditorGUI.SelectableLabel(position,
            $"{value.stringValue}");
        EditorGUI.EndProperty();
    }
}