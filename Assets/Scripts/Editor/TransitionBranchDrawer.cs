using UnityEngine;
using UnityEditor;
using System;

//[CustomPropertyDrawer(typeof(TransitionBranch))]
//public class TransitionBranchDrawer : PropertyDrawer {
//    private SerializedProperty transitionsProp;
//    private SerializedProperty nextStateIndexProp;

//    public override void OnGUI(Rect position, SerializedProperty transitionBranchProp, GUIContent label) {
//        EditorGUI.BeginProperty(position, label, transitionBranchProp);

//        GetProperties(transitionBranchProp);

//        //EditorGUILayout.BeginVertical();

//        for (int i = 0; i < transitionsProp.arraySize; i++) {
//            EditorGUI.indentLevel++;

//            var prop = transitionsProp.GetArrayElementAtIndex(i);
//            EditorGUI.PropertyField(position, prop);

//            if (GUI.Button(position, "-")) {
//                Debug.Log("Remove Entry");
//            }

//            EditorGUI.indentLevel--;
//        }
        
//        if (GUI.Button(position, "New transition")) {
//            transitionsProp.InsertArrayElementAtIndex(transitionsProp.arraySize);
//        }

//        //EditorGUILayout.EndVertical();
//        //base.OnGUI(position, transitionBranchProp, label);

//        EditorGUI.EndProperty();
//    }

//    private void GetProperties(SerializedProperty transitionBranchProp) {
//        transitionsProp = transitionBranchProp.FindPropertyRelative("transitions");
//        nextStateIndexProp = transitionBranchProp.FindPropertyRelative("nextStateIndex");
//    }

//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
//        return base.GetPropertyHeight(property, label) + 20;
//    }
//}
