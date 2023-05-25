using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PreLevelController : MonoBehaviour
{
    // References for changing the pre level UI
    public GameObject PreLevelUI;

    public Text TitleText;

    // Typing delay
    private float _typingDelay = 0.03f;

    // Dictionary for storing the scene names relative to the titles
    private Dictionary<string, string> titles_dict;

    private void Start() {
        // Initializing the dictionary
        titles_dict = new Dictionary<string, string>(){
            {"GlideIntro", "GlideIntro"},
            {"BasicGliding", "BasicGliding"},
            {"BasicGliding2", "BasicGliding2"},
            {"PipeParryIntro", "PipeParryIntro"},
            {"ChasmLevel", "ChasmLevel"},
            {"ChaoticFinal", "ChaoticFinal"},
            {"TheClimb", "TheClimb"}
        };

        InitializeScene();
    }

    // Update the text with the title
    private void InitializeScene() {
        // Update the title with the corresponding text
        string scene_name = SceneManager.GetActiveScene().name;
        Debug.Log(scene_name);
        string level_name = titles_dict[scene_name];
        StartCoroutine(TypeSentence(level_name));
    }

    // Animates the typing of the quote
    IEnumerator TypeSentence(string sentence)
    {
        TitleText.text = "";

        // Iterates through each character in the sentence, updating the dialogue and pausing per each iteration.
        foreach (char letter in sentence.ToCharArray())
        {
            TitleText.text += letter;

            // Typing delay
            yield return new WaitForSeconds(_typingDelay);
        }
    }
}
