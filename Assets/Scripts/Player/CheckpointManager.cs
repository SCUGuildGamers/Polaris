using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public PlayerData playerData;

    public string checkpoint_string;

    // On start, update the checkpoint to the current level if applicable
    private void Start()
    {
        if (!string.IsNullOrEmpty(checkpoint_string))
            UpdateCheckpoint(checkpoint_string);
    }

    // Update the checkpoint given a checkpoint_string
    public void UpdateCheckpoint(string checkpoint_string) {
        playerData.checkpoint_string = checkpoint_string;
    }

    // Return the player to the last checkpoint
    public void ReturnToCheckpoint() {
        SceneManager.LoadScene(playerData.checkpoint_string);
    }
}
