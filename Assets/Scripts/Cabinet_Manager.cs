
using UnityEngine;

public class Cabinet_Manager : MonoBehaviour
{
    // Public references
    public Room[] cabinetRooms;
    public static Cabinet_Manager instance;
    public bool playerHasCode;
    public AudioClip cabinetEmpty;
    public AudioClip cabinetLocked;

    // Private references
    private bool playerHasKey;
    private Room randomKeyRoom;
    private Room randomCodeRoom;
    private Puzzle_Four_Controller puzzleFour;

    void Awake()
    {
        // Set accessible instance
        instance = this;
    }

    // Get the fourth puzzle controller
    void Start() {
        puzzleFour = FindObjectOfType<Puzzle_Four_Controller>();

        // Connect restart function to restart event
        Game_Manager.instance.onRestart += OnRestart;
        OnRestart();
    }

    // Run restart code on start/restart to set up puzzle
    void OnRestart() {
        // Player should not have key
        playerHasKey = false;
        // Player should not have code
        playerHasCode = false;

        // Find random cabinet number to hide key
        int randomKeyNum = Random.Range(0, cabinetRooms.Length);
        // Find random cabinet number to hide the code
        int randomCodeNum = Random.Range(0, cabinetRooms.Length);

        // If the cabinet key number is the same as the code number, choose a new code number until they are different
        while (randomKeyNum == randomCodeNum) {
            randomCodeNum = Random.Range(0, cabinetRooms.Length);
        }

        // Set the random key room based on the random key cabinet number
        randomKeyRoom = cabinetRooms[randomKeyNum];
        // Set the random code room based on the random code cabinet number
        randomCodeRoom = cabinetRooms[randomCodeNum];
    }

    // Call to check if the current room cabiniet contains the key
    public void CheckCabinet() {
        // If the current room is the key room, the player will pick up the key
        if (Navigation_Manager.instance.currentRoom == randomKeyRoom){
            playerHasKey = true;
            // Display pickup text
            puzzleFour.DisplayText("You have picked up a KEY!");
            // Set the key room empty so the player cannot pick up the key again
            randomKeyRoom = null;
            // Play reinforcing audioclip
            Game_Manager.instance.audioSource.PlayOneShot(Puzzle_Manager.instance.puzzleComplete);
        }
        // If the current room is not the key room, display empty text
        else {
            puzzleFour.DisplayText("Nothing here.");
            // Play neutral audio clip
            Game_Manager.instance.audioSource.PlayOneShot(cabinetEmpty);
        }
    }

    // Call to check if current room cabinet contains the code
    public void CheckLockBox() {
        // Check if the current room is the room with the code and the player has the key
        if (Navigation_Manager.instance.currentRoom == randomCodeRoom && playerHasKey) {
            playerHasCode = true;
            // Display pickup text
            puzzleFour.DisplayText("You have picked up a CODE!");
            // Set the code room empty so the player cannot pick up another code
            randomCodeRoom = null;
            // Play reinforcing audioclip
            Game_Manager.instance.audioSource.PlayOneShot(Puzzle_Manager.instance.puzzleComplete);
        }
        // If the player has the key but the lock box is empty
        else if (playerHasKey) {
            puzzleFour.DisplayText("Nothing here.");
            // Play neutral audio clip
            Game_Manager.instance.audioSource.PlayOneShot(cabinetEmpty);
        }
        //  If the player does not yet have the key
        else {
            puzzleFour.DisplayText("This drawer is locked.");
            // Play discouraging audio clip
            Game_Manager.instance.audioSource.PlayOneShot(cabinetLocked);
        }
    }
}
