using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Direction_Button_Controller : MonoBehaviour
{
    public Button button;
    public enum Direction { Forward, Right, Back, Left};
    public Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(OnButtonClicked);
    }

    void OnButtonClicked() {
        Navigation_Manager.instance.SwitchRooms(direction);
        button.interactable = false;
        button.interactable = true;

        Debug.Log("Going" + direction);
    }
}
