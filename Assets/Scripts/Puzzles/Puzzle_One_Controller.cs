
using UnityEngine;
using TMPro;

public class Puzzle_One_Controller : Puzzle
{

    public TMP_InputField randomNumberInput;

    public override void Start()
    {
        base.Start();
        
        int randomNum = Random.Range(1000, 9999);

        Navigation_Manager.instance.randomNumberText.GetComponent<TMP_Text>().text = randomNum.ToString();
        keyString = randomNum.ToString();

        SetKey(keyString);

        randomNumberInput.onSubmit.AddListener(GetInput); 
    }

    public override void BeginPuzzle() {
        base.BeginPuzzle();
        
        randomNumberInput.gameObject.SetActive(true);
    }

    private void GetInput(string Input) {
        if (CheckInput(Input) == false) {
            randomNumberInput.text = "";
            SetStatusText("Incorrect\nTry Again");
            randomNumberInput.ActivateInputField();
        }
    }

    public override void PuzzleCompleted() {
        EndPuzzle();
        base.PuzzleCompleted();
    }

    public override void EndPuzzle() {
        base.EndPuzzle();
        randomNumberInput.gameObject.SetActive(false);
    }
}
