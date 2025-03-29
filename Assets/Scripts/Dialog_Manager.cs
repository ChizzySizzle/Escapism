using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_Manager : MonoBehaviour
{
    public static Dialog_Manager instance;
    public Chizzy chizzy;
    public Image chizzyImage;
    public Image dialogBox;
    public TMP_Text dialogText;

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
        if (dialogList.Count == 0) {
            LoadDialog();
            LoadEmotions();
        }
    } 

    public void BeginDialog() {
        chizzyImage.gameObject.SetActive(true);
        dialogBox.gameObject.SetActive(true);

        UpdateChizEmotion();
        dialogText.text = chizzy.currentDialog;
    }

    public void EndDialog() {
        chizzyImage.gameObject.SetActive(false);
        dialogBox.gameObject.SetActive(false);

        chizzy.currentDialog = dialogText.text;
    }

    void UpdateChizEmotion() {
        chizzyImage.sprite = chizzy.currentEmotion;
    }

    void LoadEmotions() {
        foreach (Sprite emotion in chizzy.Emotions) {
            emotionList.Add(emotion);
            string[] emotionName = emotion.ToString().Split('_');
            emotionDictionary.Add(emotionName[1], emotion);
        }
    }

    void LoadDialog() {
        string introDialog = "Hello! Im Chizzy, Who are you?";
        dialogList.Add(introDialog);
        dialogDictionary.Add("Introduction", introDialog);
    }
}
