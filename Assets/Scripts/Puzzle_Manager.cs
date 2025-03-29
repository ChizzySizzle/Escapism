using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle_Manager : MonoBehaviour
{
    public static Puzzle_Manager instance;
    public enum Puzzle { one, two, three, four, five, six, seven, eight };
    public List<GameObject> puzzleObjects = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void BeginPuzzle(Puzzle puzzleNum) {
        Debug.Log($"Puzzle {puzzleNum} Beginning!");
    }
}
