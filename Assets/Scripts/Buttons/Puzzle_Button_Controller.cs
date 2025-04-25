
public class Puzzle_Button_Controller : ButtonClass
{
    // 
    public PuzzleRoom currentRoom;

    public override void Start()
    {
        base.Start();
        // Cut the buttons alpha
        CutAlpha();
    }

    public void SetRoom(PuzzleRoom puzzleRoom) {
        currentRoom = puzzleRoom;
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        Puzzle_Manager.instance.GetRoomPuzzle(currentRoom);
    }
}
