///
/// Gabriel Heiser
/// 4/29/25
/// This is the container for each of chizzy's messages, including the contents, choices, and accompanying emotion
/// 

using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialog/Dialog_Message")]
public class Dialog_Message : ScriptableObject {
    // The message that is displayed to the player
    public string dialogMessage;
    // Possible choices the player can make based on the message
    public Dialog_Choice[] dialogChoices;
    // The dialog following the current message
    public Dialog_Message nextDialog;
    // Emotion for this message
    public enum Emotion { idle, concerned, laugh, confused, smug, sad };
    public Emotion emote;
    // Marker to close out dialog if the end of the conversation has been reached
    public bool isStart;
}
