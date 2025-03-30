using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Puzzle_One_Manager : Puzzle_Manager
{
    public GameObject randomNumberText;
    public TMP_InputField randomNumberInput;
    private int randomNum;
    private string keyString;

    void Start()
    {
        randomNum = Random.Range(1000, 9999);
        randomNumberText.GetComponent<TMP_Text>().text = randomNum.ToString();

        keyString = randomNum.ToString();

        SetKey(keyString);
    }

    public override void BeginPuzzle(PuzzleRoom puzzleRoom) {
        currentRoom = puzzleRoom;
        
    
        puzzleBackground.SetActive(true);
        
        if (currentRoom.puzzleNum == Puzzle.one) {
            randomNumberInput.gameObject.SetActive(true);

            randomNumberInput.onEndEdit.AddListener(CheckInput); 
        }
    }

    public override void EndPuzzle() {
        base.EndPuzzle();
        randomNumberInput.gameObject.SetActive(false);
    }
}
