/// 
/// Gabriel Heiser
/// 4/29/25
/// The dialog manager handles all things dialog, including the UI elements and player input
/// Actual messages, choices, and requirements are handled by their corresponding scriptable objects
/// This script utilizes those dialog objects to construct a branching and fleshed out conversation with chizzy
/// 
/// Full transparency:
/// Originally used dictionary and file loading to accomplish dialog, did not allow me player options
/// Conversated with AI to imagine what a system using scriptable objects for dialog might look like
/// Never asked for direct code or copy/pasted from AI, all code is entirely my own
/// 

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Dialog_Manager : MonoBehaviour
{
    // public references
    public static Dialog_Manager instance;
    [Header("Scene Objects")]
    public Image chizzyImage;
    public Image dialogBox;
    public TMP_Text dialogText;
    public GameObject userInput;
    public Button[] dialogButtons;
    [Header("Dialog Objects")]
    public Dialog_Message startDialog;
    public List<Dialog_Choice> dialogChoices;
    public string lastMessage;
    [Header("Audio")]
    public AudioClip chizzyVoice;

    // Private variables
    private string playerName = "player";
    private Dialog_Message currentDialog;
    private bool cont = false;
    private bool gettingName = false;
    private bool messageFull;

    // Set public instance
    void Awake()
    {
        instance = this;
    }

    // Run once on level start
    void Start()
    {
        // Get the input field and add a listener method
        TMP_InputField inputField = userInput.GetComponent<TMP_InputField>();
        inputField.onSubmit.AddListener(SetUsername);

        // Add the on restart method to the on restart event
        Game_Manager.instance.onRestart += OnRestart;
        // Run the restart method to initialize the dialog manager
        OnRestart();
    }

    void OnRestart() {
        // Set a default player name
        playerName = "player";
        // Reset runtime variables to avoid dialog softlock
        gettingName = false;
        messageFull = true;
        // Set the current dialog to the beginning dialog
        currentDialog = startDialog;

        // Set all dialog choices back to unused
        foreach (var dialog in dialogChoices) {
            dialog.beenUsed = false;
        }

        // Reset the input field text
        userInput.GetComponent<TMP_InputField>().text = "";
    }

    // Function for displaying current dialog and potentially dialog options to the player based on dialog passed in
    public void DisplayDialog(Dialog_Message newDialog) {
        // Set the current dialog to the new dialog being passed in
        currentDialog = newDialog;

        // If the current dialog is marked as an end, move the dialog forward and clsoe the dialog session
        if (currentDialog.isEnd) {
            currentDialog = currentDialog.nextDialog;
            EndDialog();
        }
        else {
            // If the current message is prompting the user for their name, run the get username function
            if (currentDialog.dialogMessage.EndsWith("name?")) {
                GetUsername();
            }

            // Create a formatted string that replaces payer or last message placeholders
            string DialogFormatted = currentDialog.dialogMessage.Replace("{player}", playerName).Replace("{message}", lastMessage);

            // Set any active dialog buttons inactive
            foreach (Button button in dialogButtons) {
                button.gameObject.SetActive(false);
            }

            // If the current dialog has choices
            if (currentDialog.dialogChoices.Length > 0) {
                bool anyLeft = false;
                // For every dialog choice, if any have been used, set any left variable to true
                foreach (var choice in currentDialog.dialogChoices) {
                    if (!choice.beenUsed)
                        anyLeft = true;
                }
                // If there are none left, progress the dialog
                if (!anyLeft) {
                    DisplayDialog(currentDialog.nextDialog);
                    return;
                }
            }
            // Start the coroutine that types the dialog out, using the formatted dialog
            StartCoroutine(TypeDialog(DialogFormatted));
            // Update chizzy's emotion
            chizzyImage.sprite = currentDialog.emotion;

            // Start the coroutine to wait for the players input before progressing dialog
            StartCoroutine(WaitForInput(currentDialog.nextDialog));
        }
    }

    // Coroutine to type out the given dialog
    IEnumerator TypeDialog(string message) {
        // Reset the dialog textbox
        dialogText.text = "";
        // The message is no longer full
        messageFull = false;
        // Do not continue yet
        cont = false;
        // For every character in the message, append it to the end of the dialog textbox
        foreach(char c in message) {
            // If the player hits space to continue, reset continue and fill the dialog text with the full message
            if (cont) {
                cont = false;
                dialogText.text = message;
                break;
            }
            // Append the character to the message
            dialogText.text += c;
            // Play chizzy's voice audio for each character appended at a randomized volume
            Game_Manager.instance.audioSource.PlayOneShot(chizzyVoice, Random.Range(0.7f, 1f));
            // Wait for a randomized short moment before appending the next cahracter
            yield return new WaitForSeconds(Random.Range(0.03f, 0.08f));
        }
        // If the dialog has any options, run the show dialog options function
        if (currentDialog.dialogChoices.Length > 0) {
            ShowDiaologOptions(currentDialog);
        }
        // Otherwise, the message is full
        else {
            messageFull = true;
        }
    }

    // Function to display dialog options for the current dialog
    void ShowDiaologOptions(Dialog_Message optionDialog) {

        // Iterate through all of the options in the dialog options array
        for (int i = 0; i < optionDialog.dialogChoices.Length; i++) {

            // If the dialog option has not already been used
            if (optionDialog.dialogChoices[i].beenUsed == false) {
                
                // Set the show dialog variable to true
                bool showOption = true;

                // If the dialog option has requirements that need to be satisfied
                if (optionDialog.dialogChoices[i].choiceRequirements != null) {
                    // For each requirement in the dialog option's requirements array
                    foreach (Dialog_Requirement requirement in optionDialog.dialogChoices[i].choiceRequirements) {
                        // If the requirement is not satisfied, set the show option variable to false
                        if (!requirement.isSatisfied) {
                            showOption = false;
                            break;
                        }
                    }
                }

                // If the show option variable is still true, Set the button active, update the buttons text
                // Then set the buttons choice to the current one
                if (showOption) {
                    dialogButtons[i].gameObject.SetActive(true);
                    dialogButtons[i].GetComponentInChildren<TMP_Text>().text = optionDialog.dialogChoices[i].choiceText;
                    dialogButtons[i].GetComponent<Dialog_Button_Controller>().choice = optionDialog.dialogChoices[i];
                }
            }
        }
    }

    // Called to begin the dialog interaction
    public void BeginDialog() {
        // Set the chizzy image active
        chizzyImage.enabled = true;
        // Set the dialog box active
        dialogBox.enabled = true;
        dialogText.enabled = true;

        // Display the current dialog
        DisplayDialog(currentDialog);
    }

    // Call to end the dialog interaction
    public void EndDialog() {
        // Stop any running dialog coroutines
        StopAllCoroutines();
        // Set all of the dialog UI elements inactive
        chizzyImage.enabled = false;
        dialogBox.enabled = false;
        dialogText.enabled = false;
        userInput.gameObject.SetActive(false);
        // If the current dialog is displaying options, turn them off as well
        if (currentDialog.dialogChoices.Length > 0) {
            foreach (Button button in dialogButtons) {
                button.gameObject.SetActive(false);
            }
        }
        // Switch the player back to the start room
        Navigation_Manager.instance.GoToStart();
    }

    // Call to wait for the players input
    private IEnumerator WaitForInput(Dialog_Message nextDialog) {
        // Set the continue variable to false
        cont = false;
        // While continue is false or waiting to get name or the message is not full
        while (!cont || gettingName || !messageFull) {
            // Halt dialog progression
            yield return null;
        }

        // If there is no following dialog, close out the dialog interaction
        if (nextDialog == null) {
            EndDialog();
        }
        // Otherwise, display the next dialog option
        else
            DisplayDialog(nextDialog);
    }

    // Call to get the players username
    void GetUsername() {
        // Set the input field active
        userInput.SetActive(true);
        // Set the getting name variable true
        gettingName = true;
    }

    // Called to set the players username when the input field is submitted
    void SetUsername(string username) {
        // Set the player username to the given input
        playerName = username;
        // Set the input inactive
        userInput.SetActive(false);
        // Continue the dialog
        cont = true;
        gettingName = false;
    }

    // Get the input from the player to continue the dialog
    public void OnSpacebar() {
        if (Navigation_Manager.instance.currentRoom == Navigation_Manager.instance.dialogRoom) {
            cont = true;
        }
    }
}
