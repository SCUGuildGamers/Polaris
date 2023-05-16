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

    private List<string> ocean_quote_list;
    private List<string> pollution_quote_list;

    private List<string> pollution_scenes;

    // Loading icon
    public GameObject loadingIcon;
    private int icon_count = 3;

    private void Start()
    {
        // Initialize quotes in list
        ocean_quote_list = new List<string> { "The ocean covers 71% of the Earth's surface - USGS", "According to World Register of Marine Species, there are currently at least 236,878 named marine species - WoRMS", "The record for the deepest free dive is held by Jacques Mayol. He dove to an astounding depth of 86m (282ft) without any breathing equipment - MarineBio", "The Atlantic Ocean is the youngest of the five oceans, having formed during the Jurassic Period approximately 150 million years ago following the breakup of the supercontinent Pangaea - Britannica" };
        pollution_quote_list = new List<string> { "More than 8 million tons of plastic enter the oceans every year - Earth", "Ocean plastic pollution Is on track to triple by 2060 and exceed one billion tons of plastic in the ocean - Earth", "In 2014, California became the first state to ban plastic bags. As of March 2018, 311 local bag ordinances have been adopted in 24 states, including Hawaii. As of July 2018, 127 countries have adopted some form of legislation to regulate plastic bags - WRI", "50 percent of all sea turtles, 44 percent of all seabirds, 22 percent of all cetaceans, and a long list of fish species have already eaten plastics - SurferToday" };

        // Initialize scenes in pollution list
        pollution_scenes = new List<string> { "ChaoticFinal", "TheClimb"};

        StartCoroutine(RunTransition());

        // Set random loading icon
        SetRandomLoadingIcon(); 
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

    // Loads the next scene
    private void LoadNext() {
        SceneManager.LoadScene(playerData.next_scene_string);
    }

    // Returns a random quote depending on where the player is in the game
    private string GetRandomQuote() {
        if (IsPollutionQuote())
        {
            int random_int = Random.Range(0, pollution_quote_list.Count);
            return pollution_quote_list[random_int];
        }

        else {
            int random_int = Random.Range(0, ocean_quote_list.Count);
            return ocean_quote_list[random_int];
            
        }
    }

    // Checks if the scene is a ocean-pollution relevant scene or not
    private bool IsPollutionQuote() {
        if (pollution_scenes.Contains(playerData.next_scene_string))
            return true;

        return false;
    }

    // Set a random loading icon
    private void SetRandomLoadingIcon() {
        // Get a random integer to decide the loading icon
        int random_int = Random.Range(1, icon_count+1);

        // Set the state as the random integer
        loadingIcon.GetComponent<Animator>().SetInteger("loadingIconState", random_int);
    }
}
