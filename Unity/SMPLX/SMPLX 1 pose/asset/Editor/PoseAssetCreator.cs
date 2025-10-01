using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class PoseAssetCreator : MonoBehaviour
{
    // A complete list of all the bones in your NEW smplx-male skeleton.
    // The main 'root' is excluded as it is controlled by the slider.
    private static readonly List<string> allBoneNames = new List<string>
    {
        "pelvis", "left_hip", "left_knee", "left_ankle", "left_foot",
        "right_hip", "right_knee", "right_ankle", "right_foot",
        "spine1", "spine2", "spine3", "left_collar",
        "left_shoulder", "left_elbow", "left_wrist", "neck", "head", "jaw",
        "right_collar", "right_shoulder", "right_elbow", "right_wrist"
        // Finger and eye bones are excluded for simplicity, but can be added here if needed.
    };

    [MenuItem("Tools/Create FULL BODY Pose Assets (SMPLX)")]
    private static void CreatePoseAssets()
    {
        // --- Define the KEY rotations for each pose using the NEW bone names ---
        var keyPoses = new Dictionary<string, List<BoneTransformData>>
        {
            {
                "Pose_ArmsUp", new List<BoneTransformData>
                {
                    new BoneTransformData { boneName = "left_shoulder", rotation = new Vector3(0, 0, -85) },
                    new BoneTransformData { boneName = "right_shoulder", rotation = new Vector3(0, 0, 85) },
                    new BoneTransformData { boneName = "left_elbow", rotation = new Vector3(0, 0, -15) },
                    new BoneTransformData { boneName = "right_elbow", rotation = new Vector3(0, 0, 15) }
                }
            },
            {
                "Pose_Surprised", new List<BoneTransformData>
                {
                    new BoneTransformData { boneName = "left_shoulder", rotation = new Vector3(0, 0, -70) },
                    new BoneTransformData { boneName = "right_shoulder", rotation = new Vector3(0, 0, 70) },
                    new BoneTransformData { boneName = "left_elbow", rotation = new Vector3(0, 0, -20) },
                    new BoneTransformData { boneName = "right_elbow", rotation = new Vector3(0, 0, 20) },
                    new BoneTransformData { boneName = "left_wrist", rotation = new Vector3(-60, 0, 0) },
                    new BoneTransformData { boneName = "right_wrist", rotation = new Vector3(-60, 0, 0) },
                    new BoneTransformData { boneName = "left_hip", rotation = new Vector3(-20, 0, 0) },
                    new BoneTransformData { boneName = "right_hip", rotation = new Vector3(-20, 0, 0) },
                    new BoneTransformData { boneName = "left_knee", rotation = new Vector3(40, 0, 0) },
                    new BoneTransformData { boneName = "right_knee", rotation = new Vector3(40, 0, 0) }
                }
            },
            {
                "Pose_Running", new List<BoneTransformData>
                {
                    new BoneTransformData { boneName = "spine1", rotation = new Vector3(25, 0, 0) },
                    new BoneTransformData { boneName = "left_hip", rotation = new Vector3(-60, 5, 0) },
                    new BoneTransformData { boneName = "right_hip", rotation = new Vector3(45, -5, 0) },
                    new BoneTransformData { boneName = "left_knee", rotation = new Vector3(85, 0, 0) },
                    new BoneTransformData { boneName = "right_knee", rotation = new Vector3(15, 0, 0) },
                    new BoneTransformData { boneName = "left_shoulder", rotation = new Vector3(30, 0, 75) },
                    new BoneTransformData { boneName = "right_shoulder", rotation = new Vector3(-70, 0, -75) },
                    new BoneTransformData { boneName = "left_elbow", rotation = new Vector3(100, 20, 0) },
                    new BoneTransformData { boneName = "right_elbow", rotation = new Vector3(13, 0, -30) },
                    new BoneTransformData { boneName = "left_collar", rotation = new Vector3(10, 0, 0) },
                    new BoneTransformData { boneName = "left_wrist", rotation = new Vector3(30, 0, 0) }
                }
            },
            {
                "Pose_SideKick", new List<BoneTransformData>
                {
                    new BoneTransformData { boneName = "pelvis", rotation = new Vector3(0, 0, -35) },
                    new BoneTransformData { boneName = "spine1", rotation = new Vector3(0, 0, -20) },
                    new BoneTransformData { boneName = "left_hip", rotation = new Vector3(0, 0, -45) },
                    new BoneTransformData { boneName = "right_hip", rotation = new Vector3(0, 0, 33) },
                    new BoneTransformData { boneName = "left_knee", rotation = new Vector3(0, 0, -10) },
                    new BoneTransformData { boneName = "right_knee", rotation = new Vector3(20, 0, 0) },
                    new BoneTransformData { boneName = "left_shoulder", rotation = new Vector3(0, 0, 80) },
                    new BoneTransformData { boneName = "right_shoulder", rotation = new Vector3(0, 0, -77) },
                    new BoneTransformData { boneName = "left_elbow", rotation = new Vector3(0, 0, 10) },
                    new BoneTransformData { boneName = "right_elbow", rotation = new Vector3(0, 0, 20) }
                }
            },
            {
                 "Pose_Sitting", new List<BoneTransformData>
                {
                    new BoneTransformData { boneName = "spine1", rotation = new Vector3(30, 0, 0) },
                    new BoneTransformData { boneName = "left_hip", rotation = new Vector3(-90, 3, 0) },
                    new BoneTransformData { boneName = "right_hip", rotation = new Vector3(-90, -3, 0) },
                    new BoneTransformData { boneName = "left_knee", rotation = new Vector3(90, 0, 0) },
                    new BoneTransformData { boneName = "right_knee", rotation = new Vector3(90, 0, 0) },
                    new BoneTransformData { boneName = "left_shoulder", rotation = new Vector3(-10, 0, 75) },
                    new BoneTransformData { boneName = "right_shoulder", rotation = new Vector3(-10, 0, -75) },
                    new BoneTransformData { boneName = "left_elbow", rotation = new Vector3(0, 30, 0) },
                    new BoneTransformData { boneName = "right_elbow", rotation = new Vector3(0, -30, 0) }
                }
            },
            {
                 "Pose_T_Pose", new List<BoneTransformData>
                {
                    new BoneTransformData { boneName = "left_shoulder", rotation = new Vector3(0, 0, 0) },
                    new BoneTransformData { boneName = "right_shoulder", rotation = new Vector3(0, 0, 0) },
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
            PoseAsset fullPoseAsset = ScriptableObject.CreateInstance<PoseAsset>();
            var keyRotations = keyPoseEntry.Value.ToDictionary(b => b.boneName, b => b.rotation);

            foreach (string boneName in allBoneNames)
            {
                Vector3 finalRotation = Vector3.zero;
                if (keyRotations.ContainsKey(boneName))
                {
                    finalRotation = keyRotations[boneName];
                }
                fullPoseAsset.boneTransforms.Add(new BoneTransformData { boneName = boneName, rotation = finalRotation });
            }

            string assetPath = Path.Combine(path, $"{keyPoseEntry.Key}.asset");
            AssetDatabase.CreateAsset(fullPoseAsset, assetPath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(path);
        Debug.Log($"Successfully created {keyPoses.Count} FULL BODY pose assets for the SMPLX model in the '{path}' folder.");
    }
}
