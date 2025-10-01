# Automated Armature UI Generator for Unity

This Unity project provides a complete system to automatically generate a user interface with sliders for controlling every bone of a rigged 3D model. It's a powerful tool for creating character posing apps, animation helpers, or robotics simulations with just a single click.

---

## 📑 Table of Contents
- [How It Works](#how-it-works)
- [Getting Started: Project Setup](#getting-started-project-setup)
  - [1. Create the Folder Structure](#1-create-the-folder-structure)
  - [2. Prepare the Scene](#2-prepare-the-scene)
- [Step-by-Step Guide](#step-by-step-guide)
  - [Step 1: Create the Reusable UI Prefab](#step-1-create-the-reusable-ui-prefab)
  - [Step 2: Canvas Setting](#step-2-canvas-setting)
  - [Step 3: Set Up the Builder Object](#step-3-set-up-the-builder-object)
  - [Step 4: Generate and Run!](#step-4-generate-and-run)
- [Important Notes & Troubleshooting](#important-notes--troubleshooting)

---

## How It Works

The system is built from four key scripts that work together:

1. **UniversalJointControl.cs (The Worker):**  
   This script is the core rotator. A single instance of this script is responsible for rotating one joint on one axis (X, Y, or Z). The builder script creates three of these for every bone to control its full rotation.

2. **JointUIControlLinker.cs (The Connector):**  
   This script lives on the UI prefab. Its job is to connect the UI sliders (X, Y, Z) to the correct "Worker" scripts when you press the Play button.

3. **JointUIBuilder.cs (The Factory):**  
   This is the main engine. When you tell it to run, it inspects your 3D model, finds every single bone, and then automatically builds all the necessary UI panels and controller objects.

4. **JointUIBuilderEditor.cs (The Button):**  
   This is a simple helper script that creates the "Generate All Joint Controls" button in the Unity Inspector, making the whole process easy to run.

---

## Getting Started: Project Setup

Follow these steps to set up the project from scratch.

### 1. Create the Folder Structure

In your Unity project's Assets folder, create the following folders and place the provided files inside them. This exact structure is required for the scripts to work correctly.





```plaintext
Assets/
├── Editor/
│   └── JointUIBuilderEditor.cs
│
├── Models/
│   └── SPML_male.fbx
│
├── Prefabs/
│   └── (You will create your UI prefab here)
│
└── Scripts/
    ├── JointUIBuilder.cs
    ├── JointUIControlLinker.cs
    └── UniversalJointControl.cs
```



### 2. Prepare the Scene

1. Create a new 3D Scene in Unity (`File > New Scene`).  
2. Drag the `SPML_male.fbx` model from the `Assets/Models` folder into the Hierarchy window.  
3. Adjust your Main Camera and Directional Light so you can see the model clearly.  

---

## Step-by-Step Guide

### Step 1: Create the Reusable UI Prefab

This is the visual template for a single joint's controls. The builder script will copy this template for every bone in the model.

1. **Create a Canvas:** Right-click in the Hierarchy → UI → Canvas.  
2. **Create a Scrollable Area:** Right-click on the Canvas → UI → Scroll View, rename it `JointControlsScrollView`. This ensures you can see all the generated controls even if they go off-screen.  
3. **Create the Main Panel:** Right-click on the Content object (located under `ScrollView/Viewport/Content`) → UI → Panel. Rename this new panel to `JointControlUnit`.  
4. **Add a Layout Group:** Select `JointControlUnit`. In the Inspector, click "Add Component" and add a `Layout Element`.  
   - Set Layout Height = `80`  
   - Set Layout Priority = `1`  
5. **Add the Joint Label:** Right-click on `JointControlUnit` → UI → Text - TextMeshPro. Rename it `JointNameLabel`.  
   - In Rectangle Transform, set anchor preset to **centre middle**, W = `200`, H = `50`  
6. **Create a Slider Container:** Right-click on `JointControlUnit` → Create Empty. Rename it `SliderContainer`.  
   - Set anchor preset to **stretch stretch**  
   - Add a `Horizontal Layout Group` component to `SliderContainer`  
   - Child Alignment = Upper Left  
   - Control Child Size = Width  
   - Child Force Expand = Width, Height  
7. **Add the Sliders:** Right-click on `SliderContainer` → UI → Slider.  
   - Set anchor preset = top left, H = `20`  
   - Set Min value = `-180`, Max value = `180`  
   - Duplicate the slider two times, so you have three total.  
   - Rename them precisely: `Slider_X`, `Slider_Y`, and `Slider_Z`.  
8. **Add the Connector Script:** Select the root `JointControlUnit` panel. In the Inspector, click "Add Component" and add the `JointUIControlLinker` script.  
9. **Link the Sliders:** Drag each slider from the Hierarchy into its corresponding slot on the `JointUIControlLinker` script component.  
   - Drag `Slider_X` to the Slider X field.  
   - Drag `Slider_Y` to the Slider Y field.  
   - Drag `Slider_Z` to the Slider Z field.  
10. **Create the Prefab:** Drag the fully configured `JointControlUnit` object from the Hierarchy into your `Assets/Prefabs` folder. Once the icon in the Hierarchy turns blue, you can safely delete the `JointControlUnit` from the scene.  

---

### Step 2: Canvas Setting

1. In `Canvas/JointControlsScrollView`, look in inspector:  
   - Set anchor preset to **left stretch**, W = `350`  
2. In `Canvas/JointControlsScrollView/Viewport/Content`, look in inspector:  
   - Set anchor preset to **top stretch**, W = `350`  
   - Add `Vertical Layout Group`:  
     1. Set Padding = `10`, Spacing = `5`  
     2. Child Alignment = Upper Centre  
     3. Control Child Size = Width, Height  
     4. Child Force Expand = Width, Height  
   - Add `Content Size Fitter`, set Vertical to **preferred**  

---

### Step 3: Set Up the Builder Object

1. Create Empty GameObjects in the Hierarchy. Name them exactly:  
   - `_UI_BUILDER`  
   - `_GeneratedControllers`  
2. Assign the Builder Script: Select `_UI_BUILDER`. In the Inspector, click "Add Component" and add the `JointUIBuilder` script.  
3. Link Everything: Fill in the public fields on the `JointUIBuilder` script by dragging objects from the Hierarchy and the Project window:  
   - **Model Root:** Drag the `m_avg` object from your `Hierarchy/SPML_male`.  
   - **Joint Control UI Prefab:** Drag your `JointControlUnit` prefab from the `Assets/Prefabs` folder.  
   - **UI Parent:** Drag the `Content` object (from inside your Scroll View) from the Hierarchy.  
   - **Controller Parent:** Drag the `_GeneratedControllers` object from your Hierarchy.  

---

### Step 4: Generate and Run!

1. Select the `_UI_BUILDER` GameObject in the Hierarchy.  
2. In the Inspector, you will now see a custom button. Click **"Generate All Joint Controls"**.  
3. The UI will be instantly built inside your Scroll View in the Scene.  
4. Press the **Play** button at the top of the editor.  
5. Use the sliders to pose and control the model's joints in real-time!  

---

## Important Notes & Troubleshooting

- **You Must Be in Play Mode:** The sliders will not work until you press the Play button. The `JointUIControlLinker` script only connects the sliders to the model when the game starts.  
- **Disable the Model's Animator:** If your imported model has an Animator component, it will fight with your scripts for control of the bones. You must disable or remove the Animator component from the `SPML_male` model for the sliders to work.  
- **Assign All Builder Fields:** The `JointUIBuilder` script will show an error if any of its public fields (Model Root, Prefab, etc.) are left empty. Double-check that all fields are assigned before generating.  
- **UI Must Live in a Canvas:** All UI elements in Unity must be children of a Canvas object to be visible. Our setup using a Scroll View automatically handles this.
- **Verification using provided images:** If issues persist after all troubleshooting steps, refer to the images in the "Images of Settings" folder included in this repo.  
