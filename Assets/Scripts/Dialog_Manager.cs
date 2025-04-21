
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Dialog_Manager : MonoBehaviour
{
    // public references
    public static Dialog_Manager instance;
    [Header("Scene Objects")]
    public Image chizzyImage;
    public Image dialogBox;
    public TMP_Text dialogText;
    public GameObject userInput;
    public Button[] dialogButtons;
    [Header("Dialog Objects")]
    public Dialog_Message startDialog;
    public List<Dialog_Choice> dialogChoices;
    public string lastMessage;
    [Header("Audio")]
    public AudioClip chizzyVoice;

    // Private variables
    private string playerName = "player";
    private Dialog_Message currentDialog;
    private bool cont = false;
    private bool gettingName = false;
    private bool messageFull;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {        
        TMP_InputField inputField = userInput.GetComponent<TMP_InputField>();
        inputField.onSubmit.AddListener(SetUsername);

        Game_Manager.instance.onRestart += OnRestart;
        OnRestart();
    }

    void OnRestart() {
        playerName = "player";
        currentDialog = startDialog;

        foreach (var dialog in dialogChoices) {
            dialog.beenUsed = false;
        }

        userInput.GetComponent<TMP_InputField>().text = "";
    }

    public void DisplayDialog(Dialog_Message newDialog) {
        currentDialog = newDialog;

        if (newDialog.isEnd) {
            currentDialog = newDialog.nextDialog;
            EndDialog();
        }
        else {
            chizzyImage.gameObject.SetActive(true);
            dialogBox.gameObject.SetActive(true);

            foreach (Button button in dialogButtons) {
                button.gameObject.SetActive(false);
            }

            if (newDialog.dialogMessage.EndsWith("name?")) {
                GetUsername();
            }

            string newDialogFormatted = newDialog.dialogMessage.Replace("{player}", playerName).Replace("{message}", lastMessage);

            if (newDialog.dialogChoices.Length > 0) {
                bool anyLeft = false;
                foreach (var choice in newDialog.dialogChoices) {
                    if (!choice.beenUsed)
                        anyLeft = true;
                }
                if (!anyLeft) {
                    DisplayDialog(newDialog.nextDialog);
                    return;
                }
            }
            StartCoroutine(TypeDialog(newDialogFormatted));
            chizzyImage.sprite = newDialog.emotion;

            StartCoroutine(WaitForInput(newDialog.nextDialog));
        }
    }

    IEnumerator TypeDialog(string message) {
        dialogText.text = "";
        messageFull = false;
        cont = false;
        foreach(char c in message) {
            if (cont) {
                cont = false;
                dialogText.text = message;
                break;
            }
            dialogText.text += c;
            Game_Manager.instance.audioSource.PlayOneShot(chizzyVoice, Random.Range(0.8f, 1f));
            yield return new WaitForSeconds(Random.Range(0.03f, 0.08f));
        }
        if (currentDialog.dialogChoices.Length > 0) {
            ShowDiaologOptions(currentDialog);
        }
        else {
            messageFull = true;
        }
    }

    void ShowDiaologOptions(Dialog_Message newDialog) {
        bool anyShown = false;

        for (int i = 0; i < newDialog.dialogChoices.Length; i++) {

            if (newDialog.dialogChoices[i].beenUsed == false) {

                bool showOption = true;

                if (newDialog.dialogChoices[i].choiceRequirements != null) {
                    foreach (Dialog_Requirement requirement in newDialog.dialogChoices[i].choiceRequirements) {
                        if (!requirement.isSatisfied) {
                            showOption = false;
                            break;
                        }
                    }
                }

                if (showOption) {
                    dialogButtons[i].gameObject.SetActive(true);
                    dialogButtons[i].GetComponentInChildren<TMP_Text>().text = newDialog.dialogChoices[i].choiceText;
                    dialogButtons[i].GetComponent<Dialog_Button_Controller>().choice = newDialog.dialogChoices[i];
                    anyShown = true;
                }
            }
        }
        if (!anyShown && currentDialog.nextDialog == null) {
            messageFull = true;
        }
    }

    public void BeginDialog() {
        chizzyImage.gameObject.SetActive(true);
        dialogBox.gameObject.SetActive(true);

        DisplayDialog(currentDialog);
    }

    public void EndDialog() {
        StopAllCoroutines();
        chizzyImage.gameObject.SetActive(false);
        dialogBox.gameObject.SetActive(false);
        userInput.gameObject.SetActive(false);
        Navigation_Manager.instance.GoToStart();
    }

    private IEnumerator WaitForInput(Dialog_Message newDialog) {
        cont = false;
        while (!cont || gettingName || !messageFull) {
            yield return null;
        }

        if (newDialog == null) {
            EndDialog();
        }
        else if (currentDialog.isEnd) {
            currentDialog = newDialog.nextDialog;
            EndDialog();
        }
        else
            DisplayDialog(newDialog);
    }

    void GetUsername() {
        userInput.SetActive(true);
        gettingName = true;
    }

    void SetUsername(string username) {
        playerName = username;
        userInput.SetActive(false);
        cont = true;
        gettingName = false;
    }

    public void OnSpacebar() {
        if (Navigation_Manager.instance.currentRoom == Navigation_Manager.instance.dialogRoom) {
            cont = true;
        }
    }
}
