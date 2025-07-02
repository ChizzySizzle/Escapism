///
/// Gabriel Heiser
/// 4/29/25
/// The game manager controls the games state, keeps track of resets, controls the time remaining, and handles the restart event
/// It also handles win/lose state and scene management
/// There is a fun probability value that will currently yield 3 possible easter eggs
/// 

using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Game_Manager : MonoBehaviour
{
    // Public variables
    public static Game_Manager instance;
    [Header("Scene Objects")]
    public TMP_Text timerText;
    public Image gameOverlay;
    public GameObject pauseMenu;
    [Header("Scriptable Objects")]
    public Dialog_Requirement onRemembered;
    public Room boxRoom;
    public Room tharaciaRoom;
    [Header("Audio")]
    public AudioSource musicSource;
    public AudioSource effectSource;
    public AudioClip beginAudio;
    public AudioClip backgroundMusic;
    [Header("Gameplay Variables")]
    public float timerStartAmount = 5;
    public int probability = 20;

    // Private variables
    public int decayAmount = 0;
    public string playerName = "player";
    private int attemptNumber = 1;
    private float timerRemainingAmount;
    private bool isFading;
    private bool gameRunning = true;

    public static event Action onGameClosed;

    // Singleton for the game manager
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Called when the game is started / restarted
    public void GameStart()
    {
        Chizzy_Dialog_Controller.instance.UpdateStartDialog(attemptNumber);

        // Set the timer
        timerRemainingAmount = timerStartAmount * 60;

        // Play the begin game sound
        effectSource.PlayOneShot(beginAudio);
        // Begin the background music
        musicSource.PlayOneShot(backgroundMusic);

        // Set a random number based on the range of the probability variable
        int randomProbabilityNum = UnityEngine.Random.Range(0, probability);

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

        // Start the fade in coroutine
        StartCoroutine("FadeIn");
    }

    void Update()
    {
        if (gameRunning)
        {
            // The time between frames is subtracted from the remaining time every frame
            timerRemainingAmount -= Time.deltaTime;
        }

        // Get the total remaining time in rounded seconds
        int totalSecondsRemaining = (int)timerRemainingAmount;

        // Get the remaining minutes from the total remaining seconds
        int minutesRemaining = totalSecondsRemaining / 60;
        // Get the remaining seconds for each minute from the total seconds
        int secondsRemaining = totalSecondsRemaining % 60;

        // Do not show negative seconds remainging after the timer reaches zero
        if (secondsRemaining < 0)
        {
            secondsRemaining = 0;
        }
        // Stringify seconds for formatting
        string secondsformatted = secondsRemaining.ToString("");

        // Append a zero ahead of the seconds if they are into the single digits
        if (secondsformatted.Length == 1)
        {
            secondsformatted = "0" + secondsformatted;
        }

        // Set the timer text with formatted minutes and seconds
        timerText.text = minutesRemaining + ":" + secondsformatted;

        // Begin fading out when the player has run out of time, don't run the restart if the player is already fading
        if (timerRemainingAmount <= 0 && isFading == false)
        {
            StartCoroutine(FadeOut("Restart"));
        }
    }

    public int GetCurrentAttempts()
    {
        return attemptNumber;
    }

    // Called when the player completes all of the puzzles
    public void GameWon()
    {
        // Stop any current fading coroutines
        StopAllCoroutines();

        if (attemptNumber == 1)
        {
            // 1st attempt ending
        }
        else
        {
            // Other attempt ending
        }
        StartCoroutine(FadeOut("Win"));
    }

    // Called when the player does not complete the puzzles in time
    void Restart()
    {
        if (attemptNumber > 2)
        {
            GameOver();
        }
        else
        {
            // Increase the display haze
            decayAmount++;
            // Increase the attempt number
            attemptNumber++;

            Chizzy_Dialog_Controller.instance.SetLastMessage();
            Scene_Manager.instance.ResetScene();
        }
    }

    // Load the game over scene when the player loses
    void GameOver()
    {
        Scene_Manager.instance.GoToLoseScreen();
    }

    // Coroutine to gradually decrease the overlay alpha when the player starts / restarts
    IEnumerator FadeIn()
    {
        isFading = true;

        // While the alpha is below the max, increase the alpha gradually
        while (gameOverlay.color.a > decayAmount / 3)
        {
            Color newColor = gameOverlay.color;
            newColor.a -= .05f;
            gameOverlay.color = newColor;
            yield return new WaitForSeconds(.1f);
        }

        isFading = false;
    }

    // Start coroutine to increse the alpha on the overlay, accepts an argument to decide what function to execute next
    IEnumerator FadeOut(string nextMethod)
    {
        isFading = true;

        // While the alpha is below the minimum, decrease the alpha gradually
        while (gameOverlay.color.a < 1)
        {
            Color newColor = gameOverlay.color;
            newColor.a += .025f;
            gameOverlay.color = newColor;
            yield return new WaitForSeconds(.1f);
        }
        isFading = false;

        // Stop any current audio
        musicSource.Stop();
        effectSource.Stop();

        // If the string passed is restart, call the game lost method
        if (nextMethod == "Restart")
        {
            Restart();
        }
        // If the string passed is Win, load the win screen for the first ending
        else if (nextMethod == "Win")
        {
            Scene_Manager.instance.GoToWinScreen();
        }
        // Log and error if the string passed to the argument is not valid
        else
        {
            Debug.LogError("Invalid argument passed to the Fade Out coroutine");
        }
    }

    public void CloseGame()
    {
        if (Runtime_UI_Objects.instance != null)
        {
            Destroy(Runtime_UI_Objects.instance.gameObject);
        }
        Destroy(gameObject);
    }

    public void EscapePressed()
    {
        if (Puzzle_Manager.instance != null && Puzzle_Manager.instance.inPuzzle)
        {
            Puzzle_Manager.instance.ClosePuzzle();
        }
        else if (Dialog_Manager.instance != null && Dialog_Manager.instance.inDialog)
        {
            Dialog_Manager.instance.EndDialog();
        }
        else if (pauseMenu.activeSelf == false)
        {
            pauseMenu.SetActive(true);
            gameRunning = false;
        }
        else if (pauseMenu.activeSelf == true)
        {
            pauseMenu.SetActive(false);
            gameRunning = true;
        }
    }
}