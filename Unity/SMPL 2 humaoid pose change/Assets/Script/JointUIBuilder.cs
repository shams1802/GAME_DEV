using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

// Defines a controllable joint with human-friendly limits.
[System.Serializable]
public class JointControlProfile
{
    public string jointName; // The exact name of the joint from the hierarchy
    public Vector3 minRotation = new Vector3(-180, -180, -180);
    public Vector3 maxRotation = new Vector3(180, 180, 180);

    // New flags to control which sliders are active
    public bool useX = true;
    public bool useY = true;
    public bool useZ = true;
}

public class JointUIBuilder : MonoBehaviour
{
    [Header("Model & UI References")]
    public Transform modelRoot;
    public GameObject jointControlUIPrefab;
    public Transform uiParent;
    public Transform controllerParent;

    [Header("Human-Friendly Posing Profiles")]
    public List<JointControlProfile> jointProfiles = new List<JointControlProfile>();

    // This function runs when you first add the script or reset it.
    // It pre-populates the list with your requested joints.
    void Reset()
    {
        jointProfiles = new List<JointControlProfile>()
        {
            // Root
            new JointControlProfile() { jointName = "m_avg_root", minRotation = new Vector3(0, -180, 0), maxRotation = new Vector3(0, 180, 0), useX = false, useZ = false },
            
            // Head & Spine
            new JointControlProfile() { jointName = "m_avg_Head", minRotation = new Vector3(-60, -45, -60), maxRotation = new Vector3(60, 45, 60) },
            new JointControlProfile() { jointName = "m_avg_Spine1", minRotation = new Vector3(-30, -60, -25), maxRotation = new Vector3(30, 60, 25) },

            // Left Leg
            new JointControlProfile() { jointName = "m_avg_L_Hip", minRotation = new Vector3(-90, -45, 0), maxRotation = new Vector3(75, 45, 30) },
            new JointControlProfile() { jointName = "m_avg_L_Knee", minRotation = new Vector3(-30, -30, 0), maxRotation = new Vector3(90, 30, 15) },
            new JointControlProfile() { jointName = "m_avg_L_Ankle", minRotation = new Vector3(-60, -30, -10), maxRotation = new Vector3(60, 30, 10) },

            // Right Leg
            new JointControlProfile() { jointName = "m_avg_R_Hip", minRotation = new Vector3(-90, -45, -30), maxRotation = new Vector3(75, 45, 0) }, // Z is mirrored
            new JointControlProfile() { jointName = "m_avg_R_Knee", minRotation = new Vector3(-30, -30, -15), maxRotation = new Vector3(90, 30, 0) }, // Z is mirrored
            new JointControlProfile() { jointName = "m_avg_R_Ankle", minRotation = new Vector3(-60, -30, -10), maxRotation = new Vector3(60, 30, 10) },

            // Left Hand
            new JointControlProfile() { jointName = "m_avg_L_Shoulder", minRotation = new Vector3(-30, -90, -85), maxRotation = new Vector3(30, 45, 85) },
            new JointControlProfile() { jointName = "m_avg_L_Elbow", minRotation = new Vector3(-30, -85, -30), maxRotation = new Vector3(30, 15, 30) },
            new JointControlProfile() { jointName = "m_avg_L_Wrist", minRotation = new Vector3(-30, -15, -85), maxRotation = new Vector3(30, 15, 45) },

            // Right Hand
            new JointControlProfile() { jointName = "m_avg_R_Shoulder", minRotation = new Vector3(-30, -45, -85), maxRotation = new Vector3(30, 90, 85) }, // Y is mirrored
            new JointControlProfile() { jointName = "m_avg_R_Elbow", minRotation = new Vector3(-30, -15, -30), maxRotation = new Vector3(30, 85, 30) }, // Y is mirrored
            new JointControlProfile() { jointName = "m_avg_R_Wrist", minRotation = new Vector3(-30, -15, -45), maxRotation = new Vector3(30, 15, 85) }  // Z is mirrored
        };
    }


    public void Generate()
    {
        if (modelRoot == null || jointControlUIPrefab == null || uiParent == null || controllerParent == null)
        {
            Debug.LogError("All fields in the JointUIBuilder must be assigned!");
            return;
        }

        // Call the dedicated clear function first
        ClearGeneratedObjects();

        foreach (JointControlProfile profile in jointProfiles)
        {
            Transform joint = FindDeepChild(modelRoot, profile.jointName); // Use recursive search
            if (joint == null)
            {
                Debug.LogWarning("Could not find a joint named '" + profile.jointName + "'. Skipping.");
                continue;
            }

            GameObject uiInstance = (GameObject)PrefabUtility.InstantiatePrefab(jointControlUIPrefab, uiParent);
            uiInstance.name = profile.jointName + "_UI";
            TMP_Text label = uiInstance.GetComponentInChildren<TMP_Text>();
            if (label != null) label.text = profile.jointName.Replace("m_avg_", "").Replace("_", " "); // Make name prettier

            GameObject controller = new GameObject(profile.jointName + "_Controller");
            controller.transform.SetParent(controllerParent);

            controller.AddComponent<UniversalJointControl>().axis = UniversalJointControl.RotationAxis.X;
            controller.GetComponent<UniversalJointControl>().jointToControl = joint;
            controller.AddComponent<UniversalJointControl>().axis = UniversalJointControl.RotationAxis.Y;
            controller.GetComponents<UniversalJointControl>()[1].jointToControl = joint;
            controller.AddComponent<UniversalJointControl>().axis = UniversalJointControl.RotationAxis.Z;
            controller.GetComponents<UniversalJointControl>()[2].jointToControl = joint;

            JointUIControlLinker linker = uiInstance.GetComponent<JointUIControlLinker>();
            if (linker != null)
            {
                linker.controllerObject = controller.transform;
                linker.profile = profile;
            }
        }
        Debug.Log("Success! Finished generating UI controls for " + jointProfiles.Count + " specified joints.");
    }

    // This new function can be called by a button to clear the UI and controllers.
    public void ClearGeneratedObjects()
    {
        if (uiParent != null)
        {
            // Loop backwards when destroying immediate to avoid issues with collection changing size
            for (int i = uiParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(uiParent.GetChild(i).gameObject);
            }
        }

        if (controllerParent != null)
        {
            for (int i = controllerParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(controllerParent.GetChild(i).gameObject);
            }
        }

        Debug.Log("Cleared all generated UI and Controller objects.");
    }

    // Helper function to find a child bone no matter how deeply it's nested.
    private Transform FindDeepChild(Transform aParent, string aName)
    {
        Queue<Transform> queue = new Queue<Transform>();
        queue.Enqueue(aParent);
        while (queue.Count > 0)
        {
            var c = queue.Dequeue();
            if (c.name == aName)
                return c;
            foreach (Transform t in c)
                queue.Enqueue(t);
        }
        return null;
    }
}

