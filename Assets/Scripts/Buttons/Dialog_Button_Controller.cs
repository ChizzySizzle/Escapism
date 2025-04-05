
public class Dialog_Button_Controller : ButtonClass
{
    public Dialog_Choice choice;
    
    public override void Start()
    {
        base.Start();
        CutAlpha();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();

        Dialog_Manager.instance.DisplayDialog(choice.nextMessage);
        if (!choice.isRepeatable)
            choice.beenUsed = true;
    }
}
