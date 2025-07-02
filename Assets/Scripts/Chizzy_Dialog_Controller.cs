
using UnityEngine;

public class Chizzy_Dialog_Controller : MonoBehaviour
{
    public static Chizzy_Dialog_Controller instance;

    [Header("Restart tracker Messages")]
    public Chizzy_Dialog_Message noRestarts;
    public Chizzy_Dialog_Message oneRestart;
    public Chizzy_Dialog_Message twoRestarts;

    [Header("Player Name Handling")]
    public Chizzy_Dialog_Message defaultName;
    public Chizzy_Dialog_Message funnyName;
    public Chizzy_Dialog_Message rudeName;

    private Chizzy_Dialog_Message starterDialog;
    private Chizzy_Dialog_Message currentDialog;
    private Chizzy_Dialog_Message lastMessage;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateStartDialog(int attemptNumber)
    {
        switch (attemptNumber)
        {
            case 1:
                starterDialog = noRestarts;
                break;
            case 2:
                starterDialog = oneRestart;
                break;
            case 3:
                starterDialog = twoRestarts;
                break;
        }
    }

    public void SetCurrentDialog(Chizzy_Dialog_Message newDialog)
    {
        currentDialog = newDialog;
    }

    public void SetStarterDialog()
    {
        starterDialog = currentDialog;
    }

    public void SetLastMessage()
    {
        lastMessage = currentDialog;
    }

    public Chizzy_Dialog_Message GetLastMessage()
    {
        return lastMessage;
    }

    public void BeginChizzyDialog()
    {
        Dialog_Manager.instance.BeginDialog(starterDialog);
    }
}
