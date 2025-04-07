
using UnityEngine;
using UnityEngine.UI;

public class Puz5_Scrollbar_Controller : MonoBehaviour
{
    private Scrollbar scrollbar;
    private Puzzle_Five_Controller puzzleFive;

    void Start() {
        scrollbar = GetComponent<Scrollbar>();
        puzzleFive = FindObjectOfType<Puzzle_Five_Controller>();

        scrollbar.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float value) {
        scrollbar.interactable = false;
        scrollbar.interactable = true;

        puzzleFive.ChangeInput(scrollbar);
    }
}
