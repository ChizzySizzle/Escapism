using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Chizzy")]
public class Chizzy : ScriptableObject
{
    public Sprite currentEmotion;
    public string currentDialog;
    public List<Sprite> Emotions = new List<Sprite>();
    public List<string> DialogOptions =  new List<string>();
}
