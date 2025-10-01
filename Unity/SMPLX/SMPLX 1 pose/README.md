use SMPL 4 pose app for this

use provided scripts in this folder only
use given smplx model set in SMPLX folder

you may use pose given in this folder
but it is make one yourself




# Now Adding Button for Shape Changing

Overview

We will update the PoseController.cs script to handle the logic for three distinct UI panels. Then, we will create the necessary panels and the control button in the Unity Editor and link them all together.



Part 1: Update the PoseController Script

First, we need to ensure our script has references to all three panels and the logic to toggle them correctly. The following code should be in your PoseController.cs script.





Of course. I will create a new README file that integrates the setup instructions for the three-panel system (PosePanel, ShapePanel, and ExpPanel) based on the code in the Canvas and the structure you provided.



Here is the new README file.



Part 2: Set Up the UI in the Unity Editor

Now, let's create the panels and the button to control them.



1\. Create the Panels

Locate PosePanel: You should already have a PosePanel that holds your pose buttons.



Create ShapePanel: In the Hierarchy, right-click on your Canvas and select UI -> Panel. Rename it to ShapePanel.



In the Rect Transform, make it the exact same size and position as your PosePanel so it covers it perfectly.



You can change its color in the Image component. A semi-transparent color works well.



Create ExpPanel: Right-click on your Canvas again and select UI -> Panel. Rename it to ExpPanel.



Position and size this panel as desired. This panel will appear alongside the ShapePanel. You can add other UI elements like sliders or buttons to it later.



2\. Create the Toggle Button

This button will control which set of panels is visible.



Right-click on your Canvas and select UI -> Button - TextMeshPro.



Rename it ToggleShape\_Button.



Position this button in a convenient, always-visible location.



Expand the button in the Hierarchy and select its child Text (TMP) object. In the Inspector, change its text to Shape.



3\. Connect Everything

This final step links all the new UI elements to the script.



Connect the Panels: Select your PoseManager object. In the Inspector, you will see three new fields:



Drag your PosePanel from the Hierarchy into the Pose Panel slot.



Drag your ShapePanel from the Hierarchy into the Shape Panel slot.



Drag your ExpPanel from the Hierarchy into the Exp Panel slot.



Connect the Button: Select your new ToggleShape\_Button. In the On Click () event:



Click the + icon.



Drag the PoseManager object into the object slot.



From the function dropdown, select PoseController -> TogglePoseUIVisibility ().



Final Result

Now, when you press Play, the scene will start with only the PosePanel visible. Clicking your "Shape" button will hide the PosePanel and simultaneously show both the ShapePanel and the ExpPanel. The character will also snap to a T-Pose. Clicking the "Shape" button again will reverse the process.

