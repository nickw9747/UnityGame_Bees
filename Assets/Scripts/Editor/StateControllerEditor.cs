using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StateController))]
public class StateControllerEditor : Editor {
    SerializedProperty stateTreesProp;

    private List<bool> foldoutList = new List<bool>();

    private void OnEnable() {
        stateTreesProp = serializedObject.FindProperty("stateTrees");
        for (int i = 0; i < stateTreesProp.arraySize; i++) {
            foldoutList.Add(false);
        }
    }

    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();
        //EditorGUILayout.PropertyField(stateTreesProp, new GUIContent("States:"), includeChildren: true);
        //EditorGUILayout.BeginHorizontal();

        StateTreeEntries();

        if (GUILayout.Button("+")) {
            stateTreesProp.InsertArrayElementAtIndex(stateTreesProp.arraySize);
            foldoutList.Add(false);
        }

        //EditorGUILayout.EndHorizontal();

        serializedObject.ApplyModifiedProperties();

    }

    private void StateTreeEntries() {
        
        for (int i = 0; i < stateTreesProp.arraySize; i++) {
            var stateTreeProp = stateTreesProp.GetArrayElementAtIndex(i);
            var stateProp = stateTreeProp.FindPropertyRelative("state");
            var transitionBranchesProp = stateTreeProp.FindPropertyRelative("transitionBranches");

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUI.indentLevel++;

            foldoutList[i] = EditorGUILayout.Foldout(foldoutList[i], "State " + i.ToString() + ": ");

            if (foldoutList[i]) {
                EditorGUILayout.PropertyField(stateProp, new GUIContent("State: "));

                EditorGUILayout.Space();

                EditorGUILayout.PropertyField(transitionBranchesProp, includeChildren: true);

                EditorGUILayout.Space();
                if (GUILayout.Button("Delete State")) {
                    DeleteState(i);
                }
                
            }

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
        }
    }

    private void DeleteState(int index) {
        stateTreesProp.DeleteArrayElementAtIndex(index);
        foldoutList.RemoveAt(index);
    }
}
