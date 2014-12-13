using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(WeighedPropertyAttribute))]
public class WeighedPropertyDrawer : PropertyDrawer {

    private SerializedProperty FindPropertyByType(SerializedProperty root, SerializedPropertyType type) {
        for (var it = root.Copy(); it.NextVisible(true); )
            if (it.propertyType == type)
                return it;

        return null;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        EditorGUI.BeginProperty(position, label, property);

        EditorGUI.PrefixLabel(
            new Rect(position.x, position.y, position.x + 15, position.height),
            new GUIContent("%"));

        var weight = property.FindPropertyRelative("weight");

        weight.intValue = EditorGUI.IntField(
            new Rect(position.x + 15, position.y, position.x + 25, position.height),
            weight.intValue);

        var obj = FindPropertyByType(property, SerializedPropertyType.ObjectReference);
        EditorGUI.PropertyField(position, obj, new GUIContent(" "), false);

        EditorGUI.EndProperty();
    }
}
