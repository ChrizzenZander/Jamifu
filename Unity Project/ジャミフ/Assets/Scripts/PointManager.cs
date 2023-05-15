using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PointManager : MonoBehaviour
{
    public int points = 0;
    public int[] pointList = new int[0];
    public TMP_Text displayText;

    public void Update() {
        //AddPoint(1);
        UpdateText();
    }

    public void UpdateText() {
        displayText.text = points.ToString();
    }

    public void AddPoint(int point) {
        points += point;
        UpdateText();
    }

    public void SubPoint(int point) {
        points -= point;
        UpdateText();
    }

    public void StorePoints() {
        int gameID = SceneManager.GetActiveScene().buildIndex;
        pointList[gameID] = points;
    }

    public void SetPoints(int point)
    {
        points = point;
        UpdateText();
    }
    public void ResetPoints() {
        points = 0;
        pointList = new int[0];
        UpdateText();
    }
};
