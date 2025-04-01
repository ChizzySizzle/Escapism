
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Button_Controller : MonoBehaviour
{
    public Button puzzleButton;
    public PuzzleRoom currentRoom;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
        
        puzzleButton = GetComponent<Button>();
        
        puzzleButton.onClick.AddListener(OnButtonClick);
    }

    public void SetRoom(PuzzleRoom puzzleRoom) {
        currentRoom = puzzleRoom;
    }

    void OnButtonClick() {
        puzzleButton.interactable = false;
        puzzleButton.interactable = true;

        Puzzle_Manager.instance.GetRoomPuzzle(currentRoom);
    }
}
