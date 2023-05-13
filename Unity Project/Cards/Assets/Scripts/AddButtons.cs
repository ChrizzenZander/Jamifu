using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtons : MonoBehaviour
{
    [SerializeField]
    private Transform PuzzleField;

    [SerializeField]
    private GameObject btn;

    void Awake() {
        for (int i = 0; i < 8; i++) {
            GameObject Button = Instantiate(btn);
            Button.name = ""+i.ToString();
            Button.transform.SetParent(PuzzleField, false /* For ikke at få problemer med world position sættes den til false */);
        }
    }
} // Add buttons
