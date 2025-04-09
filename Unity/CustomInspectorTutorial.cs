using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomInspectorTutorial : MonoBehaviour
{
    public float ExampleVarFloat;
    public void ExampleFunctionToRun()
    {
        this.gameObject.name = this.transform.GetChild(0).gameObject.name + "_BG";
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
            _script.ExampleFunctionToRun();
        }
        //This is an example of a foldout
        _foldoutbool = EditorGUILayout.Foldout(_foldoutbool, "Example Foldout");
        if (_foldoutbool)
        {
            EditorGUI.indentLevel++;
            //This is an example of showing an editable var
            _script.ExampleVarFloat = EditorGUILayout.FloatField("VaribleTitle", _script.ExampleVarFloat);
            EditorGUI.indentLevel--;
        }
    }
}

