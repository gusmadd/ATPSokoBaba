using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    private GameObject[] obstacles;
    private GameObject[] objects;

    void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
        objects = GameObject.FindGameObjectsWithTag("Objects");
    }

    public bool Move(Vector2 direction)
    {
        // Kalau posisi target terhalang → tidak bisa geser
        if (ObjToBlocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            // Geser kotak ke arah tujuan
            transform.position += (Vector3)direction;
            return true;
        }
    }

    public bool ObjToBlocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;

        // Cek obstacle (tembok)
        foreach (var obj in obstacles)
        {
            if (obj.transform.position.x == newPos.x && obj.transform.position.y == newPos.y)
            {
                return true;
            }
        }

        // Cek object lain (box lain)
        foreach (var objToPush in objects)
        {
            if (objToPush != gameObject && 
                objToPush.transform.position.x == newPos.x && 
                objToPush.transform.position.y == newPos.y)
            {
                return true;
            }
        }

        return false;
    }
}
