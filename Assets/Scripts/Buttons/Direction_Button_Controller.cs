///
/// Gabriel Heiser
/// 4/29/25
/// Used to move between rooms, communicates with the navigation manager
/// 

public class Direction_Button_Controller : ButtonClass
{
    // Directions for each button to represent
    public enum Direction { Forward, Right, Back, Left};
    public Direction direction;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        // Cut the buttons alpha
        CutAlpha();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        // Send the buttons direction to the navigation manager
        Navigation_Manager.instance.SwitchRooms(direction);
    }
}
