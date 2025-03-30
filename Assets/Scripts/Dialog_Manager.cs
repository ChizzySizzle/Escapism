using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.IO;
using Unity.VisualScripting;

public class Dialog_Manager : MonoBehaviour
{
    public static Dialog_Manager instance;
    public Chizzy chizzy;
    public Image chizzyImage;
    public Image dialogBox;
    public TMP_Text dialogText;
    public GameObject userInput;
    public TextAsset dialogTextFile;

    private string playerName = "";
    private string currentDialog;
    private bool cont;
    private bool hasName;
    private bool messageFull;
    private bool dialogEnded;
    private List<string> dialogList = new List<string>();
    private Dictionary<string, string[]> dialogDictionary = new Dictionary<string,string[]>();
    private Dictionary<string, Sprite> emotionDictionary = new Dictionary<string,Sprite>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    IEnumerator IntroDialog() {
        cont = false;
        chizzy.currentEmotion = emotionDictionary["concerned"];
        UpdateEmotion();

        chizzy.currentDialog = "(Press space for dialog)\n(Press escape to leave)";
        UpdateDialog();

        while (!cont || !messageFull) {
            yield return null;
        }

        cont = false;
        chizzy.currentEmotion = emotionDictionary["happy"];
        UpdateEmotion();

        chizzy.currentDialog = "Hello! Im Chizzy, Who are you?";
        UpdateDialog();

        GetUsername();

        while (!cont || !hasName || !messageFull) {
            yield return null;
        }
        cont = false;

        chizzy.currentEmotion = emotionDictionary["peace"];
        UpdateEmotion();

        chizzy.currentDialog = $"Nice to meet you, {playerName}!!";
        UpdateDialog();

        LoadDialog();

        currentDialog = "Message2";
    }

    public IEnumerator DictDialogLoader(string key) {
        string[] valueParts = dialogDictionary[key];
        int partsLength = valueParts.Length;

        for (int lineCount = 0; lineCount < partsLength; lineCount += 1) {

            cont = false;

            if (lineCount % 2 == 0) {
                chizzy.currentEmotion = emotionDictionary[valueParts[lineCount]];
                UpdateEmotion();
            }
            else {
                chizzy.currentDialog = valueParts[lineCount];
                UpdateDialog();

                while (!cont || !messageFull) {
                    yield return null;
                }
            }
        }
    }

    void Start()
    {        
        GameStart();
    }

    void GameStart() {
        dialogList.Clear();
        if (dialogList.Count == 0) {
            LoadEmotions();
        }
    }

    public void BeginDialog() {
        dialogEnded = false;

        chizzy.currentDialog = "";
        dialogText.text = "";

        chizzyImage.gameObject.SetActive(true);
        dialogBox.gameObject.SetActive(true);

        if (playerName == "") {
            StartCoroutine("IntroDialog");
        }
        else
            StartCoroutine(DictDialogLoader(currentDialog));
    }

    void UpdateDialog() {
        
        StartCoroutine(DialogTyper(chizzy.currentDialog));
    }

    IEnumerator DialogTyper(string message) {
        dialogText.text = "";
        messageFull = false;
        foreach (char c in message) {
            if (cont || dialogEnded) {
                dialogText.text = message;
                messageFull = true;
                cont = false;
                break;
            }
            dialogText.text += c;
            yield return new WaitForSeconds(.05f);
        }
        messageFull = true;

    }

    public void EndDialog() {
        chizzyImage.gameObject.SetActive(false);
        dialogBox.gameObject.SetActive(false);
        userInput.gameObject.SetActive(false);
        dialogEnded = true;
    }

    void UpdateEmotion() {
        chizzyImage.sprite = chizzy.currentEmotion;
    }

    void LoadEmotions() {
        foreach (Sprite emotion in chizzy.Emotions) {
            string[] emotionName = emotion.ToString().Split('_');
            emotionDictionary.Add(emotionName[1], emotion);
        }
    }

    void GetUsername() {
        userInput.SetActive(true);

        TMP_InputField inputField = userInput.GetComponent<TMP_InputField>();
        inputField.onSubmit.AddListener(SetUsername);
    }

    void SetUsername(string username) {
        playerName = username;
        userInput.SetActive(false);
        cont = true;
        hasName = true;
    }

    void LoadDialog() {
        string[] dialogLines = dialogTextFile.text.Split("\n");
        foreach (string dialogLine in dialogLines) {
            string[] dialogParts = dialogLine.Split(':');
            string[] valueParts = dialogParts[1].Split(';');

            int partsNum = valueParts.Length;

            for (int i = 0; i < partsNum; i++) {
                if (valueParts[i][0] == '$')
                    valueParts[i] = valueParts[i].Replace("playerName", playerName).TrimStart('$');
            }

            string dictKey = dialogParts[0];
            string[] dictValue = valueParts;

            dialogDictionary.Add(dictKey, dictValue);
        }
    }

    public void OnSpacebar() {
        if (Navigation_Manager.instance.currentRoom == Navigation_Manager.instance.dialogRoom) {
            cont = true;
        }
    }
}
