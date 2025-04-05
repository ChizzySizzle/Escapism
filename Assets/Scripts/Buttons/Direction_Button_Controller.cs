
public class Direction_Button_Controller : ButtonClass
{
    public enum Direction { Forward, Right, Back, Left};
    public Direction direction;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        CutAlpha();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        Navigation_Manager.instance.SwitchRooms(direction);
    }
}
