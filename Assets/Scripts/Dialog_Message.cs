
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialog/Dialog_Message")]
public class Dialog_Message : ScriptableObject {
    public string dialogMessage;
    public Dialog_Choice[] dialogChoices;
    public Dialog_Message nextDialog;
    public Sprite emotion;
    public bool isEnd;

}
