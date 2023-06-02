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
    public float transition_time;

    // Quote text
    public Text quote_text;

    // Dictionary for storing the scene names relative to their quotes
    private Dictionary<string, string> quotes_dict;

    // Loading icon
    public GameObject loadingIcon;
    private int icon_count = 3;

    // Typing delay for quote
    private float _typingDelay = 0.03f;

    // Boolean value to keep track of when the typing is complete
    private bool _isTyped;

    private void Start()
    {
        // Set the typing boolean
        _isTyped = false;

        // Initializing the dictionary
        quotes_dict = new Dictionary<string, string>(){
            {"GlideIntro", "The Bridge"},
            {"BasicGliding", "Shallows"},
            {"BasicGliding2", "Grotto"},
            {"PipeParryIntro", "Dumping Grounds"},
            {"ChasmLevel", "Chasm"},
            {"ChaoticFinal", "Convergence"},
            {"TheClimb", "Dead Zone"},
            {"Conclusion", "Conclusion" }
        };

        StartCoroutine(RunTransition());
    }

    // Handles the running of the transition scene
    private IEnumerator RunTransition() {
        // Types the quote in the text field
        StartCoroutine(TypeSentence(GetQuote()));

        // Wait until the quote is typed to proceed
        yield return new WaitUntil(() => _isTyped);

        // Set and show the loading icon after the quote is typed
        SetRandomLoadingIcon();

        // Pause for transition
        yield return new WaitForSeconds(transition_time);

        // Load the next scene
        LoadNext();
    }

    // Loads the next scene
    private void LoadNext() {
        SceneManager.LoadScene(playerData.next_scene_string);
    }

    // Returns a quote depending on where the player is in the game
    private string GetQuote() {
        // Get the next scene
        string scene_name = playerData.next_scene_string;

        // Return the corresponding quote
        if (quotes_dict[scene_name] != null)
            return quotes_dict[scene_name];
        else
            return null;
    }

    // Set a random loading icon
    private void SetRandomLoadingIcon() {
        // Show the loading icon
        loadingIcon.SetActive(true);

        // Get a random integer to decide the loading icon
        int random_int = Random.Range(1, icon_count+1);

        // Set the state as the random integer
        loadingIcon.GetComponent<Animator>().SetInteger("loadingIconState", random_int);
    }

    // Animates the typing of the quote
    IEnumerator TypeSentence(string sentence)
    {
        quote_text.text = "";

        // Iterates through each character in the sentence, updating the dialogue and pausing per each iteration.
        foreach (char letter in sentence.ToCharArray())
        {
            quote_text.text += letter;

            // Typing delay
            yield return new WaitForSeconds(_typingDelay);
        }

        // Update the state of the typing
        _isTyped = true;
    }
}
