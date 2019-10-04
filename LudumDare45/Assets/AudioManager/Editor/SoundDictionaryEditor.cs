using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundDictionary), true)]
public class SoundDictionaryEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Sound Names to Use For This Dictionary:", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (Event.current.type != EventType.DragPerform)
        {

            foreach (var s in ((SoundDictionary)target).audioObjects)
            {
                if (s != null)
                    EditorGUILayout.LabelField(s.soundName);
            }
        }
    }
}
