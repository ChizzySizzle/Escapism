///
/// Gabriel Heiser
/// 4/29/25
/// Button tat represents cabinets/lockboxes. Communicates with the cabinet manager.
/// 

public class Cabinet_Button_Controller : ButtonClass
{  
    // Variable to define if the button is a cabinet or a lockbox
    public bool isCabinet;
    
    public override void Start() {
        base.Start();
        // call the cut alpha method
        CutAlpha();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        // If it is marked as a cabinet, call the check cabinet function from the cabinet manager
        if (isCabinet)
            Cabinet_Manager.instance.CheckCabinet();
        // Otherwise, it is a lockbox. call the check lockbox function from the cabinet manager
        else
            Cabinet_Manager.instance.CheckLockBox();
    }
}
