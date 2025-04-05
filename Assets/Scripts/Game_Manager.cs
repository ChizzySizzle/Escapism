
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    private Dialog_Manager dialogManager;
    private Navigation_Manager navManager;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }
    
    void Start()
    {
        dialogManager = GetComponent<Dialog_Manager>();
        navManager = GetComponent<Navigation_Manager>();
    }

    void Update()
    {
        
    }
}
