using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Room")]
public class Room : ScriptableObject
{
    public enum Facing { North, East, South, West, None}
    public Facing facing;
    public Sprite backgroundImage;
    public bool hasChizzy;
    public Room forwardRoom;
    public Room rightRoom;
    public Room backRoom;
    public Room leftRoom;

}
