///
/// Gabriel Heiser
/// 4/29/25
/// Button to take the player to the settings page
/// 

public class Controls_Button : ButtonClass
{
    public override void Start()
    {
        base.Start();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        if (Controls_UI.instance != null)
        {
            Controls_UI.instance.DisplayControls();
        }
    }
}
