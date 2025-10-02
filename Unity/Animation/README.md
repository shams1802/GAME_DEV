# 🎮 Full Project Setup Guide

This guide explains how to set up a character with **two animations** (Run and Jump) and switch between them using a **UI button**.  
The character runs by default, and each button press toggles between **Running** and **Jumping**.

---

## 🏗 Step 1: Configure Your 3D Models and Animations

This is the most important step to ensure animations are shareable.

1. **Select `Running.fbx`** → In the Project window.  
2. Go to the **Inspector → Rig tab**.  
3. Set **Animation Type** → `Humanoid`.  
4. Click **Apply** → Creates an Avatar (skeleton map).  

5. **Select `Jump.fbx`** → In the Project window.  
6. Go to the **Rig tab** in Inspector.  
7. Set **Animation Type** → `Humanoid`.  
8. For **Avatar Definition**, choose **Copy From Other Avatar**.  
9. Drag the Avatar from `Running.fbx` into the **Source** slot.  
10. Click **Apply**.  

11. **Enable Looping**:  
   - Expand both `Running.fbx` and `Jump.fbx` to view clips.  
   - Select each animation → In Inspector, check **Loop Time**.  
   - Click **Apply**.  

---

## 🎛 Step 2: Set Up the Animator Controller

The **PlayerAnimator.controller** decides which animation plays.

1. **Create Animator Controller**  
   - Right-click in Assets → **Create → Animation → Animator Controller**  
   - Rename to **PlayerAnimator**  

2. **Create Parameter**  
   - Select `PlayerAnimator` and open it.  
   - In Animator window, go to **Parameters tab**.  
   - Click **+ → Bool**.  
   - Name it exactly: `isJumping`.  
   - (Optional) Check the box beside it for debugging.  

3. **Add Animations**  
   - Drag **Run clip** (from inside `Running.fbx`) and **Jump clip** (from inside `Jump.fbx`) into the Animator window.  
   - The Run state should be **orange** (default).  

4. **Create Transitions**  
   - **Run → Jump**:  
     - Right-click Run → **Make Transition → Jump**.  
     - Select arrow → In Inspector: Uncheck **Has Exit Time**.  
     - Under **Conditions**, set `isJumping = true`.  
   - **Jump → Run**:  
     - Right-click Jump → **Make Transition → Run**.  
     - Select arrow → Uncheck **Has Exit Time**.  
     - Condition: `isJumping = false`.  

---

## 🌍 Step 3: Build the Scene

1. **Add Character**  
   - Drag `Running.fbx` into the Hierarchy → places character in scene.  

2. **Assign Controller**  
   - Select character (Running).  
   - In **Inspector → Animator component**, drag in **PlayerAnimator.controller**.  

3. **Add Button**  
   - Menu: **GameObject → UI → Button**.  
   - This creates a **Canvas** + **Button**.  
   - Use **Rect Tool (T)** to position on screen.  

---

## 🔌 Step 4: Connect Script and Button

1. **Attach Script**  
   - Select `Running` character.  
   - Drag **AnimationSwitcher.cs** onto Inspector.  

2. **Link Animator to Script**  
   - In **Animation Switcher (Script)**, find **Player Animator slot**.  
   - Drag the **Animator component** of Running into this slot.  

3. **Link Button to Script**  
   - Select the **Button** in Hierarchy.  
   - In **Inspector → On Click ()** panel, click **+**.  
   - Drag `Running` character into the slot.  
   - From the dropdown, choose:  
     **AnimationSwitcher → ToggleAnimation()**.  

---

✅ Press **Play** → Button now toggles between **Run** and **Jump**!
