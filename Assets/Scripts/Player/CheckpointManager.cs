using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    public PlayerData playerData;

    private void Start() 
    {
        // Update the checkpoint with the current scene
        UpdateCheckpoint(SceneManager.GetActiveScene().name);
    }

    // Update the checkpoint given a checkpoint_string
    private void UpdateCheckpoint(string checkpoint_string) 
    {
        playerData.checkpoint_string = checkpoint_string;
    }

    // Return the player to the last checkpoint
    public void ReturnToCheckpoint() 
    {
        SceneManager.LoadScene(playerData.checkpoint_string);
    }
}
