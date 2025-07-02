
using UnityEngine;

public class Settings_UI : MonoBehaviour
{
    public static Settings_UI instance;
    public GameObject settingsPanel;
    private bool displayingSettings;

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
    }

    public void DisplaySettings()
    {
        displayingSettings = true;
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        displayingSettings = false;
        settingsPanel.SetActive(false);
    }
    public bool SettingsStatus()
    {
        return displayingSettings;
    }
}
