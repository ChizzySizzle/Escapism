
using UnityEngine;
using TMPro;

public class Puzzle_One_Manager : Puzzle
{

    public TMP_InputField randomNumberInput;

    public override void Start()
    {
        base.Start();
        
        int randomNum = Random.Range(1000, 9999);

        Navigation_Manager.instance.randomNumberText.GetComponent<TMP_Text>().text = randomNum.ToString();
        keyString = randomNum.ToString();

        SetKey(keyString);
    }

    public override void BeginPuzzle() {
        base.BeginPuzzle();
        
        randomNumberInput.gameObject.SetActive(true);

        randomNumberInput.onEndEdit.AddListener(GetInput); 
    }

    private void GetInput(string Input) {
        if (CheckInput(Input) == false) {
            randomNumberInput.ActivateInputField();
        }
    }

    public override void PuzzleCompleted() {
        base.PuzzleCompleted();
        randomNumberInput.gameObject.SetActive(false);
    }

    public override void EndPuzzle() {
        base.EndPuzzle();
        randomNumberInput.gameObject.SetActive(false);
    }
}
