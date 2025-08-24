using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public bool isFilled = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isFilled) return;

        Box box = collision.GetComponent<Box>();
        if (box != null)
        {
            isFilled = true;

            // Ambil warna dari box sebelum dihancurkan
            SpriteRenderer boxSr = box.GetComponent<SpriteRenderer>();
            SpriteRenderer holeSr = GetComponent<SpriteRenderer>();

            if (boxSr != null && holeSr != null)
            {
                holeSr.color = boxSr.color; // hole berubah warna sesuai box
            }
            BoxCollider2D boxCol = box.GetComponent<BoxCollider2D>();
            if (boxCol != null)
                boxCol.enabled = false;

            // Hapus box yang jatuh
            Destroy(collision.gameObject, 0.05f);

            // Ubah collider jadi solid biar bisa dilewati player/box lain
            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
                col.isTrigger = false;
        }
    }
    public void Fill(GameObject box)
    {
        if (isFilled) return;

        isFilled = true;

        // Ganti warna hole sesuai box
        SpriteRenderer boxSr = box.GetComponent<SpriteRenderer>();
        SpriteRenderer holeSr = GetComponent<SpriteRenderer>();

        if (boxSr != null && holeSr != null)
            holeSr.color = boxSr.color;

        // Hapus box
        Destroy(box, 0.05f);

        // Ubah collider jadi solid
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.isTrigger = false;
    }

}
