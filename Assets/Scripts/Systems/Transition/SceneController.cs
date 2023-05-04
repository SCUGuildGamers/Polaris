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
        SceneManager.LoadScene(transition_scene_string);
    }
}
