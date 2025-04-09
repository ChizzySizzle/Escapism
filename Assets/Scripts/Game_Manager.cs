
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    // public references
    public static Game_Manager instance;
    public TMP_Text timerText;
    public Image gameOverlay;
    public Dialog_Requirement onRemembered;
    public Room boxRoom;
    public float timerStartAmount = 5;

    // Private variables
    public int probability = 20;
    public float decayAmount = 0;
    public float attemptNumber = 1;
    private float timerRemainingAmount;
    private bool isFading;

    public delegate void Restart();
    public event Restart onRestart;

    // Singleton
    void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }
    
    void Start()
    {
        onRestart += OnRestart;
        OnRestart();
    }

    void OnRestart() {

        int randomNum = Random.Range(0, probability);

        if (randomNum == 1 && attemptNumber == 1) {
            onRemembered.isSatisfied = true;
        }
        else if (randomNum == 2) {
            boxRoom.hasBox = true;
        }
        else {
            onRemembered.isSatisfied = false;
            boxRoom.hasBox = false;
        }

        if (decayAmount > 2) {
            GameOver();
        }
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

        // Begin fading out when the player has run out of time
        if (timerRemainingAmount <= 0 && isFading == false) {
            StartCoroutine(FadeOut("Restart"));
        }
    }

    // Called when the player completes all of the puzzles
    public void GameWon() {
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
        decayAmount++;
        attemptNumber++;
        onRestart();
    }

    void GameOver() {
        SceneManager.LoadScene("GameOver");
    }

    IEnumerator FadeIn() {
        isFading = true;

        while (gameOverlay.color.a > decayAmount/4) {
            Color newColor = gameOverlay.color;
            newColor.a -= .05f;
            gameOverlay.color = newColor;
            yield return new WaitForSeconds(.1f);
        }

        isFading = false;
    }

    IEnumerator FadeOut(string nextMethod) {
        isFading = true;

        while (gameOverlay.color.a < 1) {
            Color newColor = gameOverlay.color;
            newColor.a += .025f;
            gameOverlay.color = newColor;
            yield return new WaitForSeconds(.1f);
        }
        isFading = false;

        if (nextMethod == "Restart") {
            GameLost();
        }
        if (nextMethod == "Win") {
            SceneManager.LoadScene("Ending_1");
        }
    }
}
