using UnityEngine;
using UnityEngine.UI;

public class JointUIControlLinker : MonoBehaviour
{
    public Transform controllerObject;
    public Slider sliderX;
    public Slider sliderY;
    public Slider sliderZ;
    public JointControlProfile profile;

    void Start()
    {
        if (controllerObject == null || profile == null)
        {
            Debug.LogError("Linker on " + gameObject.name + " was not initialized correctly by the builder.");
            return;
        }

        UniversalJointControl[] controls = controllerObject.GetComponents<UniversalJointControl>();
        if (controls.Length < 3) return;

        // NEW: Activate/Deactivate sliders based on the profile
        sliderX.gameObject.SetActive(profile.useX);
        sliderY.gameObject.SetActive(profile.useY);
        sliderZ.gameObject.SetActive(profile.useZ);

        // Configure the active sliders
        if (profile.useX)
        {
            sliderX.minValue = profile.minRotation.x;
            sliderX.maxValue = profile.maxRotation.x;
            sliderX.value = 0;
            sliderX.onValueChanged.AddListener(controls[0].SetRotation);
        }
        if (profile.useY)
        {
            sliderY.minValue = profile.minRotation.y;
            sliderY.maxValue = profile.maxRotation.y;
            sliderY.value = 0;
            sliderY.onValueChanged.AddListener(controls[1].SetRotation);
        }
        if (profile.useZ)
        {
            sliderZ.minValue = profile.minRotation.z;
            sliderZ.maxValue = profile.maxRotation.z;
            sliderZ.value = 0;
            sliderZ.onValueChanged.AddListener(controls[2].SetRotation);
        }
    }
}

