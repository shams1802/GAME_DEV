using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TextMeshPro labels

/// <summary>
/// This script dynamically creates and manages UI sliders to control
/// the beta shape parameters of an SMPLX model during runtime.
/// </summary>
public class ShapeController : MonoBehaviour
{
    [Header("Required References")]
    [Tooltip("The SMPLX model GameObject you want to control.")]
    public SMPLX smplxModel;

    [Tooltip("The UI Panel where the sliders will be created.")]
    public GameObject shapePanel;

    [Tooltip("A UI Slider prefab. It should ideally have a TextMeshPro label as a child.")]
    public GameObject sliderPrefab;

    private Slider[] betaSliders = new Slider[SMPLX.NUM_BETAS];

    void Start()
    {
        // Safety check to ensure everything is assigned in the Inspector
        if (smplxModel == null || shapePanel == null || sliderPrefab == null)
        {
            Debug.LogError("ShapeController is not set up correctly. Please assign the SMPLX Model, Shape Panel, and Slider Prefab in the Inspector.");
            return;
        }

        // Use a Layout Group on the panel for automatic positioning of sliders
        // This is a good practice for dynamically created UI elements.
        if (shapePanel.GetComponent<VerticalLayoutGroup>() == null)
        {
            Debug.LogWarning("For best results, add a VerticalLayoutGroup component to your ShapePanel.");
        }

        CreateBetaSliders();
    }

    /// <summary>
    /// Instantiates and configures sliders for all 10 beta values.
    /// </summary>
    private void CreateBetaSliders()
    {
        for (int i = 0; i < SMPLX.NUM_BETAS; i++)
        {
            // Create a new slider from the prefab inside the specified panel
            GameObject sliderInstance = Instantiate(sliderPrefab, shapePanel.transform);
            sliderInstance.name = "BetaSlider_" + i;

            // Find the Slider component within the instantiated prefab
            betaSliders[i] = sliderInstance.GetComponentInChildren<Slider>();
            if (betaSliders[i] == null)
            {
                Debug.LogError("The Slider Prefab must have a Slider component in its hierarchy.");
                continue; // Skip this iteration if the slider is not found
            }

            // Find and set the label for the slider (optional but recommended)
            TextMeshProUGUI label = sliderInstance.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null)
            {
                label.text = "Beta " + i;
            }

            // Configure the slider's properties
            betaSliders[i].minValue = -5f;
            betaSliders[i].maxValue = 5f;
            betaSliders[i].value = smplxModel.betas[i]; // Set initial value from the model

            // Add a listener that calls UpdateBetaValue when the slider is moved.
            // A local variable 'betaIndex' is used to capture the correct index for the lambda expression.
            int betaIndex = i;
            betaSliders[i].onValueChanged.AddListener(value => UpdateBetaValue(betaIndex, value));
        }
    }

    /// <summary>
    /// This method is called by a slider's onValueChanged event.
    /// </summary>
    /// <param name="index">The index of the beta value to change (0-9).</param>
    /// <param name="value">The new value from the slider.</param>
    public void UpdateBetaValue(int index, float value)
    {
        if (smplxModel != null)
        {
            // 1. Update the beta value in the SMPLX script's array
            smplxModel.betas[index] = value;

            // 2. Call the method to apply the shape changes to the mesh
            smplxModel.SetBetaShapes();

            // 3. Snap the model's feet to the ground plane to adjust for new shape
            smplxModel.SnapToGroundPlane();
        }
    }
}