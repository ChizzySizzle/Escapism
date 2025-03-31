using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_Button_Controller : MonoBehaviour
{
    private Button dialogButton;
    public Dialog_Choice choice;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;

        dialogButton = GetComponent<Button>();

        dialogButton.onClick.AddListener(OnDialogButtonClicked);
    }

    void OnDialogButtonClicked() {
        dialogButton.interactable = false;
        dialogButton.interactable = true;

        Dialog_Manager.instance.NewDialog(choice.nextMessage);
        choice.beenUsed = true;
    }
}
