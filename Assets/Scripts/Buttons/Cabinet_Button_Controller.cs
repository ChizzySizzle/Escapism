
public class Cabinet_Button_Controller : ButtonClass
{  
    public bool isCabinet;
    
    public override void Start() {
        base.Start();
        CutAlpha();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        if (isCabinet)
            Cabinet_Manager.instance.CheckCabinet();
        else
            Cabinet_Manager.instance.CheckLockBox();
    }
}
