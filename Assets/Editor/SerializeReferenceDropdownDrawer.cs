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

// ==========================================
// 1. ザコ敵・ステージ用 (TimelineEvent)
// ==========================================
[CustomPropertyDrawer(typeof(TimelineEvent), true)]
public class TimelineEventDrawer : SerializeReferenceMenuDrawerBase
{
    protected override Type BaseType => typeof(TimelineEvent);
}

// ==========================================
// 2. ボス行動用 (BossMoveEvents)
// ==========================================
[CustomPropertyDrawer(typeof(BossMoveEvents), true)]
public class BossMoveEventDrawer : SerializeReferenceMenuDrawerBase
{
    protected override Type BaseType => typeof(BossMoveEvents);
}

// ==========================================
// 共通の描画ロジック (ベースクラス)
// ==========================================
public abstract class SerializeReferenceMenuDrawerBase : PropertyDrawer
{
    protected abstract Type BaseType { get; }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, true) + EditorGUIUtility.singleLineHeight + 4;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var buttonRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        var type = property.managedReferenceValue?.GetType();
        var typeName = type != null ? type.Name : $"None (クリックして {BaseType.Name} を選択)";

        if (GUI.Button(buttonRect, typeName))
        {
            var menu = new GenericMenu();

            // 各基底クラスを継承しているクラスをすべて取得
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => BaseType.IsAssignableFrom(t) && !t.IsAbstract);

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

        var fieldRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, position.height - EditorGUIUtility.singleLineHeight - 2);
        EditorGUI.PropertyField(fieldRect, property, label, true);
        EditorGUI.EndProperty();
    }
}