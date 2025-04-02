
using TMPro;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public enum PuzzleNum { one, two, three, four, five, six, seven, eight }
    public PuzzleNum puzzleNum;
    public GameObject puzzleBackground;
    public TMP_Text puzzleStatus;

    protected string keyString;

    public virtual void Start() {

    }

    public virtual void BeginPuzzle() {
        puzzleBackground.SetActive(true);
        puzzleStatus.gameObject.SetActive(true);
        puzzleStatus.text = "";
    }

    protected void SetKey(string key) {
        keyString = key;
    }

    protected bool CheckInput(string input) {
        if (input == keyString) {
            PuzzleCompleted();
            return true;
        }
        else return false;
    }

    protected void SetStatusText(string input) {
        puzzleStatus.text = input;
    }

    public virtual void PuzzleCompleted() {
        Puzzle_Manager.instance.AddCompletedPuzzle();
    }

    public virtual void EndPuzzle() {
        puzzleBackground.SetActive(false);
        puzzleStatus.gameObject.SetActive(false);
    }
}
