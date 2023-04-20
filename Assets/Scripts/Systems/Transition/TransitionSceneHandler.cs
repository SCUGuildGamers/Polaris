using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionSceneHandler : MonoBehaviour
{
    // Next scene string
    public PlayerData playerData;

    // Transition time for scene
    private float transition_time = 10f;

    // Quote text
    public Text quote_text;

    private List<string> quote_list;

    private void Start()
    {
        quote_list = new List<string> {"Hello world1", "Hello world2"};

        StartCoroutine(RunTransition());
    }

    // Handles the running of the transition scene
    private IEnumerator RunTransition() {
        // Set the text for the quote
        quote_text.text = GetRandomQuote();

        // Pause for transition
        yield return new WaitForSeconds(transition_time);

        // Load the next scene
        LoadNext();
    }

    private void LoadNext() {
        SceneManager.LoadScene(playerData.next_scene_string);
    }

    private string GetRandomQuote() {
        // Generate a random integer to choose one of the quotes
        int random_int = Random.Range(0, quote_list.Count);

        return quote_list[random_int];
    }
}
