
using UnityEngine;

public class Map_Button_Controller : ButtonClass
{
    // public variables
    public bool affectsAnswer;
    public bool isInPlace;
    public bool isTwoWay;
    public Vector3 correctRotation;

    // private reference
    private Puzzle_Six_Controller puzzleSix;
    
    public override void Start()
    {
        base.Start();
        // Get the puzzle six script
        puzzleSix = FindObjectOfType<Puzzle_Six_Controller>();
    }

    public void SetCorrectRotation() {
        // Set the pieces correct rotation based on its rotation at the very start
        correctRotation = transform.rotation.eulerAngles;
    }

    public override void OnButtonClick() {
        base.OnButtonClick();
        // Rotate the piece by 90 degrees
        gameObject.transform.Rotate(new Vector3(0, 0, -90));

        // Play the button pressed audio
        Game_Manager.instance.audioSource.PlayOneShot(Puzzle_Manager.instance.buttonPressed);

        // if the button has two correct positions
        if (isTwoWay) {
            // If the current rotation OR current rotation - 180 degrees is equal to the saved correct rotation
            if (transform.rotation.eulerAngles == correctRotation 
            || new Vector3(0, 0, transform.rotation.eulerAngles.z - 180) == correctRotation) {
                // Puzzle piece is in place
                isInPlace = true;
                // Check to see if all pieces are in place
                puzzleSix.CheckRotations();
            }
        }
        // if the button has one correct position, check if current rotation equals saved correct rotation
        else if (gameObject.transform.rotation.eulerAngles == correctRotation) {
            // Piece is in place
            isInPlace = true;
            // Check all piece placements
            puzzleSix.CheckRotations();
        }
        // Otherwise, the puzzle piece is not in place
        else {
            isInPlace = false;
        }
    }
}
