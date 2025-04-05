
public class Simon_Button_Controller : ButtonClass
{
    public int buttonNum;

    private Puzzle_Two_Controller puzzleTwo;

    public override void Start()
    {
        base.Start();
        puzzleTwo = FindAnyObjectByType<Puzzle_Two_Controller>();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        puzzleTwo.AddInput(buttonNum);
    }
}
