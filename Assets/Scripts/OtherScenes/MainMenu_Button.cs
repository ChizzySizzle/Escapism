using UnityEngine.SceneManagement;

public class MainMenu_Button : ButtonClass
{
    public override void Start()
    {
        base.Start();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        // Load the menu screen
        SceneManager.LoadScene("MenuScreen");
    }
}
