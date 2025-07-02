
using UnityEngine;

public class Game_UI_Objects : MonoBehaviour
{
    public static Game_UI_Objects instance;
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
}
