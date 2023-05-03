using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public PlayerData playerData;

    private string transition_scene_string = "TransitionScene";

    public void ChangeScene(string next_scene_string) {
        // Transfer the next scene string and quote so the transition properly works
        playerData.next_scene_string = next_scene_string;

        // Update the respawn checkpoint
        FindObjectOfType<CheckpointManager>().UpdateCheckpoint(next_scene_string);

        // Load the transition scene
        SceneManager.LoadScene(transition_scene_string);
    }
}
