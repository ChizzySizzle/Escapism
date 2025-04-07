
using UnityEngine;
using UnityEngine.UI;


public class Navigation_Manager : MonoBehaviour
{
    public static Navigation_Manager instance;
    public Room currentRoom;
    public Room startRoom;
    public Room dialogRoom;
    public Image backgroundImage;
    public GameObject compass;
    public GameObject chizzyButton;
    public GameObject puzzleButton;
    public GameObject forwardButton;
    public GameObject rightButton;
    public GameObject backButton;
    public GameObject leftButton;
    public GameObject rightCabinet;
    public GameObject leftCabinet;
    public GameObject puzzleOneNumber;
    public GameObject puzzleFiveNumber;

    private Compass_Controller compassController;
    
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        compassController = compass.GetComponent<Compass_Controller>();
        
        Game_Manager.instance.onRestart += OnRestart;
        OnRestart();
    }

    void OnRestart() {
        if (currentRoom == dialogRoom) {
            Dialog_Manager.instance.EndDialog();
        }
        GoToStart();
    }

    public void Unpack() {
        forwardButton.SetActive(currentRoom.forwardRoom != null);
        rightButton.SetActive(currentRoom.rightRoom != null);
        backButton.SetActive(currentRoom.backRoom != null && currentRoom != startRoom);
        leftButton.SetActive(currentRoom.leftRoom != null);

        chizzyButton.SetActive(currentRoom.hasChizzy);

        rightCabinet.SetActive(currentRoom.hasRightCabinet);
        leftCabinet.SetActive(currentRoom.hasLeftCabinet);

        backgroundImage.sprite = currentRoom.backgroundImage;

        if (currentRoom is PuzzleRoom puzzleRoom) {
            puzzleButton.SetActive(puzzleRoom.hasPuzzle);
            puzzleOneNumber.SetActive(puzzleRoom.hasPuzzleOneNum);
            puzzleFiveNumber.SetActive(puzzleRoom.hasPuzzleFiveNum);
            puzzleButton.GetComponent<Puzzle_Button_Controller>().SetRoom(puzzleRoom);
        }
        else {
            puzzleButton.SetActive(false);
            puzzleOneNumber.SetActive(false);
            puzzleFiveNumber.SetActive(false);
        }

        if (currentRoom == dialogRoom) {
            Dialog_Manager.instance.BeginDialog();
            compass.SetActive(false);
        }
        else {
            compass.SetActive(true);
        }

        compassController.UpdateCompass(currentRoom);
    }

    public void SwitchRooms(Direction_Button_Controller.Direction dir) {
        if (dir == Direction_Button_Controller.Direction.Forward) {
            currentRoom = currentRoom.forwardRoom;
        }
        else if (dir == Direction_Button_Controller.Direction.Right) {
            currentRoom = currentRoom.rightRoom;
        }
        else if (dir == Direction_Button_Controller.Direction.Back) {
            currentRoom = currentRoom.backRoom;
        }
        else if ( dir == Direction_Button_Controller.Direction.Left) {
            currentRoom = currentRoom.leftRoom;
        }
        Unpack();
    }

    public void GoToStart() {
        currentRoom = startRoom;
        Unpack();
    }

    public void OnEscape() {
        if (currentRoom == dialogRoom) {
            Dialog_Manager.instance.EndDialog();
            GoToStart();
        }
    }
}
