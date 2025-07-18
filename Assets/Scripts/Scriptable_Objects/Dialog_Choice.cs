///
/// Gabriel Heiser
/// 4/29/25
/// This object represents the choices the player is able to make, and holds possible requirements needed to make the choices
/// Also tracks if a choice has been used yet and if the choice is repeatable
/// 

using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialog/Dialog_Choice")]
public class Dialog_Choice : ScriptableObject {
    // Text that represents the player's choice
    public string choiceText;
    // The message following the players choice
    public Dialog_Message nextMessage;
    // Possible requirements that need to be satisfied for the choice to appear
    public Dialog_Requirement[] choiceRequirements;
    // Keep track if the choice has already been selected
    public bool beenUsed;
    // Allow the choice to be used repeatedly
    public bool isRepeatable;
}
