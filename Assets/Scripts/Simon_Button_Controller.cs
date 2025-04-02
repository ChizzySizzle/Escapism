
using UnityEngine;
using UnityEngine.UI;

public class Simon_Button_Controller : MonoBehaviour
{
    public int buttonNum;

    private Button button;
    private Puzzle_Two_Controller puzzleTwo;

    
    void Start()
    {
        button = GetComponent<Button>();
        puzzleTwo = FindObjectOfType<Puzzle_Two_Controller>();
        
        button.onClick.AddListener(ButtonClicked);
    }

    void ButtonClicked() {
        button.interactable = false;
        button.interactable = true;

        puzzleTwo.AddInput(buttonNum);
    }
}
