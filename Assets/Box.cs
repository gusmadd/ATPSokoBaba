using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public string boxColor; // "Red" atau "Blue"
    private bool onGoal = false;

    public bool IsOnGoal()
    {
        return onGoal;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (boxColor == "Red" && collision.CompareTag("Goals Red"))
        {
            onGoal = true;
        }
        else if (boxColor == "Blue" && collision.CompareTag("Goals AFBlue"))
        {
            onGoal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (boxColor == "Red" && collision.CompareTag("Goals Red"))
        {
            onGoal = false;
        }
        else if (boxColor == "Blue" && collision.CompareTag("Goals Blue"))
        {
            onGoal = false;
        }
    }
}
