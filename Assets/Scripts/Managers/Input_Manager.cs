using UnityEngine;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void OnSpacebar()
    {
        if (Dialog_Manager.instance != null)
        {
            Dialog_Manager.instance.ContinueDialog();
        }
    }

    public void OnEscape()
    {
        if (Controls_UI.instance != null && Controls_UI.instance.ControlsStatus())
        {
            Controls_UI.instance.CloseControls();
        }
        else if (Settings_UI.instance != null && Settings_UI.instance.SettingsStatus())
        {
            Settings_UI.instance.CloseSettings();
        }
        else if (Game_Manager.instance != null)
        {
            Game_Manager.instance.EscapePressed();
        }
    }
}
