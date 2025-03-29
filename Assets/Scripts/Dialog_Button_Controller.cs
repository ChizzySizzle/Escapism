using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_Button_Controller : MonoBehaviour
{
    private Button dialogButton;
    public TMP_Text buttonText;
    public enum ButtonPosition { top, bottom };
    public ButtonPosition buttonPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;

        dialogButton = GetComponent<Button>();

        dialogButton.onClick.AddListener(OnDialogButtonClicked);
    }

    void OnDialogButtonClicked() {
        if (buttonPosition == ButtonPosition.top) {
            buttonText.text = "top, Pressed";
            StartCoroutine(Dialog_Manager.instance.DictDialogLoader("Message3"));
            dialogButton.gameObject.SetActive(false);
        }
        if (buttonPosition == ButtonPosition.bottom) {
            buttonText.text = "bot, Pressed";
        }
    }

}
