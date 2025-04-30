///
/// Gabriel Heiser
/// 4/29/25
/// This provides a scriptable object to store room data for all non-puzzle rooms
/// 

using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Rooms/Room")]
public class Room : ScriptableObject
{
    // No longer being used
    // public enum Facing { North, East, South, West, None}
    // public Facing facing;

    // Image of the room
    public Sprite backgroundImage;
    // Does the room have chizzy in it?
    public bool hasChizzy;
    // Does the room have Box in it?
    public bool hasBox;
    // Does the room have tharacia in it?
    public bool hasTharacia;
    // Does the room contain a cabinet on the left?
    public bool hasLeftCabinet;
    // Does the room contain a cabinet on the right?
    public bool hasRightCabinet;
    // References to the surrounding rooms
    public Room forwardRoom;
    public Room rightRoom;
    public Room backRoom;
    public Room leftRoom;
}
