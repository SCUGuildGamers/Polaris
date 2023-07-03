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

    private AudioManager _audioManager;

    private void Start()
    {
        // Set the typing boolean
        _isTyped = false;

        // Initializing the dictionary
        quotes_dict = new Dictionary<string, string>(){
            {"GlideIntro", "The global conveyor belt slowly but steadily transports vital nutrients all over the world! This system of deep ocean currents depends on differences in density, as warm surface waters move downward and push nutrient-rich arctic waters upwards. (1)"},
            {"BasicGliding", "Despite their large size, Manta Rays are surprisingly agile. They maneuver with ease through the ocean by utilizing a mix of both powered strokes and unpowered glides. (2)"},
            {"BasicGliding2", "The remote nature of ocean caves yields astonishing evolutionary traits. The olm is a cave salamander which evolved to lose its eyes, with a life span of more than 100 years and the ability to resist starvation for up to eight years. (3)"},
            {"PipeParryIntro", "90% of plastic items are only used once and thrown out, resulting in a prevalence of plastic waste. Research suggests that microplastics are even spread by mosquitoes, as they remain embedded across mosquito life stages in different habitats! (4)"},
            {"ChasmLevel", "Pollution is found even in the deepest parts of the ocean, such as the Mariana Trench. Due to the absence of sunlight and low oxygen levels, debris which reaches the deep ocean takes far longer to degrade (sometimes even thousands of years)! (5)"},
            {"ChaoticFinal", "Global warming is changing how currents function. Increasingly warmer and faster surface waters are less willing to mix with deep ocean waters, negatively impacting the ocean’s ability to absorb heat and the temperature of marine ecosystems. (6)"},
            {"TheClimb", "Almost 37% of the world population lives in coastal areas. As coastal regions increasingly urbanize, poorly managed urban stormwater runoff endangers underwater ecosystems through chemical and nutrient pollution. (7)"},
            {"TitleProduction", "Thanks For Playing!"}
        };
        _audioManager = FindObjectOfType<AudioManager>();
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

        foreach (AudioSource a in _audioManager.FindAudioPlaying()){
            StartCoroutine(_audioManager.FadeAudio(a, transition_time));
        }
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
