using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

//※ AI生成拡張ツール

//Inspectorでイベントの種類を選び、
//選択した各イベントの中身を編集できるようにする拡張ツールです

//例：
//Element0 -> EnemySpawnEvent
//Element1 -> PauseTimerEvent...など
[CustomPropertyDrawer(typeof(TimelineEvent), true)]
public class SerializeReferenceMenuDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // フィールド全体の高さを返す
        return EditorGUI.GetPropertyHeight(property, true) + EditorGUIUtility.singleLineHeight + 4;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // ボタン部分
        var buttonRect = new Rect(
            position.x,
            position.y,
            position.width,
            EditorGUIUtility.singleLineHeight
        );

        var type = property.managedReferenceValue?.GetType();
        var typeName = type != null ? type.Name : "None";

        if (GUI.Button(buttonRect, typeName))
        {
            var menu = new GenericMenu();
            var baseType = typeof(TimelineEvent);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => baseType.IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var t in types)
            {
                menu.AddItem(new GUIContent(t.Name), false, () =>
                {
                    property.serializedObject.Update();
                    property.managedReferenceValue = Activator.CreateInstance(t);
                    property.serializedObject.ApplyModifiedProperties();
                });
            }

            menu.ShowAsContext();
        }

        // ▼フィールド部分（ボタンの下に描画）
        var fieldRect = new Rect(
            position.x,
            position.y + EditorGUIUtility.singleLineHeight + 2,
            position.width,
            position.height - EditorGUIUtility.singleLineHeight - 2
        );

        EditorGUI.PropertyField(fieldRect, property, label, true);

        EditorGUI.EndProperty();
    }
}
