
using UnityEngine;
using UnityEngine.UI;

public class Puz5_Scrollbar_Controller : MonoBehaviour
{
    // Runtime variables
    private Scrollbar scrollbar;
    private Puzzle_Five_Controller puzzleFive;

    ////////////////////// TODO change scrollbars to sliders, more efficient code /////////////////////////

    void Start() {
        // Attach the scrollbar component and the puzzle five controller to their respective variables
        scrollbar = GetComponent<Scrollbar>();
        puzzleFive = FindObjectOfType<Puzzle_Five_Controller>();

        // Add a listener event for when the scrollbar changes values
        scrollbar.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float value) {
        // Reset interactability
        scrollbar.interactable = false;
        scrollbar.interactable = true;

        // Send the scrollbar to the puzzle five controller
        puzzleFive.ChangeInput(scrollbar);
    }
}
