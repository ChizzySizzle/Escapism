
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialog/Dialog_Requirement")]
public class Dialog_Requirement : ScriptableObject
{
    public string Condition;
    public bool isSatisfied;
}
