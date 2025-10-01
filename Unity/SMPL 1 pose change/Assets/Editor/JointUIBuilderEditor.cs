using UnityEngine;
using UnityEditor;

// This script creates the custom button in the Inspector for the JointUIBuilder component.
// It MUST be inside a folder named "Editor".
[CustomEditor(typeof(JointUIBuilder))]
public class JointUIBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default Inspector variables (modelRoot, uiParent, etc.)
        DrawDefaultInspector();

        // Get a reference to the JointUIBuilder script we are inspecting
        JointUIBuilder generator = (JointUIBuilder)target;

        // Add a button to the Inspector
        if (GUILayout.Button("Generate All Joint Controls"))
        {
            // Call the public Generate() function when the button is clicked
            generator.Generate();
        }
    }
}
