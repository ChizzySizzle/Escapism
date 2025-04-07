using UnityEngine.SceneManagement;

public class BeginButton : ButtonClass
{
    public override void Start()
    {
        base.Start();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        SceneManager.LoadScene("Main");
    }
}
