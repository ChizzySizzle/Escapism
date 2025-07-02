
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager instance;
    public string titleScreen;
    public string winScreen;
    public string loseScreen;
    public string currentBegin;

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

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == currentBegin)
        {
            Game_Manager.instance.GameStart();
        }
        else if (Game_Manager.instance != null)
        {
            Game_Manager.instance.CloseGame();
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void GoToTitleScreen()
    {
        SceneManager.LoadScene(titleScreen);
    }
    public void GoToLoseScreen()
    {
        SceneManager.LoadScene(loseScreen);
    }
    public void GoToWinScreen()
    {
        SceneManager.LoadScene(winScreen);
    }
    public void LoadCurrentStart() 
    {
        SceneManager.LoadScene(currentBegin);
    }
}
