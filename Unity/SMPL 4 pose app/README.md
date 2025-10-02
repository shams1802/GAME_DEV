# Unity Character Pose Controller  

This Unity project provides a complete system for applying pre-defined, full-body poses to a 3D model using a simple UI.  

It features an automated editor tool that generates all necessary pose data from a single script, making it incredibly fast to set up and customize.  

---

## 📑 Table of Contents  
- [How It Works](#how-it-works)  
- [Getting Started: Project Setup](#getting-started-project-setup)  
  1. [Create the Folder Structure](#1-create-the-folder-structure)  
  2. [Prepare the Scene](#2-prepare-the-scene)  
- [Step-by-Step Guide](#step-by-step-guide)  
  - [Step 1: Add and Configure the Scripts](#step-1-add-and-configure-the-scripts)  
  - [Step 2: Generate the Pose Assets](#step-2-generate-the-pose-assets)  
  - [Step 3: Set Up the Scene Controller](#step-3-set-up-the-scene-controller)  
  - [Step 4: Build the UI](#step-4-build-the-ui)  
  - [Step 5: Connect the UI to the Controller](#step-5-connect-the-ui-to-the-controller)  
- [Customization: Using Images on Buttons](#customization-using-images-on-buttons)  
- [Important Notes & Troubleshooting](#important-notes--troubleshooting)  

---

## How It Works  

The system is built from three key components that work together:  

### `PoseController.cs` (The Manager)  
This is the main script that manages the scene. It lives on a **PoseManager** object and holds references to the character model and all the possible poses. It contains the public functions that the UI buttons and slider call to update the model.  

### `PoseAsset` (The Data)  
This is not a script but a **ScriptableObject asset file**. Each PoseAsset stores all the bone rotation data needed for one complete, full-body pose (e.g., "Running", "Sitting"). This makes poses modular, reusable, and easy to edit.  

### `PoseAssetCreator.cs` (The Factory)  
This is a special **editor script** that provides the automation.  
It adds a new menu item in Unity that, when clicked, reads a list of pre-defined poses and automatically generates all the required PoseAsset files, ensuring every bone is accounted for to prevent posing errors.  

---

## Getting Started: Project Setup  

Follow these steps to set up the project from a new scene.  

### 1. Create the Folder Structure  
In your Unity project's **Assets** folder, create the following folders and place the provided files inside them:  



```plaintext
Assets/
├── Editor/
│ └── PoseAssetCreator.cs
│
├── Models/
│ └── SPML_male.fbx
│
├── Poses/
│ └── (The generator script will create assets here)
│
└── Scripts/
└── PoseController.cs
```



---

### 2. Prepare the Scene  
- Create a new 3D Scene in Unity (**File > New Scene**).  
- Drag the **SPML_male.fbx** model from the `Assets/Models` folder into the Hierarchy window.  
- Adjust your **Main Camera** and **Directional Light** so you can see the model clearly.  

---

## Step-by-Step Guide  

### Step 1: Add and Configure the Scripts  
- Place **PoseController.cs** in your `Assets/Scripts` folder.  
- Create the `Assets/Editor` folder and place **PoseAssetCreator.cs** inside it.  
- Unity must recompile, after which you'll see a new top menu.  

---

### Step 2: Generate the Pose Assets  
From the Unity menu bar, click:  
**Tools > Create FULL BODY Pose Assets**  

This will automatically create the `Assets/Poses` folder and fill it with all the `.asset` files defined in the PoseAssetCreator script.  

---

### Step 3: Set Up the Scene Controller  
1. Create an **empty GameObject** in the Hierarchy → name it **PoseManager**.  
2. Select **PoseManager**, then in the Inspector, click **Add Component** → add the **Pose Controller** script.  
3. Fill in the public fields by dragging objects:  
   - **Character Root** → drag the main SPML_male GameObject.  
   - **Root Bone For Slider** → expand SPML_male, drag the `m_avg_root` bone.  
   - **Poses** → lock the Inspector, select all generated PoseAssets, and drag them onto the Poses list.  

---

### Step 4: Build the UI  
1. **Canvas**: Right-click Hierarchy → **UI > Canvas** (an EventSystem is created automatically).  
2. **Button Panel**: Right-click Canvas → **UI > Panel** → name it **PosePanel**.  
   - Anchor it to middle-left.  
   - Add a **Vertical Layout Group** to stack the buttons.  
3. **Buttons**: Right-click PosePanel → **UI > Button - TextMeshPro** → create one per pose.  
4. **Slider**: Right-click Canvas → **UI > Slider** → name it **RotationSlider**.  
   - Anchor it bottom-center.  
   - Set **Min Value = -180** and **Max Value = 180**.  

---

### Step 5: Connect the UI to the Controller  
**Buttons**:  
- Select a button → in Inspector, go to **On Click ()**.  
- Click **+** → drag **PoseManager** into the slot.  
- From function dropdown: **PoseController → ApplyPose(int)**.  
- Enter `0, 1, 2...` for each pose (matching PoseManager's list order).  

**Slider**:  
- Select **RotationSlider** → in Inspector, find **On Value Changed (Single)**.  
- Click **+** → drag **PoseManager** into the slot.  
- From function dropdown: **PoseController → SetRootRotation(float)**.  

✅ Now press **Play** → The UI will be fully interactive.  

---

## Customization: Using Images on Buttons  
- Import your icon images into the project.  
- Select an image → set **Texture Type = Sprite (2D and UI)** → click **Apply**.  
- Select a button → expand it and delete its **Text** object.  
- On the button object → in the **Image** component, drag your sprite into **Source Image**.  

---

## Important Notes & Troubleshooting  
- **EventSystem is Required**: UI won’t respond without it (Unity auto-creates one).  
- **Full Body Poses**: PoseAssetCreator ensures every bone rotation is stored (avoids incorrect additive poses).  
- **Check Console**: If a button fails, look for missing bone errors in PoseAssetCreator.  
- **Assign All Fields**: Ensure Character Root, Root Bone, and Poses are properly assigned.  
