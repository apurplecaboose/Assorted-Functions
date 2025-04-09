using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
///This is an example of a script you want to add a custom inspector to
public class CustomInspectorTutorial : MonoBehaviour
{
    public float ExampleVarFloat;
    public void ExampleFunctionToRun()
    {
         Debug.Log("Custom Inspector button pressed")
    }
}
//below is the class that will handle the custom inspector.
[CustomEditor(typeof(CustomInspectorTutorial))]
public class CustomInspectorTutorial_Editor : Editor
{
    CustomInspectorTutorial _script;
    bool _foldoutbool;
    private void OnEnable()
    {
        _script = (CustomInspectorTutorial)target;
    }
    public override void OnInspectorGUI()
    {
        //This is a header equivalant
        EditorGUILayout.LabelField("This is a title", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold });
        //This is an inspector button
        if (GUILayout.Button("Button"))
        {
            _script.ExampleFunctionToRun(); // will run the function you want on button click event
        }
        //This is an example of a foldout
        _foldoutbool = EditorGUILayout.Foldout(_foldoutbool, "Example Foldout");
        if (_foldoutbool)
        {
            EditorGUI.indentLevel++; // this is used to indent
            //This is an example of showing an editable var
            _script.ExampleVarFloat = EditorGUILayout.FloatField("VaribleTitle", _script.ExampleVarFloat);
            EditorGUI.indentLevel--;// this is used to remove indent
        }
    }
}

