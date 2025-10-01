using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TextMeshPro labels

/// <summary>
/// This script dynamically creates and manages UI sliders to control
/// the facial expression parameters of an SMPLX model during runtime.
/// </summary>
public class ExpressionController : MonoBehaviour
{
    [Header("Required References")]
    [Tooltip("The SMPLX model GameObject you want to control.")]
    public SMPLX smplxModel;

    [Tooltip("The UI Panel where the expression sliders will be created.")]
    public GameObject expressionPanel;

    [Tooltip("A UI Slider prefab. It should ideally have a TextMeshPro label as a child.")]
    public GameObject sliderPrefab;

    private Slider[] expressionSliders = new Slider[SMPLX.NUM_EXPRESSIONS];

    void Start()
    {
        // Safety check to ensure everything is assigned in the Inspector
        if (smplxModel == null || expressionPanel == null || sliderPrefab == null)
        {
            Debug.LogError("ExpressionController is not set up correctly. Please assign the SMPLX Model, Expression Panel, and Slider Prefab in the Inspector.");
            return;
        }

        // It's good practice to have a layout group for dynamically created UI
        if (expressionPanel.GetComponent<VerticalLayoutGroup>() == null)
        {
            Debug.LogWarning("For best results, add a VerticalLayoutGroup component to your Expression Panel.");
        }

        CreateExpressionSliders();
    }

    /// <summary>
    /// Instantiates and configures sliders for all 10 expression values.
    /// </summary>
    private void CreateExpressionSliders()
    {
        for (int i = 0; i < SMPLX.NUM_EXPRESSIONS; i++)
        {
            // Create a new slider instance inside the specified panel
            GameObject sliderInstance = Instantiate(sliderPrefab, expressionPanel.transform);
            sliderInstance.name = "ExpressionSlider_" + i;

            // Find the Slider component in the new instance
            expressionSliders[i] = sliderInstance.GetComponentInChildren<Slider>();
            if (expressionSliders[i] == null)
            {
                Debug.LogError("The Slider Prefab must have a Slider component in its hierarchy.");
                continue;
            }

            // Find and set the label for the slider
            TextMeshProUGUI label = sliderInstance.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null)
            {
                label.text = "Exp " + i;
            }

            // Configure the slider's properties
            expressionSliders[i].minValue = -2.5f; // Expression values have a different typical range
            expressionSliders[i].maxValue = 2.5f;
            expressionSliders[i].value = smplxModel.expressions[i]; // Set initial value from the model

            // Add a listener to call UpdateExpressionValue when the slider is moved
            int expressionIndex = i; // Local variable to ensure the correct index is used in the lambda
            expressionSliders[i].onValueChanged.AddListener(value => UpdateExpressionValue(expressionIndex, value));
        }
    }

    /// <summary>
    /// This method is called by a slider's onValueChanged event.
    /// </summary>
    /// <param name="index">The index of the expression value to change (0-9).</param>
    /// <param name="value">The new value from the slider.</param>
    public void UpdateExpressionValue(int index, float value)
    {
        if (smplxModel != null)
        {
            // 1. Update the expression value in the SMPLX script's array
            smplxModel.expressions[index] = value;

            // 2. Call the method to apply the expression changes to the model's face
            smplxModel.SetExpressions();
        }
    }
}