///
/// Gabriel Heiser
/// 4/29/25
/// This creates global requirements that must be satisfied to unlock dialog choices
/// 

using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Dialog/Dialog_Requirement")]
public class Dialog_Requirement : ScriptableObject
{
    public string Condition;
    public bool isSatisfied;
}
