///
/// Gabriel Heiser
/// 4/29/25
/// Class that all buttons inherit from
/// Provides helpful methods that the buttons can inherit/utiize
/// 

using UnityEngine;
using UnityEngine.UI;

// Parent class for all buttons
public class ButtonClass : MonoBehaviour
{
    // Variable to contain the the button component
    protected Button button;

    public virtual void Start() {
        // Get the button coponent
        button = GetComponent<Button>();

        // Add a listener event for when the button is pressed
        button.onClick.AddListener(OnButtonClick);
    }

    // Method for children to cut the alpha out of the button if needed
    // Found this technique on a youtibe video
    protected void CutAlpha() {
        // Only let the player click on non transparent parts of the button
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

    // Method called when the button is clicked
    public virtual void OnButtonClick() {
        // Reset button interactivity to stop it from being a toggle
        button.interactable = false;
        button.interactable = true;
    }
}