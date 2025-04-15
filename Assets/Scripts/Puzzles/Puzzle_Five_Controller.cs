
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Five_Controller : Puzzle
{
    public Scrollbar[] scrollbars;
    public Button submitButton;
    public string playerInput;

    private TMP_Text puzzleFiveNumber;
    private int lastValueChange = 0;

    public override void Start() {
        base.Start();
        puzzleFiveNumber = Navigation_Manager.instance.puzzleFiveNumber.GetComponent<TMP_Text>();

        submitButton.onClick.AddListener(SubmitInput);

        OnRestart();
    }

    public override void OnRestart() {
        string randomString = "";
        puzzleFiveNumber.text = "";

        for (int i = 0; i < scrollbars.Length; i++) {
            int randomNum = Random.Range(0,6);
            randomString += randomNum;
            puzzleFiveNumber.text += randomNum + "\n";
        }

        keyString = randomString;
    }

    public override void BeginPuzzle()
    {
        for (int i = 0; i < scrollbars.Length; i++) {
            scrollbars[i].gameObject.SetActive(true);
        }
        submitButton.gameObject.SetActive(true);
        base.BeginPuzzle();
    }

    public void ChangeInput(Scrollbar scrollbar) {
        int value = (int)(scrollbar.value * 6);

        if (value != lastValueChange) {
            playerInput = "";
            foreach (Scrollbar bar in scrollbars) {
                playerInput += ((int)(bar.value * 6 - .01)).ToString();
            }
            lastValueChange = value;
            puzzleStatus.text = playerInput;
        }
    }

    public void SubmitInput() {
        GetInput(playerInput);
        Debug.Log(playerInput);
        Debug.Log(keyString);
    }

    public void GetInput(string input) {
        if (CheckInput(input) == false) {
        }
    }

    public override void PuzzleCompleted()
    {
        EndPuzzle();
        base.PuzzleCompleted();
    }

    public override void EndPuzzle() {
        for (int i = 0; i < scrollbars.Length; i++) {
            scrollbars[i].value = 0;
            scrollbars[i].gameObject.SetActive(false);
        }
        submitButton.gameObject.SetActive(false);
        base.EndPuzzle();
    }
}