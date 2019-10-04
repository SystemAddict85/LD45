using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AudioObject),true)]
public class AudioObjectEditor : Editor {
    
    [SerializeField] private AudioSource _previewer;

    public void OnEnable()
    {
        _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
    }

    public void OnDisable()
    {
        DestroyImmediate(_previewer.gameObject);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
        if (GUILayout.Button("Play"))
        {           
           ((AudioObject)target).PlayAudio(_previewer);
        }
        if (GUILayout.Button("Stop"))
        {
            ((AudioObject)target).StopAudio(_previewer);
        }
        EditorGUI.EndDisabledGroup();
    }
}
