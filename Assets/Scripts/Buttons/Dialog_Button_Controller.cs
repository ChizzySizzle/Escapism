
public class Dialog_Button_Controller : ButtonClass
{
    // Variable to store the choice the button represents
    public Dialog_Choice choice;
    
    public override void Start()
    {
        base.Start();
        // Cut the buttons alpha
        CutAlpha();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();

        // Display the dialog associated with the selected choice
        Dialog_Manager.instance.DisplayDialog(choice.nextMessage);
        // If the choice is not repeatable, mark the choice as used
        if (!choice.isRepeatable)
            choice.beenUsed = true;
    }
}
