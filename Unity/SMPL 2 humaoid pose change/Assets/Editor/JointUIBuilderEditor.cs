using UnityEngine;
using UnityEditor;

// This script creates the custom buttons in the Inspector for the JointUIBuilder.
[CustomEditor(typeof(JointUIBuilder))]
public class JointUIBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default fields like modelRoot, uiParent, etc.
        DrawDefaultInspector();

        // Get a reference to the script we're inspecting.
        JointUIBuilder generator = (JointUIBuilder)target;

        // Add the main "Generate" button.
        if (GUILayout.Button("Generate All Joint Controls"))
        {
            generator.Generate();
        }

        // Add the new "Clear" button.
        if (GUILayout.Button("Clear All Generated Objects"))
        {
            generator.ClearGeneratedObjects();
        }
    }
}

