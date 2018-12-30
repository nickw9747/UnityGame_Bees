using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

//[CustomPropertyDrawer(typeof(TransitionBranch))]
//public class TransitionBranchDrawer : PropertyDrawer {
//    private SerializedProperty transitionsProp;
//    private SerializedProperty nextStateIndexProp;

//    public override void OnGUI(Rect position, SerializedProperty transitionBranchProp, GUIContent label) {

//        EditorGUI.BeginProperty(position, label, transitionBranchProp);

//        GetProperties(transitionBranchProp);

//        //EditorGUILayout.BeginVertical();
//        var transitionsRect = new Rect(
//            position.x, 
//            position.y, 
//            position.width / 2, 
//            position.height / 3);

//        var newTransitionButtonRect = new Rect(
//            position.x,
//            position.y + 2 * position.height / 3,
//            position.width / 2,
//            position.height / 3);

//        var nextStateRect = new Rect(
//            position.x + position.width / 2,
//            position.y + position.height / 3,
//            position.width / 2,
//            position.height / 3);

//        //List<Rect> transRects = new List<Rect>();

//        for (int i = 0; i < transitionsProp.arraySize; i++) {
//            var rect = new Rect(
//                transitionsRect.x,
//                transitionsRect.y + (i + 1) * transitionsRect.height / transitionsProp.arraySize,
//                transitionsRect.width,
//                transitionsRect.height / transitionsProp.arraySize);

//            EditorGUI.indentLevel++;

//            var prop = transitionsProp.GetArrayElementAtIndex(i);
//            EditorGUI.PropertyField(rect, prop);

//            //if (GUI.Button(position, "-")) {
//            //    Debug.Log("Remove Entry");
//            //}

//            EditorGUI.indentLevel--;
//        }

//        if (GUI.Button(newTransitionButtonRect, "New transition")) {
//            transitionsProp.InsertArrayElementAtIndex(transitionsProp.arraySize);
//        }

//        EditorGUI.PropertyField(nextStateRect, nextStateIndexProp);

//        //EditorGUILayout.EndVertical();
//        //base.OnGUI(position, transitionBranchProp, label);

//        EditorGUI.EndProperty();
//    }

//    private void GetProperties(SerializedProperty transitionBranchProp) {
//        transitionsProp = transitionBranchProp.FindPropertyRelative("transitions");
//        nextStateIndexProp = transitionBranchProp.FindPropertyRelative("nextStateIndex");
//    }

//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
//        return base.GetPropertyHeight(property, label) + property.FindPropertyRelative("transitions").arraySize * 20;
//    }
//}
