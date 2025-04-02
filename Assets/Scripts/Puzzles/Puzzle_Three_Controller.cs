
using System.Security.Cryptography;
using UnityEngine;

public class Puzzle_Three_Controller : Puzzle
{
    string inputString;
    public override void Start()
    {
        base.Start();
        inputString = "";

        int randomNum = Random.Range(2, 10);
        int answer = 0;

        for (int i = 1; i < 4; i++) {
            Debug.Log("RandomNum: " + randomNum);
            answer = i + randomNum * i + randomNum;
            Debug.Log("Iteration: " + i + "   Answer: " + answer);
        }

        keyString = answer.ToString();

        SetKey(keyString);
    }

    public override void BeginPuzzle() {

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

        base.EndPuzzle();
    }
}
