using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class JointUIBuilder : MonoBehaviour
{
    [Header("Model & UI References")]
    public Transform modelRoot;
    public GameObject jointControlUIPrefab;
    public Transform uiParent;

    [Header("Controller Parent")]
    public Transform controllerParent;

    public void Generate()
    {
        if (modelRoot == null || jointControlUIPrefab == null || uiParent == null || controllerParent == null)
        {
            Debug.LogError("All fields in the JointUIBuilder must be assigned!");
            return;
        }

        foreach (Transform child in controllerParent) { if (child != null) DestroyImmediate(child.gameObject); }
        foreach (Transform child in uiParent) { if (child != null) DestroyImmediate(child.gameObject); }

        Transform[] allJoints = modelRoot.GetComponentsInChildren<Transform>();

        foreach (Transform joint in allJoints)
        {
            GameObject uiInstance = (GameObject)PrefabUtility.InstantiatePrefab(jointControlUIPrefab, uiParent);
            uiInstance.name = joint.name + "_UI";
            TMP_Text label = uiInstance.GetComponentInChildren<TMP_Text>();
            if (label != null) label.text = joint.name;

            GameObject controller = new GameObject(joint.name + "_Controller");
            controller.transform.SetParent(controllerParent);

            // Add the three control scripts
            controller.AddComponent<UniversalJointControl>().axis = UniversalJointControl.RotationAxis.X;
            controller.GetComponent<UniversalJointControl>().jointToControl = joint;

            controller.AddComponent<UniversalJointControl>().axis = UniversalJointControl.RotationAxis.Y;
            controller.GetComponents<UniversalJointControl>()[1].jointToControl = joint;

            controller.AddComponent<UniversalJointControl>().axis = UniversalJointControl.RotationAxis.Z;
            controller.GetComponents<UniversalJointControl>()[2].jointToControl = joint;


            // Find the linker script on the UI instance.
            JointUIControlLinker linker = uiInstance.GetComponent<JointUIControlLinker>();
            if (linker != null)
            {
                // Give the linker the reference to its controller.
                linker.controllerObject = controller.transform;
            }
        }
        Debug.Log("Success! Finished generating UI controls for " + allJoints.Length + " joints.");
    }
}

