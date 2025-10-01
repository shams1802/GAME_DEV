using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// A serializable class to hold rotation data for a single bone using its string name.
[System.Serializable]
public class BoneTransformData
{
    public string boneName;
    public Vector3 rotation;
}

// A ScriptableObject to define a complete pose.
[CreateAssetMenu(fileName = "New Custom Pose", menuName = "Animation/Custom Pose Asset")]
public class PoseAsset : ScriptableObject
{
    public List<BoneTransformData> boneTransforms = new List<BoneTransformData>();
}

public class PoseController : MonoBehaviour
{
    [Header("Character Setup")]
    public GameObject characterRoot;
    public Transform rootBoneForSlider;

    [Header("Pose Assets")]
    public List<PoseAsset> poses;
    [Tooltip("The specific T-Pose asset to apply when hiding the UI.")]
    public PoseAsset tPoseAsset;

    [Header("UI Elements")]
    [Tooltip("The main panel holding all the pose buttons.")]
    public GameObject posePanel;
    [Tooltip("The panel that covers the poses when toggled.")]
    public GameObject shapePanel;
    [Tooltip("The extra expression/shape panel to show.")]
    public GameObject expPanel;

    private Dictionary<string, Transform> boneMap = new Dictionary<string, Transform>();

    void Awake()
    {
        if (characterRoot == null)
        {
            Debug.LogError("Character Root is not assigned.", this);
            return;
        }

        Transform[] allChildren = characterRoot.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            boneMap[child.name] = child;
        }

        if (rootBoneForSlider == null)
        {
            if (boneMap.ContainsKey("root")) { rootBoneForSlider = boneMap["root"]; }
            else { Debug.LogError("Could not find 'root' bone.", this); }
        }

        // Set the initial state of the panels
        if (posePanel != null) posePanel.SetActive(true);
        if (shapePanel != null) shapePanel.SetActive(false);
        if (expPanel != null) expPanel.SetActive(false);
    }

    public void TogglePoseUIVisibility()
    {
        // Check if all panels are assigned to avoid errors
        if (posePanel == null || shapePanel == null || expPanel == null)
        {
            Debug.LogWarning("One or more UI panels are not assigned in the PoseController Inspector.", this);
            return;
        }

        // Determine the new state based on the current state of the PosePanel
        bool isPosePanelCurrentlyActive = posePanel.activeSelf;

        // Set the new active states
        posePanel.SetActive(!isPosePanelCurrentlyActive);
        shapePanel.SetActive(isPosePanelCurrentlyActive);
        expPanel.SetActive(isPosePanelCurrentlyActive);

        // If we just activated the shape/exp panels, apply the T-Pose.
        if (isPosePanelCurrentlyActive && tPoseAsset != null)
        {
            ApplyPoseLogic(tPoseAsset);
        }
    }

    public void ApplyPose(int poseIndex)
    {
        if (poses == null || poseIndex < 0 || poseIndex >= poses.Count)
        {
            Debug.LogWarning("Invalid pose index.", this);
            return;
        }

        PoseAsset poseToApply = poses[poseIndex];
        ApplyPoseLogic(poseToApply);
    }

    private void ApplyPoseLogic(PoseAsset poseToApply)
    {
        if (poseToApply == null)
        {
            Debug.LogWarning("Pose Asset to apply is null.", this);
            return;
        }

        foreach (var boneData in poseToApply.boneTransforms)
        {
            if (boneMap.TryGetValue(boneData.boneName, out Transform boneTransform))
            {
                boneTransform.localEulerAngles = boneData.rotation;
            }
            else
            {
                Debug.LogWarning($"Bone '{boneData.boneName}' not found.", this);
            }
        }
    }

    public void SetRootRotation(float yRotation)
    {
        if (rootBoneForSlider != null)
        {
            Vector3 currentRotation = rootBoneForSlider.localEulerAngles;
            rootBoneForSlider.localEulerAngles = new Vector3(currentRotation.x, yRotation, currentRotation.z);
        }
    }
}

