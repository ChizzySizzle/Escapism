
using UnityEngine;

public class Runtime_UI_Objects : MonoBehaviour
{
    public static Runtime_UI_Objects instance;
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
