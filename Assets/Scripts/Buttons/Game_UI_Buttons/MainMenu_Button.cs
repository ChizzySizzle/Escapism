///
/// Gabriel Heiser
/// 4/29/25
/// Button to take the player back to the title screen
/// 

public class MainMenu_Button : ButtonClass
{
    public override void Start()
    {
        base.Start();
    }

    public override void OnButtonClick()
    {
        base.OnButtonClick();
        // Load the menu screen
        Scene_Manager.instance.GoToTitleScreen();
    }
}
