
using UnityEngine;

public class Map_Button_Controller : ButtonClass
{
    public bool affectsAnswer;
    public bool isInPlace;
    public bool isTwoWay;
    public Vector3 correctRotation;

    private Puzzle_Six_Controller puzzleSix;
    
    public override void Start()
    {
        base.Start();
        puzzleSix = FindObjectOfType<Puzzle_Six_Controller>();
    }

    public void SetCorrectRotation() {
        correctRotation = transform.rotation.eulerAngles;
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        gameObject.transform.Rotate(new Vector3(0, 0, -90));

        if (isTwoWay) {
            if (transform.rotation.eulerAngles == correctRotation 
            || new Vector3(0, 0, transform.rotation.eulerAngles.z - 180) == correctRotation) {
                isInPlace = true;
                puzzleSix.CheckRotations();
            }
        }
        else if (gameObject.transform.rotation.eulerAngles == correctRotation) {
            isInPlace = true;
            puzzleSix.CheckRotations();
        }
        else {
            isInPlace = false;
        }
    }
}
