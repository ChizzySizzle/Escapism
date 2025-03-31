
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialog/Dialog_Choice")]
public class Dialog_Choice : ScriptableObject {
    public string choiceText;
    public Dialog_Message nextMessage;
    public bool beenUsed;
}
