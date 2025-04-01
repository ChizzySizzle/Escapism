
using UnityEngine;
using UnityEngine.InputSystem;

public class Puzzle : MonoBehaviour
{
    public enum PuzzleNum { one, two, three, four, five, six, seven, eight }
    public PuzzleNum puzzleNum;
    public GameObject puzzleBackground;

    protected string keyString;

    public virtual void Start() {

    }

    public virtual void BeginPuzzle() {
        puzzleBackground.SetActive(true);
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

    public virtual void PuzzleCompleted() {
        Debug.Log("You have completed the puzzle!");
        puzzleBackground.SetActive(false);
        Puzzle_Manager.instance.AddCompletedPuzzle();
    }

    public virtual void EndPuzzle() {
        puzzleBackground.SetActive(false);
    }
}
