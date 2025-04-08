
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Five_Controller : Puzzle
{
    public Scrollbar[] scrollbars;

    private TMP_Text puzzleFiveNumber;
    private int lastValueChange = 0;

    public override void Start() {
        base.Start();
        puzzleFiveNumber = Navigation_Manager.instance.puzzleFiveNumber.GetComponent<TMP_Text>();

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
        base.BeginPuzzle();
    }

    public void ChangeInput(Scrollbar scrollbar) {
        string playerInput = "";

        int value = (int)(scrollbar.value * 6);

        if (value != lastValueChange) {
            foreach (Scrollbar bar in scrollbars) {
                playerInput += ((int)(bar.value * 6 - .01)).ToString();
            }
            GetInput(playerInput);

            lastValueChange = value;
        }
    }

    public void GetInput(string input) {
        if (CheckInput(input) == false) {
            puzzleStatus.text = input;
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
        base.EndPuzzle();
    }
}