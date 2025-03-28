using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Room")]
public class Room : ScriptableObject
{
    public Sprite backgroundImage;
    public bool hasChizzy;
    public Room forwardRoom;
    public Room rightRoom;
    public Room backRoom;
    public Room leftRoom;

}
