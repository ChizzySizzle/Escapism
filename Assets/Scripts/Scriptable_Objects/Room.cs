
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Rooms/Room")]
public class Room : ScriptableObject
{
    public enum Facing { North, East, South, West, None}
    public Facing facing;
    public Sprite backgroundImage;
    public bool hasChizzy;
    public bool hasBox;
    public bool hasTharacia;
    public bool hasLeftCabinet;
    public bool hasRightCabinet;
    public Room forwardRoom;
    public Room rightRoom;
    public Room backRoom;
    public Room leftRoom;

}
