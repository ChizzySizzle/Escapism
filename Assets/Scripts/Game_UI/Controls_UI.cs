
using UnityEngine;

public class Controls_UI : MonoBehaviour
{
    public static Controls_UI instance;
    public GameObject controlsPanel;
    private bool displayingControls;

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

    public void DisplayControls()
    {
        displayingControls = true;
        controlsPanel.SetActive(true);
    }
    public void CloseControls()
    {
        displayingControls = false;
        controlsPanel.SetActive(false);
    }
    public bool ControlsStatus()
    {
        return displayingControls;
    }
}
