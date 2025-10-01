using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI; // Required for the Slider

// A serializable class to hold rotation data for a single bone using its string name.
[System.Serializable]
public class BoneTransformData
{
    // We use a string for the bone name to match your specific skeleton.
    public string boneName;
    public Vector3 rotation;
}

// A ScriptableObject to define a complete pose.
[CreateAssetMenu(fileName = "New Custom Pose", menuName = "Animation/Custom Pose Asset")]
public class PoseAsset : ScriptableObject
{
    public List<BoneTransformData> boneTransforms = new List<BoneTransformData>();
}

// The main controller class that manages the character and UI for a custom rig.
public class PoseController : MonoBehaviour
{
    [Header("Character Setup")]
    [Tooltip("The root GameObject of your character model.")]
    public GameObject characterRoot;

    [Tooltip("The root bone to be rotated by the slider. Should be 'm_avg_root'.")]
    public Transform rootBoneForSlider;

    [Header("Pose Assets")]
    [Tooltip("A list of PoseAsset files to be used by the UI buttons.")]
    public List<PoseAsset> poses;

    // A dictionary to hold all bone transforms of the character for quick access.
    private Dictionary<string, Transform> boneMap = new Dictionary<string, Transform>();

    void Awake()
    {
        if (characterRoot == null)
        {
            Debug.LogError("Character Root is not assigned in the PoseController.", this);
            return;
        }

        // Populate the boneMap dictionary by finding all transforms within the character.
        // This makes the system flexible to any skeleton structure.
        Transform[] allChildren = characterRoot.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            boneMap[child.name] = child;
        }

        // If the root bone for the slider isn't manually assigned, try to find it.
        if (rootBoneForSlider == null)
        {
            if (boneMap.ContainsKey("m_avg_root"))
            {
                rootBoneForSlider = boneMap["m_avg_root"];
            }
            else
            {
                Debug.LogError("Could not automatically find 'm_avg_root'. Please assign it manually in the Inspector.", this);
            }
        }
    }

    /// <summary>
    /// Applies a specific pose from the list based on its index.
    /// This is called by the UI buttons.
    /// </summary>
    public void ApplyPose(int poseIndex)
    {
        if (poses == null || poseIndex < 0 || poseIndex >= poses.Count)
        {
            Debug.LogWarning("Invalid pose index or poses list is not set up.", this);
            return;
        }

        PoseAsset poseToApply = poses[poseIndex];

        // Resetting is optional, but good for applying clean poses.
        // You could create a 'Reset Pose' asset for this.

        foreach (var boneData in poseToApply.boneTransforms)
        {
            if (boneMap.TryGetValue(boneData.boneName, out Transform boneTransform))
            {
                boneTransform.localEulerAngles = boneData.rotation;
            }
            else
            {
                Debug.LogWarning($"Bone '{boneData.boneName}' not found in the character model.", this);
            }
        }
    }

    /// <summary>
    /// A dedicated function to reset the character to a T-Pose or default state.
    /// You should create a PoseAsset for your model's default T-Pose and assign it to a button.
    /// </summary>
    public void ResetPose()
    {
        // For a true reset, you'd apply a T-Pose asset.
        // For a simple test, we can zero out all rotations, but this might not look right.
        foreach (var boneTransform in boneMap.Values)
        {
            boneTransform.localEulerAngles = Vector3.zero;
        }
        Debug.Log("Resetting pose. For best results, create and apply a T-Pose asset.");
    }


    /// <summary>
    /// Sets the Y-axis rotation of the character's root bone.
    /// This is called by the UI slider's OnValueChanged event.
    /// </summary>
    public void SetRootRotation(float yRotation)
    {
        if (rootBoneForSlider != null)
        {
            Vector3 currentRotation = rootBoneForSlider.localEulerAngles;
            rootBoneForSlider.localEulerAngles = new Vector3(currentRotation.x, yRotation, currentRotation.z);
        }
    }
}
