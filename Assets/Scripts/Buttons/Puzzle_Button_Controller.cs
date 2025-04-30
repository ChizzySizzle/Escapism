///
/// Gabriel Heiser
/// 4/29/25
/// Button to load the puzzle rooms current puzzle
/// 

public class Puzzle_Button_Controller : ButtonClass
{
    public override void Start()
    {
        base.Start();
        // Cut the buttons alpha
        CutAlpha();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        // Tell the puzzle manager to display the puzzle corresponding to the current room
        Puzzle_Manager.instance.GetRoomPuzzle((PuzzleRoom)Navigation_Manager.instance.currentRoom);
    }
}
