using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Dialog_Manager : MonoBehaviour
{
    public static Dialog_Manager instance;
    public Chizzy chizzy;
    public Image chizzyImage;
    public Image dialogBox;
    public TMP_Text dialogText;
    public GameObject userInput;

    private string playerName;
    private List<string> dialogList = new List<string>();
    private Dictionary<string, string> dialogDictionary = new Dictionary<string,string>();
    private List<Sprite> emotionList = new List<Sprite>();
    private Dictionary<string, Sprite> emotionDictionary = new Dictionary<string,Sprite>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        dialogList.Clear();
        if (dialogList.Count == 0) {
            LoadEmotions();
        }

        chizzy.currentEmotion = emotionDictionary["happy"];
        chizzy.currentDialog = "Hello! Im Chizzy, Who are you?";
        
        GetUsername();
    } 

    public void BeginDialog() {
        chizzyImage.gameObject.SetActive(true);
        dialogBox.gameObject.SetActive(true);

        UpdateEmotion();
        UpdateDialog(chizzy.currentDialog);
    }

    void UpdateDialog(string message) {
        StartCoroutine(DialogTyper(message));
    }

    IEnumerator DialogTyper(string message) {
        dialogText.text = "";
        foreach (char c in message) {
            dialogText.text += c;
            yield return new WaitForSeconds(.1f);
        }
        chizzy.currentDialog = dialogText.text;
    }

    public void EndDialog() {
        chizzyImage.gameObject.SetActive(false);
        dialogBox.gameObject.SetActive(false);
    }

    void UpdateEmotion() {
        chizzyImage.sprite = chizzy.currentEmotion;
    }

    void LoadEmotions() {
        foreach (Sprite emotion in chizzy.Emotions) {
            emotionList.Add(emotion);
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
        UpdateDialog($"Nice to meet you, {playerName}!!");

        chizzy.currentEmotion = emotionDictionary["peace"];
        UpdateEmotion();
        
        LoadDialog();
    }

    void LoadDialog() {
        Debug.Log("Loading Dialog File");
    }
}
