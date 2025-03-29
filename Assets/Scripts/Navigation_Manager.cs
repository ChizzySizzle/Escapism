using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    public GameObject forwardButton;
    public GameObject rightButton;
    public GameObject backButton;
    public GameObject leftButton;
    private Compass_Controller compassController;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        compassController = compass.GetComponent<Compass_Controller>();

        currentRoom = startRoom;
        Unpack();
    }

    void Unpack() {
        forwardButton.SetActive(currentRoom.forwardRoom != null);
        rightButton.SetActive(currentRoom.rightRoom != null);
        backButton.SetActive(currentRoom.backRoom != null && currentRoom != startRoom);
        leftButton.SetActive(currentRoom.leftRoom != null);

        chizzyButton.SetActive(currentRoom.hasChizzy);

        backgroundImage.sprite = currentRoom.backgroundImage;

        if (currentRoom == dialogRoom) {
            Dialog_Manager.instance.BeginDialog();
            compass.SetActive(false);
        }

        compassController.UpdateCompass();
    }

    public void SwitchRooms(Direction_Button_Controller.Direction dir) {
        if (currentRoom == dialogRoom) {
            Dialog_Manager.instance.EndDialog();
            compass.SetActive(true);
        }
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
}
