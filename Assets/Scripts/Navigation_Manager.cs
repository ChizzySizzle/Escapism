
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Navigation_Manager : MonoBehaviour
{
    // public references
    public static Navigation_Manager instance;
    [Header("Room references")]
    public Room currentRoom;
    public Image backgroundImage;
    public Room startRoom;
    public Room dialogRoom;
    public AudioClip switchRoomsAudio;
    [Header("Room Objects")]
    public GameObject rightCabinet;
    public GameObject leftCabinet;
    public GameObject puzzleOneNumber;
    public GameObject puzzleFiveNumber;
    public GameObject box;
    public GameObject tharacia;
    public GameObject chizzyButton;
    [Header("Puzzle Objects")]
    public GameObject puzzleButton;
    public GameObject puzzleNumberText;
    [Header("Direction Buttons")]
    public GameObject forwardButton;
    public GameObject rightButton;
    public GameObject backButton;
    public GameObject leftButton;
    // No longer being used
    // public GameObject compass;


    // private references
    // private Compass_Controller compassController;
    
    // Set the publically accessible instance
    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // compassController = compass.GetComponent<Compass_Controller>();
        
        // Add the on restart method to the on restart event
        Game_Manager.instance.onRestart += OnRestart;
        OnRestart();
    }

    // Called on game start/restart
    void OnRestart() {
        // End dialog if player is currently in dialog room
        if (currentRoom == dialogRoom) {
            Dialog_Manager.instance.EndDialog();
        }
        // Set the player to the start room
        GoToStart();
    }

    // Unpack the room when the player enters
    public void Unpack() {
        // Set the directions buttons active based on the new rooms exits
        forwardButton.SetActive(currentRoom.forwardRoom != null);
        rightButton.SetActive(currentRoom.rightRoom != null);
        backButton.SetActive(currentRoom.backRoom != null && currentRoom != startRoom);
        leftButton.SetActive(currentRoom.leftRoom != null);

        // Set chizzy active if she is in the new room
        chizzyButton.SetActive(currentRoom.hasChizzy);
        // Set box active if the current room has box
        box.SetActive(currentRoom.hasBox);
        // Set tharacia active if the current room has tharacia
        tharacia.SetActive(currentRoom.hasTharacia);

        // Set cabinets active if there are any in the new room
        rightCabinet.SetActive(currentRoom.hasRightCabinet);
        leftCabinet.SetActive(currentRoom.hasLeftCabinet);

        // Set the room background image to the new rooms
        backgroundImage.sprite = currentRoom.backgroundImage;

        // If the new room is a puzzle room, set puzzle room objects active
        if (currentRoom is PuzzleRoom puzzleRoom) {
            puzzleButton.SetActive(puzzleRoom.hasPuzzle);
            // Set the random hidden number texts active if the room has it
            puzzleOneNumber.SetActive(puzzleRoom.hasPuzzleOneNum);
            puzzleFiveNumber.SetActive(puzzleRoom.hasPuzzleFiveNum);
            // Turn on the puzzle number text and set the text to the 
            puzzleNumberText.SetActive(true);
            puzzleNumberText.GetComponent<TMP_Text>().text = puzzleRoom.roomPuzzleNum.ToString();
            puzzleButton.GetComponent<Puzzle_Button_Controller>().SetRoom(puzzleRoom);
        }
        // If the new room is not a puzzle room, make sure that the puzzle room objects are inactive
        else {
            puzzleButton.SetActive(false);
            puzzleOneNumber.SetActive(false);
            puzzleFiveNumber.SetActive(false);
            puzzleNumberText.SetActive(false);
        }

        // If the new room is the dialog room, start the dialog script
        if (currentRoom == dialogRoom) {
            Dialog_Manager.instance.BeginDialog();
            // compass.SetActive(false);
        }
        // else {
        //     compass.SetActive(true);
        // }

        // compassController.UpdateCompass(currentRoom);
    }

    // Call to switch rooms, pass it the direction to the new room
    public void SwitchRooms(Direction_Button_Controller.Direction dir) {
        // If the direction passed is forward, go to the room that is forward from the current room
        switch (dir) {
            case Direction_Button_Controller.Direction.Forward:
                currentRoom = currentRoom.forwardRoom;
                break;
            case Direction_Button_Controller.Direction.Right:
                currentRoom = currentRoom.rightRoom;
                break;
            case Direction_Button_Controller.Direction.Back:
                currentRoom = currentRoom.backRoom;
                break;
            case Direction_Button_Controller.Direction.Left:
                currentRoom = currentRoom.leftRoom;
                break;
        }

        if (currentRoom != dialogRoom) {
            // Play the switch rooms audio
            Game_Manager.instance.audioSource.PlayOneShot(switchRoomsAudio);
        }

        // Unpack the new room
        Unpack();


        ////////////////////// OLD LOGIC////////////////////////

        // if (dir == Direction_Button_Controller.Direction.Forward) {
        //     currentRoom = currentRoom.forwardRoom;
        // }
        // // Go right
        // else if (dir == Direction_Button_Controller.Direction.Right) {
        //     currentRoom = currentRoom.rightRoom;
        // }
        // // Go Back
        // else if (dir == Direction_Button_Controller.Direction.Back) {
        //     currentRoom = currentRoom.backRoom;
        // }
        // // Go left
        // else if ( dir == Direction_Button_Controller.Direction.Left) {
        //     currentRoom = currentRoom.leftRoom;
        // }
    }

    // Take the player back to the start and unpack it
    public void GoToStart() {
        currentRoom = startRoom;
        Unpack();
    }

    // Called when the player presses escape
    public void OnEscape() {
        // If the player is in the dialog room, go back to the start room and end the dialog
        if (currentRoom == dialogRoom) {
            Dialog_Manager.instance.EndDialog();
            GoToStart();
        }
    }
}
