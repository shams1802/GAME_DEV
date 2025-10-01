Unity SMPLX Runtime Shape \& Expression Controller

1\. Overview

This project provides a set of Unity scripts to dynamically generate a user interface for controlling the shape (beta) and facial expression (expression) parameters of an SMPLX model in real-time during gameplay.



Instead of setting blendshape values in the editor, this solution creates UI sliders at runtime, allowing for live, interactive character customization. This is ideal for in-game character creators, avatar customization menus, or real-time animation tools.



Key Features

Dynamic UI Generation: Automatically creates and configures sliders for all 10 shape and 10 expression parameters.



Real-Time Feedback: The model's mesh updates instantly as sliders are adjusted.



Modular Code: Separate, clean controllers for shape and expression make the system easy to manage and extend.



Automatic Layout: Uses Unity's layout components to neatly arrange the UI elements without manual positioning.



Ground Snapping: Automatically adjusts the model's vertical position to keep its feet on the ground after its shape changes.



2\. Prerequisites (What Was Required)

If followed previous SMPLX and SMPLX 1 pose no need to worry.



To implement this system, the following components were necessary:



A Unity Project: A standard Unity environment (2019.x or newer recommended).



SMPLX Model: A 3D model compatible with the SMPLX rig, correctly imported into Unity. Crucially, the model's mesh must contain the necessary blendshapes named Shape000 through Shape009 and Exp000 through Exp009.



Core SMPLX.cs Script: The provided script that serves as the API for the model. It must contain:



public float\[] betas = new float\[10];



public float\[] expressions = new float\[10];



A public void SetBetaShapes() method to apply the betas array to the model's blendshapes.



A public void SetExpressions() method to apply the expressions array.



A public void SnapToGroundPlane() method to adjust the model's height.



Unity UI Canvas and Panels:



A UI Canvas in the scene.



Two empty UI Panel GameObjects parented to the Canvas: one named ShapePanel and another named ExpPanel.



TextMeshPro Package: Unity's TextMeshPro package is required for the slider labels. This usually comes pre-installed with modern versions of Unity.



3\. Implementation (What We Did)

We created two new scripts to act as controllers, separating the logic for shape and expression.



ShapeController.cs

This script manages the UI for the body shape parameters.



References: It holds public variables for the SMPLX model, the ShapePanel, and a Slider prefab.



Slider Creation: In its Start() method, it loops 10 times. In each iteration, it instantiates the slider prefab inside the ShapePanel.



Configuration: Each new slider is configured with a label ("Beta 0", "Beta 1", etc.) and its min/max values are set (from -5 to 5).



Event Listening: A listener is added to each slider's onValueChanged event. This listener calls the UpdateBetaValue method, passing the slider's specific index and its new value.



Model Update: The UpdateBetaValue method updates the corresponding index in the smplxModel.betas array and then calls smplxModel.SetBetaShapes() and smplxModel.SnapToGroundPlane() to apply the changes.



ExpressionController.cs

This script follows the exact same pattern as the ShapeController but for facial expressions.



References: It holds references to the SMPLX model, the ExpPanel, and the same Slider prefab.



Slider Creation \& Configuration: It instantiates and configures 10 sliders inside the ExpPanel, labeling them "Exp 0", "Exp 1", etc., and setting their value range (from -2.5 to 2.5).



Event Listening \& Model Update: Its listeners call an UpdateExpressionValue method, which updates the smplxModel.expressions array and then calls smplxModel.SetExpressions() to make the face change.



Unity Editor Setup

The final step was to configure everything in the Unity Inspector:



A UI Slider was created and saved as a prefab.



In the Hierarchy, right-click your ShapePanel and go to UI -> Slider - TextMeshPro. This will create a slider with a text label.



Select the new Slider object. You can adjust its size and look in the Inspector.



Crucially, add a Layout Element component to this slider to control its size within the panel. Give it a Min Height, for example, 30.



Drag the configured Slider from your Hierarchy into your Assets folder (e.g., into a Prefabs folder) to create a prefab.



You can now delete the slider from the Hierarchy; we only need the prefab.







A Vertical Layout Group was added to both ShapePanel and ExpPanel to ensure the sliders would be arranged vertically.



Select the ShapePanel GameObject in your Hierarchy.



In the Inspector, click Add Component and search for Vertical Layout Group.



You can adjust its settings (like Padding and Spacing) to make the UI look nice.



Repeat for ExpPanel .







An empty GameObject (ShapeManager) was created to hold the controller scripts.



Create a new empty GameObject in your scene (Right-click in Hierarchy -> Create Empty). Rename it ShapeManager.







Attach both ShapeController.cs and ExpressionController.cs  to the ShapeManager object.







Select the ShapeManager GameObject. You will see three empty fields in the Inspector for the ShapeController script.



Smplx Model: Drag your smplx-male GameObject from the Hierarchy into this slot.



Shape/Exp Panel: Drag your ShapePanel/ExpPanel UI object from the Hierarchy into this slot.



Slider Prefab: Drag the Slider prefab you created in Step 1 from your Assets folder into this slot.



4\. Common Mistakes to Avoid

When implementing a system like this, several common issues can arise.



❌ Forgetting Inspector References:



Problem: The most frequent error is failing to drag the SMPLX model, UI panels, or slider prefab into their public fields in the Inspector. This will cause a NullReferenceException error when you press Play, as the script won't know what to control or where to create the UI.



Solution: Double-check that all public fields on both controller scripts are assigned before running the scene.



❌ Missing Layout Group:



Problem: If you don't add a Vertical Layout Group (or a similar component) to the UI panels, all 10 sliders will be instantiated at the same default position (the center of the panel), stacked on top of each other.



Solution: Always add a layout group component to any panel that will hold dynamically generated UI elements.



❌ Incorrect C# Lambda/Closure in Loop:



Problem: A classic C# mistake is to use a loop variable directly inside a lambda expression, like slider.onValueChanged.AddListener(value => UpdateBetaValue(i, value));. Because of how closures work, i would not be its value at the time the listener was created. Instead, all 10 listeners would end up using the final value of i (which is 10), causing all sliders to control the non-existent "Beta 10".



Solution: We avoided this by creating a local copy of the index inside the loop before defining the listener: int betaIndex = i;. This ensures each listener captures a unique, correct index value.



❌ Incorrect Slider Prefab:



Problem: The scripts assume the prefab has a Slider component and a TextMeshProUGUI component in its hierarchy. If the prefab is missing these, you will get errors or the UI will not look correct.



Solution: Create the prefab carefully, ensuring it's a "Slider - TextMeshPro" object from the Unity UI menu, or that you've added the components manually.

