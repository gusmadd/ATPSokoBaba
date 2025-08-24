using UnityEngine;

public class Goal : MonoBehaviour
{
    public string goalColor; // "Red", "Blue", dll
    public bool isOccupied = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Box box = collision.GetComponent<Box>();
        if (box != null && box.boxColor == goalColor)
        {
            // Kalau box masuk dan goal masih kosong, tandai occupied
            if (!isOccupied)
            {
                isOccupied = true;
                Debug.Log("✅ Goal " + goalColor + " ditempati " + box.name);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Box box = collision.GetComponent<Box>();
        if (box != null && box.boxColor == goalColor)
        {
            // Kalau box keluar, goal jadi kosong lagi
            isOccupied = false;
            Debug.Log("❌ Goal " + goalColor + " kosong lagi");
        }
    }
}
