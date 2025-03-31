
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Dialog_Manager : MonoBehaviour
{
    public static Dialog_Manager instance;
    public Image chizzyImage;
    public Image dialogBox;
    public TMP_Text dialogText;
    public GameObject userInput;
    public Dialog_Message startDialog;
    public Button[] dialogButtons;
    public List<Dialog_Choice> dialogChoices;


    private string playerName = "player";
    private Dialog_Message currentDialog;
    private bool cont = false;
    private bool gettingName = false;
    private bool messageFull;
    private bool dialogEnded;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {        
        currentDialog = startDialog;
        playerName = "player";

        foreach (var dialog in dialogChoices) {
            dialog.beenUsed = false;
        }
    }

    private void DisplayDialog() {
        if (currentDialog == null) {
            Navigation_Manager.instance.OnEscape();
        }
        else {
            chizzyImage.gameObject.SetActive(true);
            dialogBox.gameObject.SetActive(true);

            foreach (Button button in dialogButtons) {
                button.gameObject.SetActive(false);
            }

            if (currentDialog.dialogMessage.EndsWith("name?")) {
                GetUsername();
            }

            string currentDialogFormatted = currentDialog.dialogMessage.Replace("{player}", playerName);
            
            dialogText.text = currentDialogFormatted;
            chizzyImage.sprite = currentDialog.emotion;

            if (currentDialog.dialogChoices.Length > 0) {
                bool anyLeft = false;

                for (int i = 0; i < currentDialog.dialogChoices.Length; i++) {
                    if (currentDialog.dialogChoices[i].beenUsed == false) {
                        dialogButtons[i].gameObject.SetActive(true);
                        dialogButtons[i].GetComponentInChildren<TMP_Text>().text = currentDialog.dialogChoices[i].choiceText;
                        dialogButtons[i].GetComponent<Dialog_Button_Controller>().choice = currentDialog.dialogChoices[i];
                        anyLeft = true;
                    }
                }
                if (!anyLeft) {
                    NewDialog(currentDialog.nextDialog);
                }
            }
            else {
                StartCoroutine(WaitForInput(currentDialog.nextDialog));
            }
        }
    }

    public void NewDialog(Dialog_Message newDialog) {
        currentDialog = newDialog;
        DisplayDialog();
    }

    public void BeginDialog() {
        chizzyImage.gameObject.SetActive(true);
        dialogBox.gameObject.SetActive(true);

        DisplayDialog();
    }

    public void EndDialog() {
        chizzyImage.gameObject.SetActive(false);
        dialogBox.gameObject.SetActive(false);
        userInput.gameObject.SetActive(false);
        dialogEnded = true;
    }

    private IEnumerator WaitForInput(Dialog_Message newDialog) {
        cont = false;
        while (!cont || gettingName) {
            yield return null;
        }
        currentDialog = newDialog;
        if (currentDialog.isStart) {
            Navigation_Manager.instance.OnEscape();
        }
        else {
            DisplayDialog();
        }

    }

    // IEnumerator IntroDialog() {
    //     cont = false;
    //     chizzy.currentEmotion = emotionDictionary["concerned"];
    //     UpdateEmotion();
    //     chizzy.currentDialog = "(Press space for dialog)\n(Press escape to leave)";
    //     UpdateDialog();

    //     while (!cont || !messageFull) {
    //         yield return null;
    //     }
    //     cont = false;
    //     chizzy.currentEmotion = emotionDictionary["happy"];
    //     UpdateEmotion();
    //     chizzy.currentDialog = "Hello! Im Chizzy, Who are you?";
    //     UpdateDialog();

    //     GetUsername();

    //     while (!cont || !hasName || !messageFull) {
    //         yield return null;
    //     }
    //     cont = false;
    //     chizzy.currentEmotion = emotionDictionary["peace"];
    //     UpdateEmotion();
    //     chizzy.currentDialog = $"Nice to meet you, {playerName}!!";
    //     UpdateDialog();

    //     LoadDialog();
    //     currentDialog = "Message2";

    //     while (!cont || !messageFull) {
    //         yield return null;
    //     }
    //     cont = false;
    //     Navigation_Manager.instance.OnEscape();
    // }

    // public IEnumerator DictDialogLoader(string key) {
    //     string[] valueParts = dialogDictionary[key];
    //     int partsLength = valueParts.Length;

    //     for (int lineCount = 0; lineCount < partsLength; lineCount += 1) {

    //         cont = false;

    //         if (lineCount % 2 == 0) {
    //             chizzy.currentEmotion = emotionDictionary[valueParts[lineCount]];
    //             UpdateEmotion();
    //         }
    //         else {
    //             chizzy.currentDialog = valueParts[lineCount];
    //             UpdateDialog();

    //             while (!cont || !messageFull) {
    //                 yield return null;
    //             }
    //         }
    //     }
    //     Navigation_Manager.instance.OnEscape();
    // }


    // void UpdateDialog() {
    //     StartCoroutine(DialogTyper(chizzy.currentDialog));
    // }

    // IEnumerator DialogTyper(string message) {
    //     dialogText.text = "";
    //     messageFull = false;
    //     foreach (char c in message) {
    //         if (cont || dialogEnded) {
    //             dialogText.text = message;
    //             messageFull = true;
    //             cont = false;
    //             break;
    //         }
    //         dialogText.text += c;
    //         yield return new WaitForSeconds(.05f);
    //     }
    //     messageFull = true;
    // }

    void GetUsername() {
        userInput.SetActive(true);
        gettingName = true;

        TMP_InputField inputField = userInput.GetComponent<TMP_InputField>();
        inputField.onSubmit.AddListener(SetUsername);
    }

    void SetUsername(string username) {
        playerName = username;
        userInput.SetActive(false);
        cont = true;
        gettingName = false;
    }


    // void LoadDialog() {
    //     string[] dialogLines = dialogTextFile.text.Split("\n");
    //     foreach (string dialogLine in dialogLines) {
    //         string[] dialogParts = dialogLine.Split(':');
    //         string[] valueParts = dialogParts[1].Split(';');

    //         int partsNum = valueParts.Length;

    //         for (int i = 0; i < partsNum; i++) {
    //             if (valueParts[i][0] == '$')
    //                 valueParts[i] = valueParts[i].Replace("playerName", playerName).TrimStart('$');
    //         }

    //         string dictKey = dialogParts[0];
    //         string[] dictValue = valueParts;

    //         dialogDictionary.Add(dictKey, dictValue);
    //     }
    // }

    public void OnSpacebar() {
        if (Navigation_Manager.instance.currentRoom == Navigation_Manager.instance.dialogRoom) {
            cont = true;
        }
    }
}
