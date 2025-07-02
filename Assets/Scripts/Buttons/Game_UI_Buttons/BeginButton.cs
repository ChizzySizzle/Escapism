///
/// Gabriel Heiser
/// 4/29/25
/// Button to begin the game from the title screen
/// 

public class BeginButton : ButtonClass
{
    public override void Start()
    {
        base.Start();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        // Load the main scene
        Scene_Manager.instance.LoadCurrentStart();
    }
}
