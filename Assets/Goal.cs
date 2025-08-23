using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isOccupied = false;

    void Update()
    {
        // cek apakah ada box di posisi goal ini
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Objects");
        isOccupied = false;

        foreach (var box in boxes)
        {
            Vector2 boxPos = new Vector2(Mathf.Round(box.transform.position.x), Mathf.Round(box.transform.position.y));
            Vector2 goalPos = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));

            if (boxPos == goalPos)
            {
                isOccupied = true;
                break;
            }
        }
    }
}
