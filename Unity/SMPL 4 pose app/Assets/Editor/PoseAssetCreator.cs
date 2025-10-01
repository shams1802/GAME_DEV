using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class PoseAssetCreator
{
    // A complete list of all the bones in your character's skeleton that can be posed.
    // The m_avg_root is excluded as it is controlled by the slider.
    private static readonly List<string> allBoneNames = new List<string>
    {
        "m_avg_Pelvis", "m_avg_L_Hip", "m_avg_L_Knee", "m_avg_L_Ankle", "m_avg_L_Foot",
        "m_avg_R_Hip", "m_avg_R_Knee", "m_avg_R_Ankle", "m_avg_R_Foot",
        "m_avg_Spine1", "m_avg_Spine2", "m_avg_Spine3", "m_avg_L_Collar",
        "m_avg_L_Shoulder", "m_avg_L_Elbow", "m_avg_L_Wrist", "m_avg_L_Hand",
        "m_avg_Neck", "m_avg_Head", "m_avg_R_Collar", "m_avg_R_Shoulder",
        "m_avg_R_Elbow", "m_avg_R_Wrist", "m_avg_R_Hand"
    };

    [MenuItem("Tools/Create FULL BODY Pose Assets")]
    private static void CreatePoseAssets()
    {
        // --- Define the KEY rotations for each pose ---
        // These are only the bones that are actively doing something in the pose.
        var keyPoses = new Dictionary<string, List<BoneTransformData>>
        {
            {
                "Pose_ArmsUp", new List<BoneTransformData>
                {
                    new BoneTransformData { boneName = "m_avg_L_Shoulder", rotation = new Vector3(0, 0, -85) },
                    new BoneTransformData { boneName = "m_avg_R_Shoulder", rotation = new Vector3(0, 0, 85) },
                    new BoneTransformData { boneName = "m_avg_L_Elbow", rotation = new Vector3(0, 0, -15) },
                    new BoneTransformData { boneName = "m_avg_R_Elbow", rotation = new Vector3(0, 0, 15) }
                }
            },
            {
                "Pose_Surprised", new List<BoneTransformData>
                {
                    new BoneTransformData { boneName = "m_avg_L_Shoulder", rotation = new Vector3(0, 0, -70) },
                    new BoneTransformData { boneName = "m_avg_R_Shoulder", rotation = new Vector3(0, 0, 70) },
                    new BoneTransformData { boneName = "m_avg_L_Elbow", rotation = new Vector3(0, 0, -20) },
                    new BoneTransformData { boneName = "m_avg_R_Elbow", rotation = new Vector3(0, 0, 20) },
                    new BoneTransformData { boneName = "m_avg_L_Wrist", rotation = new Vector3(-60, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_R_Wrist", rotation = new Vector3(-60, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_L_Hip", rotation = new Vector3(-20, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_R_Hip", rotation = new Vector3(-20, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_L_Knee", rotation = new Vector3(40, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_R_Knee", rotation = new Vector3(40, 0, 0) }
                }
            },
            {
                "Pose_Running", new List<BoneTransformData>
                {
                    new BoneTransformData { boneName = "m_avg_Spine1", rotation = new Vector3(25, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_L_Hip", rotation = new Vector3(-60, 5, 0) },
                    new BoneTransformData { boneName = "m_avg_R_Hip", rotation = new Vector3(45, -5, 0) },
                    new BoneTransformData { boneName = "m_avg_L_Knee", rotation = new Vector3(85, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_R_Knee", rotation = new Vector3(15, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_L_Shoulder", rotation = new Vector3(30, 0, 75) },
                    new BoneTransformData { boneName = "m_avg_R_Shoulder", rotation = new Vector3(-70, 0, -75) },
                    new BoneTransformData { boneName = "m_avg_L_Elbow", rotation = new Vector3(100, 20, 0) },
                    new BoneTransformData { boneName = "m_avg_R_Elbow", rotation = new Vector3(13, 0, -30) },
                    new BoneTransformData { boneName = "m_avg_L_Collar", rotation = new Vector3(10, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_L_Wrist", rotation = new Vector3(30, 0, 0) }
                }
            },
            {
                "Pose_SideKick", new List<BoneTransformData>
                {
                    new BoneTransformData { boneName = "m_avg_Pelvis", rotation = new Vector3(0, 0, -35) },
                    new BoneTransformData { boneName = "m_avg_Spine1", rotation = new Vector3(0, 0, -20) },
                    new BoneTransformData { boneName = "m_avg_L_Hip", rotation = new Vector3(0, 0, -45) },
                    new BoneTransformData { boneName = "m_avg_R_Hip", rotation = new Vector3(0, 0, 33) },
                    new BoneTransformData { boneName = "m_avg_L_Knee", rotation = new Vector3(0, 0, -10) },
                    new BoneTransformData { boneName = "m_avg_R_Knee", rotation = new Vector3(20, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_L_Shoulder", rotation = new Vector3(0, 0, 80) },
                    new BoneTransformData { boneName = "m_avg_R_Shoulder", rotation = new Vector3(0, 0, -77) },
                    new BoneTransformData { boneName = "m_avg_L_Elbow", rotation = new Vector3(0, 0, 10) },
                    new BoneTransformData { boneName = "m_avg_R_Elbow", rotation = new Vector3(0, 0, 20) }
                }
            },
            {
                 "Pose_Sitting", new List<BoneTransformData>
                {
                    new BoneTransformData { boneName = "m_avg_Spine1", rotation = new Vector3(30, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_L_Hip", rotation = new Vector3(-90, 3, 0) },
                    new BoneTransformData { boneName = "m_avg_R_Hip", rotation = new Vector3(-90, -3, 0) },
                    new BoneTransformData { boneName = "m_avg_L_Knee", rotation = new Vector3(90, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_R_Knee", rotation = new Vector3(90, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_L_Shoulder", rotation = new Vector3(-10, 0, 75) },
                    new BoneTransformData { boneName = "m_avg_R_Shoulder", rotation = new Vector3(-10, 0, -75) },
                    new BoneTransformData { boneName = "m_avg_L_Elbow", rotation = new Vector3(0, 30, 0) },
                    new BoneTransformData { boneName = "m_avg_R_Elbow", rotation = new Vector3(0, -30, 0) }
                }
            },
            {
                 "Pose_T_Pose", new List<BoneTransformData>
                {
                    new BoneTransformData { boneName = "m_avg_L_Shoulder", rotation = new Vector3(0, 0, 0) },
                    new BoneTransformData { boneName = "m_avg_R_Shoulder", rotation = new Vector3(0, 0, 0) }
                }
            }
        };

        // --- Logic to create the final asset files ---
        string path = "Assets/Poses";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        foreach (var keyPoseEntry in keyPoses)
        {
            // Create a new Pose Asset instance in memory
            PoseAsset fullPoseAsset = ScriptableObject.CreateInstance<PoseAsset>();

            // Create a dictionary of the key rotations for quick lookup
            var keyRotations = keyPoseEntry.Value.ToDictionary(b => b.boneName, b => b.rotation);

            // For every bone in the skeleton...
            foreach (string boneName in allBoneNames)
            {
                Vector3 finalRotation = Vector3.zero;

                // Check if this bone has a specific rotation in the key pose definition.
                if (keyRotations.ContainsKey(boneName))
                {
                    // If yes, use that specific rotation.
                    finalRotation = keyRotations[boneName];
                }

                // Add the bone data to the asset. If it wasn't a key bone,
                // its rotation will be Vector3.zero, effectively resetting it.
                fullPoseAsset.boneTransforms.Add(new BoneTransformData { boneName = boneName, rotation = finalRotation });
            }

            // Save the completed, full-body pose asset to the project folder.
            string assetPath = Path.Combine(path, $"{keyPoseEntry.Key}.asset");
            AssetDatabase.CreateAsset(fullPoseAsset, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(path);

        Debug.Log($"Successfully created {keyPoses.Count} FULL BODY pose assets in the '{path}' folder.");
    }
}

