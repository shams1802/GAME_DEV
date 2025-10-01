using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
    public Animator playerAnimator;
    private bool isJumpingState = false;

    // This function will be called by the button
    public void ToggleAnimation()
    {
        // Flip the boolean state
        isJumpingState = !isJumpingState;

        // Update the Animator's parameter
        playerAnimator.SetBool("isJumping", isJumpingState);
    }
}