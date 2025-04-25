
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    // Public variables
    public static Game_Manager instance;
    [Header("Scene Objects")]
    public TMP_Text timerText;
    public Image gameOverlay;
    [Header("Scriptable Objects")]
    public Dialog_Requirement onRemembered;
    public Room boxRoom;
    public Room tharaciaRoom;
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip beginAudio;
    public AudioClip backgroundMusic;
    [Header("Gameplay Variables")]
    public float timerStartAmount = 5;
    public int probability = 20;

    // Private variables
    private float decayAmount = 0;
    private float attemptNumber = 1;
    private float timerRemainingAmount;
    private bool isFading;


    // Define a delegate called restart for the restart event
    public delegate void Restart();
    // Declare an event based on the delegate
    public event Restart onRestart;

    // Singleton for the game manager
    void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {        
        // initialize the audioSource
        audioSource = GetComponent<AudioSource>();

        // Add the on restart method to the on restart event
        onRestart += OnRestart;
        // Call the game restart to begin the game
        OnRestart();
    }

    // Called when the game is started / restarted
    void OnRestart() {
        // Play the begin game sound
        audioSource.PlayOneShot(beginAudio);
        // Begin the background music
        audioSource.PlayOneShot(backgroundMusic);

        // Set a random number based on the range of the probability variable
        int randomProbabilityNum = Random.Range(0, probability);

        // Define booleans to store the condition's status
        bool remembered = false;
        bool boxPresent = false;
        bool tharaciaPresent = false;

        // If the random probability number is 1, and the player is on their first attempt, they may "remember"
        // If on remember is satisfied, an additional dialog choice will be available to the player in part 1 of the dialog
        if (randomProbabilityNum == 1 && attemptNumber == 1) {
            remembered = true;
        }
        // if the random probability number is 2, the Box character may appear in the room containing puzzle six
        else if (randomProbabilityNum == 2) {
            boxPresent = true;
        }
        // if the randomd probability is 3, the tharacia character may appear in the north room
        else if (randomProbabilityNum == 3) {
            tharaciaPresent = true;
        }
        // Set the conditions based on the random boolean values
        onRemembered.isSatisfied = remembered;
        boxRoom.hasBox = boxPresent;
        tharaciaRoom.hasTharacia = tharaciaPresent;

        // If the the player has exceeded three attempts, the player loses
        if (attemptNumber > 3) {
            GameOver();
        }
        // Otherwise, restart the timer and start the fade in coroutine
        else {
            timerRemainingAmount = timerStartAmount * 60;

            StartCoroutine("FadeIn");
        }
    }

    void Update()
    {
        // The time between frames is subtracted from the remaining time every frame
        timerRemainingAmount -= Time.deltaTime;
        // Get the total remaining time in rounded seconds
        int totalSecondsRemaining = (int)timerRemainingAmount;

        // Get the remaining minutes from the total remaining seconds
        int minutesRemaining = totalSecondsRemaining / 60;
        // Get the remaining seconds for each minute from the total seconds
        int secondsRemaining = totalSecondsRemaining % 60;

        // Do not show negative seconds remainging after the timer reaches zero
        if (secondsRemaining < 0) {
            secondsRemaining = 0;
        }
        // Stringify seconds for formatting
        string secondsformatted = secondsRemaining.ToString("");
        
        // Append a zero ahead of the seconds if they are into the single digits
        if (secondsformatted.Length == 1) {
            secondsformatted = "0" + secondsformatted;
        }

        // Set the timer text with formatted minutes and seconds
        timerText.text = minutesRemaining + ":" + secondsformatted;

        // Begin fading out when the player has run out of time, don't run the restart if the player is already fading
        if (timerRemainingAmount <= 0 && isFading == false) {
            StartCoroutine(FadeOut("Restart"));
        }
    }

    // Called when the player completes all of the puzzles
    public void GameWon() {
        // Stop any current fading coroutines
        StopAllCoroutines();
        
        if (attemptNumber == 1) {
            // 1st attempt ending
        }
        else {
            // Other attempt ending
        }
        StartCoroutine(FadeOut("Win"));
    }

    // Called when the player does not complete the puzzles in time
    void GameLost() {
        // Increase the display haze
        decayAmount++;
        // Increase the attempt number
        attemptNumber++;
        // Call the restart event
        onRestart();
    }

    // Load the game over scene when the player loses
    void GameOver() {
        SceneManager.LoadScene("GameOver");
        Destroy(gameObject);
    }

    // Coroutine to gradually decrease the overlay alpha when the player starts / restarts
    IEnumerator FadeIn() {
        isFading = true;

        // While the alpha is below the max, increase the alpha gradually
        while (gameOverlay.color.a > decayAmount/3) {
            Color newColor = gameOverlay.color;
            newColor.a -= .05f;
            gameOverlay.color = newColor;
            yield return new WaitForSeconds(.1f);
        }

        isFading = false;
    }

    // Start coroutine to increse the alpha on the overlay, accepts an argument to decide what function to execute next
    IEnumerator FadeOut(string nextMethod) {
        isFading = true;

        // While the alpha is below the minimum, decrease the alpha gradually
        while (gameOverlay.color.a < 1) {
            Color newColor = gameOverlay.color;
            newColor.a += .025f;
            gameOverlay.color = newColor;
            yield return new WaitForSeconds(.1f);
        }
        isFading = false;

        // Stop any current audio
        audioSource.Stop();

        // If the string passed is restart, call the game lost method
        if (nextMethod == "Restart") {
            GameLost();
        }
        // If the string passed is Win, load the win screen for the first ending
        else if (nextMethod == "Win") {
            SceneManager.LoadScene("Ending_1");
            Destroy(gameObject);

        }
        // Log and error if the string passed to the argument is not valid
        else {
            Debug.LogError("Invalid argument passed to the Fade Out coroutine");
        }
    }
}
