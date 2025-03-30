
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Puzzle_Manager : MonoBehaviour
{
    public static Puzzle_Manager instance;
    public enum Puzzle { one, two, three, four, five, six, seven, eight };
    public Puzzle puzzleNum;
    public GameObject puzzleBackground;
    private List<PuzzleRoom> puzzleRooms= new List<PuzzleRoom>();
    private List<Puzzle_Manager> puzzleManagers = new List<Puzzle_Manager>();

    protected PuzzleRoom currentRoom;
    private int completedPuzzles;
    private string puzzleKey;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        foreach (PuzzleRoom room in puzzleRooms) {
            room.hasPuzzle = true;
        }

        completedPuzzles = 0;
    }

    public virtual void BeginPuzzle(PuzzleRoom puzzleRoom) {
        currentRoom = puzzleRoom;

        foreach (Puzzle_Manager manager in puzzleManagers) {
            
        }


    
        puzzleBackground.SetActive(true);
    }

    protected void SetKey(string key) {
        puzzleKey = key;
    }

    protected void CheckInput(string inputText) {
        if (currentRoom.puzzleNum == Puzzle.one)
            if (inputText == puzzleKey) {
                puzzleBackground.SetActive(false);
                currentRoom.hasPuzzle = false;
                Navigation_Manager.instance.puzzleButton.gameObject.SetActive(false);
                completedPuzzles += 1;
                Debug.Log(completedPuzzles);
            }
    }

    public virtual void EndPuzzle() {
        puzzleBackground.SetActive(false);
    }

    public void OnEscape() {
        EndPuzzle();
    }
}
