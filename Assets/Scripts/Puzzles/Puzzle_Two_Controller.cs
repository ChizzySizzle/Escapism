
using UnityEngine;

public class Puzzle_Two_Controller : Puzzle
{
    public int simonLength = 10;
    string inputString;
    public GameObject[] simonButtons;
    public override void Start()
    {
        base.Start();

        string randomString = "";
        inputString = "";

        for (int i = 0; i < simonLength; i++) {
            randomString += Random.Range(1, 5).ToString();
        }

        keyString = randomString;

        SetKey(keyString);
    }

    public override void BeginPuzzle() {
        foreach (GameObject button in simonButtons) {
            button.SetActive(true);
        }
        base.BeginPuzzle();
    }

    public void AddInput(int input) {
        inputString += input.ToString();
        SetStatusText(inputString);

        if (inputString[inputString.Length - 1] == keyString[inputString.Length - 1]) {
            if (inputString.Length == keyString.Length) {
                GetInput(inputString);
            }
            return;
        }
        else {
            GetInput(inputString);
        }
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
        foreach(GameObject button in simonButtons) {
            button.SetActive(false);
        }
        base.EndPuzzle();
    }
}
