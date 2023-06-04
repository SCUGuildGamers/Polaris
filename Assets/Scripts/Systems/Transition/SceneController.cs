using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public PlayerData playerData;

    private string transition_scene_string = "TransitionScene";

    // Changes scenes with a transition between the scene change
    public void ChangeSceneTransition(string next_scene_string) {
        // Transfer the next scene string and quote so the transition properly works
        playerData.next_scene_string = next_scene_string;

        // Load the transition scene
        SceneManager.LoadScene(transition_scene_string);
    }

    // Changes scene directly with no transition
    public void ChangeScene(string next_scene_string) {
        SceneManager.LoadScene(next_scene_string);
    }

    // Reload current level and restore player health
    public void Reload()
    {
        // Restore player health
        playerData.player_health = playerData.max_player_health;

        // Reset player glides
        FindObjectOfType<GlideCharge>().ResetCharges();

        // Increase death counter
        playerData.death_counter++;

        // Reload the current scene
        SceneManager.LoadScene(playerData.checkpoint_string);
    }
}
