using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public PlayerData playerData;

    private string transition_scene_string = "TransitionScene";

    public void ChangeScene(string next_scene_string, string quote_text) {
        // Transfer the next scene string and quote so the transition properly works
        playerData.next_scene_string = next_scene_string;
        playerData.transition_quote_text = quote_text;

        // Load the transition scene
        SceneManager.LoadScene(transition_scene_string);
    }
}
