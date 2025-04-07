
using UnityEngine;
using TMPro;

public class Puzzle_Three_Controller : Puzzle
{
    string displayText = "";
    public TMP_InputField inputField;
    public TMP_Text puzzleText;

    public override void Start()
    {
        base.Start();
        inputField.onSubmit.AddListener(OnInputFieldSubmit);

        OnRestart();
    }

    public override void OnRestart() {
        displayText = "";
        inputField.text = "";

        int randomConstX = Random.Range(3, 7);
        int randomVarX = Random.Range(3, 10);

        int randomConstY = Random.Range(3, 7);
        int randomVarY = Random.Range(3, 10);

        int randomConstZ = Random.Range(3, 7);
        int randomVarZ = Random.Range(3, 10);


        displayText += randomConstX.ToString() + "X" + " = " + (randomConstX * randomVarX).ToString() + "\n";
        displayText += randomConstY.ToString() + "Y" + " = " + (randomConstY * randomVarY).ToString() + "\n";
        displayText += randomConstZ.ToString() + "Z" + " = " + (randomConstZ * randomVarZ).ToString() + "\n";
        displayText += "XYZ = ?";

        keyString = (randomVarX * randomVarY * randomVarZ).ToString();
    }
    
    public void OnInputFieldSubmit(string inputText) {
        GetInput(inputText);
    }
    public override void BeginPuzzle() {
        puzzleText.text = displayText;
        inputField.gameObject.SetActive(true);
        puzzleText.gameObject.SetActive(true);
        base.BeginPuzzle();
    }

    private void GetInput(string input) {
        if (CheckInput(input) == false) {
            inputField.text = "";
            inputField.ActivateInputField();

            puzzleStatus.text = "Incorrect\nTry Again";
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
