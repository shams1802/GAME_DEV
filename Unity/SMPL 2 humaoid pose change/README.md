# 🚀 Migration Guide: Updating from SMPL 1 Pose Change to SMPL 2 Humanoid Pose Change

This guide explains the procedure for upgrading the **Automated Armature UI system** from the **SMPL 1 pose change model** to the **SMPL 2 humanoid pose change model** in Unity.  
Following these steps ensures a clean transition and proper functionality.

---

## 🔧 Step 1: Update Core Scripts
Replace the old scripts with the new SMPL 2 versions.

1. Navigate to your project’s `Assets` folder.  
2. Delete all existing scripts related to the **SMPL 1 pose change** system.  
3. Import the new scripts from the **SMPL 2 humanoid pose change** asset package into their corresponding folders (e.g., `Assets/Scripts`, `Assets/Editor`).  

---

## 🗑 Step 2: Clear Previously Generated Objects
Remove old scene objects to avoid conflicts.

1. In the **Hierarchy**, select the `_UI_BUILDER` GameObject.  
2. In the **Inspector**, locate the **Joint UI Builder** script component.  
3. Click **"Clear All Generated Objects"**.  
   - This automatically deletes all child objects under `Content` and `_GeneratedControllers`.  

---

## 🎛 Step 3: Generate New UI Controls
Build fresh UI controls for the SMPL 2 humanoid pose change model.

1. Keep the `_UI_BUILDER` GameObject selected.  
2. Verify public fields in **Joint UI Builder** (e.g., `Model Root`, `UI Parent`) are correctly assigned for the new model.  
3. Click **"Generate All Joint Controls"**.  
   - The system inspects the model and populates the UI scroll view with appropriate sliders.  

---

## ▶️ Step 4: Run and Verify
1. Enter **Play Mode**.  
2. Test the new configuration.  
3. The generated UI sliders should now manipulate the pose of the **SMPL v2 humanoid model** in real time.  

---

## ❗ Troubleshooting

- **Verify Component Settings**  
  Ensure `_UI_BUILDER` → **Joint UI Builder** component is configured correctly. Compare with the reference images in the `Images of Settings` folder.  

- **Check for Animator Conflicts**  
  If the SMPL 2 humanoid model has an `Animator` component, it will override bone control. Disable or remove the `Animator` component.  

- **Confirm Play Mode**  
  The UI controls are only functional in **Play Mode**. Make sure you’ve pressed the **Play** button in the editor.  

---
