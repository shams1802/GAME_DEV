\## Full Project Setup Guide  



This guide explains how to set up a character with two animations (run and jump) and switch between them using a UI button. The character will run by default, and each button press will toggle between the running and jumping animations.  



---



\### Step 1: Configure Your 3D Models and Animations  



This is the most important step to ensure animations can be shared.  



1\. \*\*Select Running.fbx\*\*: In your Project window, click on this file.  

2\. Go to the Inspector and select the \*\*Rig\*\* tab.  

3\. Set \*\*Animation Type\*\* to \*\*Humanoid\*\*.  

4\. Click \*\*Apply\*\*. This creates an Avatar, which is a map of your model's skeleton.  



5\. \*\*Select Jump.fbx\*\*: Now click on this file in the Project window.  

6\. Go to the \*\*Rig\*\* tab in the Inspector.  

7\. Set \*\*Animation Type\*\* to \*\*Humanoid\*\*.  

8\. For \*\*Avatar Definition\*\*, choose \*\*Copy From Other Avatar\*\*.  

9\. Drag the Avatar from your \*\*Running.fbx\*\* file (it has a little person icon) into the \*\*Source\*\* slot.  

10\. Click \*\*Apply\*\*.  



11\. \*\*Enable Looping\*\*: Expand both \*\*Running.fbx\*\* and \*\*Jump.fbx\*\* to see the animation clips inside.  

&nbsp;   - Select each animation clip, go to the Inspector, and check the \*\*Loop Time\*\* box.  

&nbsp;   - Click \*\*Apply\*\*.  



---



\### Step 2: Set Up the Animator Controller  



Your \*\*PlayerAnimator.controller\*\* file is the brain that decides which animation to play.  



1\. \*\*Create the Animator\*\*: Right click in Assets folder, Create -> Animation -> Animation Controller, rename it PlayerAnimator.  

2\. \*\*Create a Parameter\*\*:

&nbsp;  - Select PlayerAnimator, in Inspector click open.  

&nbsp;  - In the top-left of the Animator window, click the \*\*Parameters\*\* tab.  

&nbsp;  - Click the \*\*+\*\* icon and select \*\*Bool\*\*.  

&nbsp;  - Name this parameter exactly `isJumping`, check the box beside it.  

4\. \*\*Add Animations\*\*:  

&nbsp;  - Drag the run animation clip (from inside \*\*Running.fbx\*\*) and the jump animation clip (from inside \*\*Jump.fbx\*\*) into the Animator window.  

&nbsp;  - The run state should be orange, making it the default.  

5\. \*\*Create Transitions\*\*:  

&nbsp;  - \*\*Run to Jump\*\*: Right-click the run state → \*\*Make Transition\*\* → click on the jump state.  

&nbsp;    - Select the new transition arrow. In the Inspector, uncheck \*\*Has Exit Time\*\*.  

&nbsp;    - Under \*\*Conditions\*\*, add a new condition: `isJumping` is \*\*true\*\*.  

&nbsp;  - \*\*Jump to Run\*\*: Right-click the jump state → \*\*Make Transition\*\* → click on the run state.  

&nbsp;    - Select this arrow, uncheck \*\*Has Exit Time\*\*, and set its condition to `isJumping` is \*\*false\*\*.  



---



\### Step 3: Build the Scene  



Now, let's put the character and the button into your game world.  



1\. \*\*Add the Character\*\*: Drag your \*\*Running.fbx\*\* model from the Project window into the Hierarchy window. This places your character in the scene.  

2\. \*\*Assign the Controller\*\*:  

&nbsp;  - Select your character (named \*\*Running\*\*) in the Hierarchy.  

&nbsp;  - In the Inspector, find the \*\*Animator\*\* component.  

&nbsp;  - Drag your \*\*PlayerAnimator.controller\*\* asset into the \*\*Controller\*\* slot.  

3\. \*\*Add the Button\*\*:  

&nbsp;  - Go to the top menu and click \*\*GameObject > UI > Button\*\*. This creates a Canvas and a Button.  

&nbsp;  - Use the \*\*Rect Tool\*\* (shortcut: T) to position the button where you want it on the screen.  



---



\### Step 4: Connect the Script and Button  



This final step "wires" everything together so it works.  



1\. \*\*Attach the Script\*\*:  

&nbsp;  - Select your \*\*Running\*\* character in the Hierarchy.  

&nbsp;  - Drag your \*\*AnimationSwitcher.cs\*\* script from the Project window onto the Inspector for the \*\*Running\*\* object.  

2\. \*\*Link the Animator to the Script\*\*:  

&nbsp;  - Look at the \*\*Animation Switcher (Script)\*\* component you just added.  

&nbsp;  - It has an empty slot called \*\*Player Animator\*\*.  

&nbsp;  - Drag the \*\*Animator\*\* component (drag running from your hierarchy) into this slot.  

3\. \*\*Link the Button to the Script\*\*:  

&nbsp;  - Select the \*\*Button\*\* in your Hierarchy.  

&nbsp;  - In the Inspector, find the \*\*On Click ()\*\* panel. Click the \*\*+\*\* icon.  

&nbsp;  - Drag your \*\*Running\*\* character from the Hierarchy into the object slot that says "None (Object)".  

&nbsp;  - Click the "No Function" dropdown and select \*\*AnimationSwitcher > ToggleAnimation()\*\*.  



✅ Now you can press \*\*Play\*\*. The project is fully set up.  



