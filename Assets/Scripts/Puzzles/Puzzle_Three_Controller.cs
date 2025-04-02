
using UnityEngine;
using TMPro;

public class Puzzle_Three_Controller : Puzzle
{
    string inputString;
    string displayText = "";
    public TMP_InputField inputField;
    public TMP_Text puzzleText;

    public override void Start()
    {
        base.Start();

        int randomNumX = Random.Range(2, 10);
        int randomNumY = Random.Range(2, 10);
        int answer = 0;

        for (int i = 1; i < 4; i++) {
            answer = randomNumX * i + randomNumY;
            displayText += i.ToString() + " = " + answer + "\n";
        }
        displayText += "4 = X?";

        answer = randomNumX * 4 + randomNumY;
        keyString = answer.ToString();
        SetKey(keyString);

        inputField.onSubmit.AddListener(OnInputFieldSubmit);
    }
    
    public void OnInputFieldSubmit(string inputText) {
        inputString = inputText;
        GetInput(inputString);
    }
    public override void BeginPuzzle() {
        puzzleText.text = displayText;
        inputString = "";
        inputField.gameObject.SetActive(true);
        puzzleText.gameObject.SetActive(true);
        base.BeginPuzzle();
    }

    private void GetInput(string input) {
        if (CheckInput(input) == false) {
            inputString = "";
            SetStatusText("Incorrect\nTry Again");
        }
    }

    public override void PuzzleCompleted() {
        EndPuzzle();
        base.PuzzleCompleted();
    }

    public override void EndPuzzle() {
        inputField.gameObject.SetActive(false);
        puzzleText.gameObject.SetActive(false);
        base.EndPuzzle();
    }
}
