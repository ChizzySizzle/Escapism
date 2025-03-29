using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Button_Controller : MonoBehaviour
{
    public Button puzzleButton;

    // Start is called before the first frame update
    void Start()
    {
        puzzleButton = GetComponent<Button>();
        
        puzzleButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick() {
        puzzleButton.interactable = false;
        puzzleButton.interactable = true;

        if (Navigation_Manager.instance.currentRoom is PuzzleRoom puzzleRoom) {
            Puzzle_Manager.instance.BeginPuzzle(puzzleRoom.puzzleNum);
        }
    }
}
