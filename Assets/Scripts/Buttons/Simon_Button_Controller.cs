
public class Simon_Button_Controller : ButtonClass
{
    // Assign each button a number
    public int buttonNum;
    // Reference to puzzle two
    private Puzzle_Two_Controller puzzleTwo;

    public override void Start()
    {
        base.Start();
        // Find the puzzle two controller script
        puzzleTwo = FindObjectOfType<Puzzle_Two_Controller>();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        // Play the button pressed audio
        Game_Manager.instance.audioSource.PlayOneShot(Puzzle_Manager.instance.buttonPressed);
        // Send the current buttons number to the puzzle two controller
        puzzleTwo.AddInput(buttonNum);
    }
}
