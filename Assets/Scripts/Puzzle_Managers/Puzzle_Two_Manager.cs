using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Two_Manager : Puzzle_Manager
{
    private string simonString;
    private string simonGuess;

    void Start()
    {
            for (int i = 0; i < 10; i++) {
            simonString += Random.Range(1, 5);
        }
    }

    public void AddNumber(int number) {
        if (currentRoom.puzzleNum == Puzzle.two) {
            simonGuess += number;
        }
    }
    
    void ButtonGlow(int num) {
        
    }
}
