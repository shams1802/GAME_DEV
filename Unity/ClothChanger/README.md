\# Unity Animated Cloth Changer



\## 📖 Overview

This project demonstrates a complete workflow for creating and switching between multiple pieces of animated clothing on a character in Unity.  



The system uses high-fidelity cloth simulations baked in \*\*Blender\*\* and exported to Unity using the \*\*Alembic (.abc)\*\* format. A simple UI button allows the user to toggle between different clothing items at runtime.  



This workflow is ideal for character customization systems where detailed, pre-baked cloth physics is desired.



---



\## ✨ Features

\- \*\*Dynamic Clothing Switch\*\*: Swap between different animated garments with a single button click.  

\- \*\*High-Fidelity Simulation\*\*: Utilizes Blender’s robust cloth physics, baked into Alembic format for high performance in Unity.  

\- \*\*UI Control\*\*: Simple UI implementation using a Canvas and Button to trigger the clothing change.  

\- \*\*Stationary \& Animated Models\*\*: Works for both stationary models and fully animated characters by parenting clothes to the skeleton.  



---



\## 🛠 Workflow Summary

The process is divided into two main parts: \*\*asset creation in Blender\*\* and \*\*implementation in Unity\*\*.



\### Blender

\- Create and sew a 3D garment around a character model.  

\- Configure cloth physics simulation and bake it.  

\- Export the final animated mesh as \*\*Alembic (.abc)\*\* with `Scale = 100`.  



\### Unity

\- Install the \*\*Alembic package\*\* via Package Manager.  

\- Import the character and `.abc` clothing files into the project.  

\- Use the \*\*AlembicClothingChanger.cs\*\* script to manage clothing swaps.  

\- Hook up a \*\*UI Button\*\* to trigger the clothing switch.  



---



\## 🚀 Setup \& Usage Guide



\### Step 1: Blender – Asset Preparation

For each piece of clothing (e.g., `Shirt1`, `Shirt2`):  

1\. Finalize and \*\*Bake\*\* the cloth simulation.  

2\. Select the animated cloth object only.  

3\. Go to \*\*File > Export > Alembic (.abc)\*\*.  

&nbsp;  - Enable \*\*Selected Objects\*\*.  

&nbsp;  - Set \*\*Scale = 100\*\*.  

4\. Export each clothing item separately (`Shirt1.abc`, `Shirt2.abc`).  



---



\### Step 2: Unity – Project Setup

1\. Open \*\*Window > Package Manager\*\*.  

2\. Set the filter to \*\*Unity Registry\*\*.  

3\. Install the \*\*Alembic package\*\*.  



---



\### Step 3: Unity – Scene Setup

1\. Drag your character model and `.abc` clothing files into the \*\*Project window\*\*.  

2\. Create an empty \*\*GameObject\*\* called `GameController` for the script.  

3\. Suggested hierarchy structure:  





```plaintext

Model (Parent Object)/

&nbsp;   ├── Ch36 (Character\_Body\_Mesh)

&nbsp;   └── mixamorig1:Hips(Armature)

Shirt1

Shirt2

GameController

Canvas

&nbsp;    └── Button

```









---



\### Step 4: Unity – Controller Script

1\. In Project, right-click → \*\*Create > C# Script\*\* → name it `AlembicClothingChanger`.  

2\. Paste the script (from `AlembicClothingChanger.cs`).  

3\. Attach it to `GameController`.  

4\. In the Inspector:  

&nbsp;  - Assign \*\*Shirt1\*\* to `Shirt Player`.  

&nbsp;  - Assign \*\*Shirt2\*\* to `Jacket Player`.  



---



\### Step 5: Unity – UI Button Setup

1\. Right-click in Hierarchy → \*\*UI > Button - TextMeshPro\*\*.  

&nbsp;  - This creates a \*\*Canvas\*\*, \*\*EventSystem\*\*, and \*\*Button\*\*.  

2\. Select the Button → In Inspector → \*\*On Click()\*\* panel.  

3\. Add a new event (+).  

4\. Drag `GameController` into the slot.  

5\. From the dropdown, choose:  

&nbsp;  `AlembicClothingChanger > SwitchClothing()`  



✅ Now press \*\*Play\*\* and test the button.  



---



\## ⚡ Troubleshooting

\- \*\*Clothes are tiny\*\* → Forgot to set \*\*Scale = 100\*\* during Blender export.  

\- \*\*Animation doesn’t play\*\* → Ensure \*\*Alembic package\*\* is installed and script is updating animation time.  

\- \*\*Button does nothing\*\* →  

&nbsp; - Verify OnClick is linked to `SwitchClothing()`.  

&nbsp; - Ensure \*\*EventSystem\*\* exists in the scene.  



---



