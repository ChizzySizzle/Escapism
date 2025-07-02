///
/// Gabriel Heiser
/// 4/29/25
/// Button to take the player to the settings page
/// 

public class Settings_Button : ButtonClass
{
    public override void Start()
    {
        base.Start();
    }

    public override void OnButtonClick()
    {
        base.OnButtonClick();
        if (Settings_UI.instance != null)
        {
            Settings_UI.instance.DisplaySettings();
        }
    }
}
