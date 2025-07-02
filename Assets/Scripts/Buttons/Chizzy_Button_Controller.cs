
using UnityEngine.UI;

///
/// Gabriel Heiser
/// 4/29/25
/// 
public class Chizzy_Button_Controller : ButtonClass
{
    private Image chizzyImage;

    public override void Start()
    {
        base.Start();
        // Cut the buttons alpha
        CutAlpha();
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
  
        Chizzy_Dialog_Controller.instance.BeginChizzyDialog();
    }
}
