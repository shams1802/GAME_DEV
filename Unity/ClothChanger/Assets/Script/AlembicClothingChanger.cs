using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
using System.Collections;

public class AlembicClothingChanger : MonoBehaviour
{
    public AlembicStreamPlayer shirtPlayer;
    public AlembicStreamPlayer jacketPlayer;

    private Coroutine currentAnimationCoroutine;
    private bool isShirtCurrentlyActive;

    void Start()
    {
        Debug.Log("Game Started! Setting up initial clothing.");

        jacketPlayer.gameObject.SetActive(false);
        shirtPlayer.gameObject.SetActive(true);
        isShirtCurrentlyActive = true;

        PlayAnimation(shirtPlayer);
    }

    public void SwitchClothing()
    {
        Debug.Log("SwitchClothing button was clicked!");

        if (currentAnimationCoroutine != null)
        {
            StopCoroutine(currentAnimationCoroutine);
        }

        if (isShirtCurrentlyActive)
        {
            Debug.Log("Switching to JACKET.");
            shirtPlayer.gameObject.SetActive(false);
            jacketPlayer.gameObject.SetActive(true);
            isShirtCurrentlyActive = false;

            PlayAnimation(jacketPlayer);
        }
        else
        {
            Debug.Log("Switching to SHIRT.");
            jacketPlayer.gameObject.SetActive(false);
            shirtPlayer.gameObject.SetActive(true);
            isShirtCurrentlyActive = true;

            PlayAnimation(shirtPlayer);
        }
    }

    private void PlayAnimation(AlembicStreamPlayer player)
    {
        player.CurrentTime = 0f;
        currentAnimationCoroutine = StartCoroutine(PlayAlembicAnimation(player));
    }

    // --- MODIFIED COROUTINE ---
    IEnumerator PlayAlembicAnimation(AlembicStreamPlayer player)
    {
        Debug.Log($"Starting animation for {player.gameObject.name}");

        // Loop ONLY while the current time is less than the animation's total duration
        while (player.CurrentTime < player.Duration)
        {
            player.CurrentTime += Time.deltaTime;
            yield return null;
        }

        // After the loop, clamp the time to the exact end to ensure it's on the last frame
        player.CurrentTime = player.Duration;
        Debug.Log($"Animation finished and paused at the end for {player.gameObject.name}");
    }
}