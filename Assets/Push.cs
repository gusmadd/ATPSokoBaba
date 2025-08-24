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
        Vector2 targetPos = new Vector2(
            Mathf.Round(transform.position.x + direction.x),
            Mathf.Round(transform.position.y + direction.y)
        );

        if (ObjToBlocked(transform.position, direction))
            return false;

        transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);

        // cek goal
        GameObject[] goals = GameObject.FindGameObjectsWithTag("Goal");
        foreach (var g in goals)
        {
            Vector2 goalPos = new Vector2(Mathf.Round(g.transform.position.x), Mathf.Round(g.transform.position.y));
            Goal goalScript = g.GetComponent<Goal>();
            if (goalScript != null)
            {
                goalScript.isOccupied = ((Vector2)transform.position == goalPos);
            }
        }
        // Setelah transform.position dipindahkan
        GameObject[] holes = GameObject.FindGameObjectsWithTag("Hole");
        foreach (var h in holes)
        {
            Vector2 holePos = new Vector2(Mathf.Round(h.transform.position.x), Mathf.Round(h.transform.position.y));
            if ((Vector2)transform.position == holePos)
            {
                Hole holeScript = h.GetComponent<Hole>();
                if (holeScript != null && !holeScript.isFilled)
                {
                    holeScript.Fill(gameObject);
                }
            }
        }


        return true;
    }

    public bool ObjToBlocked(Vector3 position, Vector2 direction)
    {
        Vector2 targetPos = new Vector2(
            Mathf.Round(position.x + direction.x),
            Mathf.Round(position.y + direction.y)
        );

        foreach (var obj in obstacles)
        {
            if (obj == null) continue; // ⬅️ skip kalau sudah dihancurkan

            Vector2 obsPos = new Vector2(
                Mathf.Round(obj.transform.position.x),
                Mathf.Round(obj.transform.position.y)
            );
            if (targetPos == obsPos) return true;
        }

        foreach (var objToPush in objects)
        {
            if (objToPush == null || objToPush == gameObject) continue; // ⬅️ skip null & diri sendiri

            Vector2 objPos = new Vector2(
                Mathf.Round(objToPush.transform.position.x),
                Mathf.Round(objToPush.transform.position.y)
            );
            if (targetPos == objPos) return true;
        }

        return false;
    }
}
