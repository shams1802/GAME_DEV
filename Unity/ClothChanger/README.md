# Unity Animated Cloth Changer

## 📖 Overview
This project demonstrates a complete workflow for creating and switching between multiple pieces of animated clothing on a character in Unity.  

The system uses high-fidelity cloth simulations baked in **Blender** and exported to Unity using the **Alembic (.abc)** format. A simple UI button allows the user to toggle between different clothing items at runtime.  

This workflow is ideal for character customization systems where detailed, pre-baked cloth physics is desired.

---

## ✨ Features
- **Dynamic Clothing Switch**: Swap between different animated garments with a single button click.  
- **High-Fidelity Simulation**: Utilizes Blender’s robust cloth physics, baked into Alembic format for high performance in Unity.  
- **UI Control**: Simple UI implementation using a Canvas and Button to trigger the clothing change.  
- **Stationary & Animated Models**: Works for both stationary models and fully animated characters by parenting clothes to the skeleton.  

---

## 🛠 Workflow Summary
The process is divided into two main parts: **asset creation in Blender** and **implementation in Unity**.

### Blender
- Create and sew a 3D garment around a character model.  
# Unity SMPL-X Cloth Switcher (Alembic)

This Unity setup shows how to swap pre-baked cloth simulations on a character using Alembic animations and a simple UI button. Cloth motion is authored in Blender, baked, and exported as Alembic, then toggled in Unity at runtime.

---

## 📑 Table of Contents

* [How It Works](#how-it-works)
* [Getting Started: Project Setup](#getting-started-project-setup)

  1. [Prerequisites](#1-prerequisites)
  2. [Scene Preparation](#2-scene-preparation)

* [Step-by-Step Implementation Guide](#step-by-step-implementation-guide)

  * [Step 1: Blender – Bake and Export Alembic](#step-1-blender--bake-and-export-alembic)
  * [Step 2: Unity – Install Alembic Package](#step-2-unity--install-alembic-package)
  * [Step 3: Unity – Import Models and Alembic Files](#step-3-unity--import-models-and-alembic-files)
  * [Step 4: Unity – Scene Setup](#step-4-unity--scene-setup)
  * [Step 5: Unity – Controller Script](#step-5-unity--controller-script)
  * [Step 6: Unity – UI Button Wiring](#step-6-unity--ui-button-wiring)

* [Important Notes & Common Mistakes](#important-notes--common-mistakes)

---

## How It Works

Blender handles the cloth simulation and bakes the motion. The baked mesh is exported as Alembic (`.abc`) with scale set for Unity. In Unity, an Alembic component (or script-driven player) drives the animated cloth meshes. A simple C# script swaps active Alembic instances when the UI button is pressed.

---

## Getting Started: Project Setup

### 1. Prerequisites

- Unity project (2020.x or newer recommended) with a stable render pipeline and UI system available.
- Alembic package installed from the Unity Registry so `.abc` files can be played back correctly.
- Blender cloth simulations exported as Alembic with `Scale = 100` to maintain consistent world units.
- A character rig in Unity to parent garments to (works for stationary or fully animated characters).

### 2. Scene Preparation

- Open the target Unity scene and ensure lighting/camera framing make garments clearly visible.
- Confirm your character is present in the scene, using uniform scale `(1, 1, 1)` with transforms reset.
- Verify TextMeshPro is imported so button labels render properly in the UI.

---

## Step-by-Step Implementation Guide

### Step 1: Blender – Bake and Export Alembic

- Finalize and **Bake** the cloth simulation for each garment (e.g., `Shirt1`, `Shirt2`) to capture motion deterministically.
- Select only the animated cloth object prior to export to avoid bundling unintended meshes.
- Use File → Export → **Alembic (.abc)** with:
   - **Selected Objects** enabled so only the cloth exports
   - **Scale = 100** to match Unity’s default unit scale
- Export each garment to its own file (e.g., `Shirt1.abc`, `Shirt2.abc`) for modular swapping in Unity.

### Step 2: Unity – Install Alembic Package

- Open Window → Package Manager and switch scope to **Unity Registry**.
- Install **Alembic** so Unity can import and play `.abc` animation caches reliably.

### Step 3: Unity – Import Models and Alembic Files

- Drag the character FBX and all `.abc` garments into the Project to register assets.
- Check Alembic import settings (time range, normals, topology) and adjust if playback or shading appears incorrect.

### Step 4: Unity – Scene Setup

- Create an empty GameObject named `GameController` to host the switching script.
- Use a tidy hierarchy to keep character and garments organized:

```plaintext
Model (Parent Object)/
   ├── Ch36 (Character_Body_Mesh)
   └── mixamorig1:Hips (Armature)
Shirt1
Shirt2
GameController
Canvas
   └── Button
```

- Parent garments to the character rig (or relevant bone) if the character is animated so cloth follows the skeleton during playback.

### Step 5: Unity – Controller Script

- Create a C# script `AlembicClothingChanger` (or use the provided `AlembicClothingChanger.cs`).
- Attach the script to `GameController` so it is easy to find and manage.
- In the Inspector, assign garment references clearly:
   - `Shirt Player` → `Shirt1`
   - `Jacket Player` → `Shirt2`

### Step 6: Unity – UI Button Wiring

- Right-click Hierarchy → UI → **Button - TextMeshPro** (this auto-creates a Canvas and EventSystem if missing).
- Select the Button and configure **On Click()** events:
   - Click **+** to add a new event
   - Drag `GameController` into the object slot
   - Choose `AlembicClothingChanger → SwitchClothing()` from the dropdown

✅ Press **Play** and test the switch.

---

## Important Notes & Common Mistakes

### ❌ Clothes import tiny

*Cause:* Alembic exported without **Scale = 100**.

*Fix:* Re-export from Blender with `Scale = 100` and re-import.

---

### ❌ Animation not playing

*Cause:* Alembic package missing or player not updating time.

*Fix:* Install the Alembic package; ensure the Alembic component is active and time is driven (by player or script).

---

### ❌ Button does nothing

*Cause:* OnClick not wired or no EventSystem.

*Fix:* Re-link OnClick to `SwitchClothing()` and confirm EventSystem exists.

---

### ❌ Garments not following the character

*Cause:* Cloth objects not parented/aligned to the rig.

*Fix:* Parent garments under the character rig and match transforms before Play.

---

## Final Result

After setup, a single UI button swaps between Alembic-baked garments at runtime, retaining high-fidelity cloth motion while the character remains stationary or animated.