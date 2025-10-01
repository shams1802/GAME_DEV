using UnityEngine;
using UnityEngine.UI;

// This script now makes the connection itself when the game starts.
public class JointUIControlLinker : MonoBehaviour
{
    // The builder will assign this. It tells the linker which controller to talk to.
    public Transform controllerObject;

    // We still need these assigned in the prefab.
    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderZ;

    // Start() is called automatically only when you press Play.
    void Start()
    {
        if (controllerObject == null)
        {
            Debug.LogError("Controller Object was not assigned to the Linker on " + gameObject.name);
            return;
        }

        // Get all three control scripts from the assigned controller object.
        UniversalJointControl[] controls = controllerObject.GetComponents<UniversalJointControl>();

        if (controls.Length < 3)
        {
            Debug.LogError("Could not find all 3 control scripts on " + controllerObject.name);
            return;
        }

        // Connect the sliders. This is now happening purely at runtime.
        sliderX.onValueChanged.AddListener(controls[0].SetRotation); // Assumes X is the first script
        sliderY.onValueChanged.AddListener(controls[1].SetRotation); // Assumes Y is the second
        sliderZ.onValueChanged.AddListener(controls[2].SetRotation); // Assumes Z is the third
    }
}

