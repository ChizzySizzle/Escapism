
using UnityEngine;

public class Map_Button_Controller : ButtonClass
{
    public bool affectsAnswer;
    public bool isInPlace;
    public Vector3 correctRotation;

    private Puzzle_Seven_Controller puzzleSeven;

    public override void Start()
    {
        base.Start();
        puzzleSeven = FindObjectOfType<Puzzle_Seven_Controller>();
        correctRotation = transform.rotation.eulerAngles;
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        gameObject.transform.Rotate(new Vector3(0, 0, -90));
        if (gameObject.transform.rotation.eulerAngles == correctRotation) {
            isInPlace = true;
        }
        else {
            isInPlace = false;
        }
    }
}
