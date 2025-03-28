using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog_Manager : MonoBehaviour
{
    public Chizzy chizzy;
    public Image chizzyImage;
    public Image dialogBox;
    
    void Start()
    {
        
    }

    public void BeginDialog() {
        chizzyImage.sprite = chizzy.currentEmotion;
    }
}
