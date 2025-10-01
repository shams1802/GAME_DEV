using UnityEngine;

// This script can be attached multiple times to control a single joint on different axes.
public class UniversalJointControl : MonoBehaviour
{
    // The joint this script will control (e.g., m_avg_L_Knee)
    public Transform jointToControl;

    // The axis this specific instance of the script will rotate
    public enum RotationAxis { X, Y, Z }
    public RotationAxis axis = RotationAxis.X;

    // This public function will be called by a UI Slider.
    public void SetRotation(float angle)
    {
        if (jointToControl == null) return;

        // --- FINAL DEBUG TEST ---
        // This message will prove the slider is successfully calling this function.
        Debug.Log("SetRotation called for joint '" + jointToControl.name + "' on axis " + axis + " with angle " + angle);

        // Get the current local rotation of the joint
        Vector3 currentRotation = jointToControl.localEulerAngles;

        // Modify only the axis this script is responsible for
        switch (axis)
        {
            case RotationAxis.X:
                currentRotation.x = angle;
                break;
            case RotationAxis.Y:
                currentRotation.y = angle;
                break;
            case RotationAxis.Z:
                currentRotation.z = angle;
                break;
        }

        // Apply the new, modified rotation
        jointToControl.localEulerAngles = currentRotation;
    }
}

