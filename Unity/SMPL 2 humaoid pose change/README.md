\# Migration Guide: Updating from SMPL 1 pose change to SMPL 2 humanoid pose change



This document outlines the standard procedure for upgrading the Automated Armature UI system from the SMPL 1 pose change model to the SMPL 2 humanoid pose change humanoid model within your Unity project. Following these steps will ensure a clean transition and proper functionality.



---



\## Step 1: Update Core Scripts

To begin, you must replace the outdated script files with the new versions provided for the SMPL 2 humanoid pose change model.



1\. Navigate to your project's `Assets` folder.

2\. Delete the existing scripts associated with the SMPL 1 pose change system.

3\. Import the new scripts provided in the SMPL 2 humanoid pose change asset package into their corresponding folders (e.g., `Assets/Scripts`, `Assets/Editor`).



---



\## Step 2: Clear Previously Generated Objects

Next, you will clear the scene of all objects that were generated for the previous model. This prevents conflicts and ensures a fresh build for the new model.



1\. In the Unity Hierarchy window, select the `\_UI\_BUILDER` GameObject.

2\. In the Inspector, locate the `Joint UI Builder` script component.

3\. Click the \*\*"Clear All Generated Objects"\*\* button. This action will automatically delete all child objects from the `Content` and `\_GeneratedControllers` GameObjects.



---



\## Step 3: Generate New UI Controls

Once the scene is clean, you can generate the new UI controls tailored for the SMPL 2 human pose change model's armature.



1\. Ensure the `\_UI\_BUILDER` GameObject is still selected in the Hierarchy.

2\. Verify that all public fields in the `Joint UI Builder` component (e.g., `Model Root`, `UI Parent`) are correctly assigned for the new SMPL 2 hum pose change model.

3\. Click the \*\*"Generate All Joint Controls"\*\* button. The system will inspect the new model and populate the UI scroll view with the appropriate sliders.



---



\## Step 4: Run and Verify

Finally, enter \*\*Play Mode\*\* to test the new configuration. The generated UI sliders should now be fully functional, allowing you to manipulate the pose of the SMPL v2 humanoid model in real-time.



---



\## Troubleshooting

If you encounter issues after following the steps above, please perform the following checks:



\* \*\*Verify Component Settings:\*\* The most common source of error is an incorrect configuration on the `\_UI\_BUILDER` GameObject. Carefully compare your `Joint UI Builder` component's settings against the reference images provided in the "Images of Settings" folder.



\* \*\*Check for Animator Conflicts:\*\* The new humanoid model may have an `Animator` component attached. This component will override the scripts' control over the model's bones. Disable or remove the `Animator` component from the SMPL 2 huamnoid pose change model in the scene.



\* \*\*Confirm Play Mode:\*\* The UI controls are only active during \*\*Play Mode\*\*. Ensure you have pressed the "Play" button in the editor to test functionality.

