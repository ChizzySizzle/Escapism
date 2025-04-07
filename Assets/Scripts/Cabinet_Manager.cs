
using UnityEngine;

public class Cabinet_Manager : MonoBehaviour
{
    public Room[] cabinetRooms;
    public static Cabinet_Manager instance;
    public bool playerHasCode;

    private bool playerHasKey;
    private Room randomKeyRoom;
    private Room randomCodeRoom;
    private Puzzle_Four_Controller puzzleFour;

    void Awake()
    {
        instance = this;
    }

    void Start() {
        puzzleFour = FindObjectOfType<Puzzle_Four_Controller>();
    
        Game_Manager.instance.onRestart += OnRestart;
        OnRestart();
    }

    void OnRestart() {
        playerHasKey = false;
        playerHasCode = false;

        int randomKeyNum = Random.Range(0, cabinetRooms.Length);
        int randomCodeNum = Random.Range(0, cabinetRooms.Length);

        while (randomKeyNum == randomCodeNum) {
            randomCodeNum = Random.Range(0, cabinetRooms.Length);
        }

        randomKeyRoom = cabinetRooms[randomKeyNum];
        randomCodeRoom = cabinetRooms[randomCodeNum];
    }

    public void CheckCabinet() {
        if (Navigation_Manager.instance.currentRoom == randomKeyRoom){
            playerHasKey = true;
            puzzleFour.DisplayText("You have picked up a KEY!");
            randomKeyRoom = null;
        }
    }

    public void CheckLockBox() {
        if (Navigation_Manager.instance.currentRoom == randomCodeRoom && playerHasKey) {
            playerHasCode = true;
            puzzleFour.DisplayText("You have picked up a CODE!");
            randomCodeRoom = null;
        }
    }
}
