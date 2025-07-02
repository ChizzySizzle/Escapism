///
/// Gabriel Heiser
/// 4/29/25
/// This is the container for each of chizzy's messages, including the contents, choices, and accompanying emotion
/// 

using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialog/Chizzy_Dialog_Message")]
public class Chizzy_Dialog_Message : Dialog_Message {
    public bool isMainlineDialog;
    public bool pickupLastMessage;
}
