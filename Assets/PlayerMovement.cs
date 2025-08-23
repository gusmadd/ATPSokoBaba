using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject[] obstacles;
    private GameObject[] objects;

    private bool readyToMove = true;
    public float moveCooldown = 0.15f; // delay biar gerakan step-by-step

    void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
        objects = GameObject.FindGameObjectsWithTag("Objects");
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moveInput.sqrMagnitude > 0.5f && readyToMove)
        {
            readyToMove = false;
            Move(moveInput);
            Invoke(nameof(ResetMove), moveCooldown);
        }
    }

    void ResetMove()
    {
        readyToMove = true;
    }

    public bool Move(Vector2 direction)
    {
        // Hanya gerak horizontal atau vertikal
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            direction = new Vector2(Mathf.Sign(direction.x), 0);
        else
            direction = new Vector2(0, Mathf.Sign(direction.y));

        // Cek kalau posisi depan terhalang
        if (!Blocked(transform.position, direction))
        {
            transform.position += (Vector3)direction;
            return true;
        }

        return false;
    }

    public bool Blocked(Vector3 position, Vector2 direction)
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

        // Cek object (box)
        foreach (var objToPush in objects)
        {
            if (objToPush.transform.position.x == newPos.x && objToPush.transform.position.y == newPos.y)
            {
                Push objPush = objToPush.GetComponent<Push>();
                if (objPush != null)
                {
                    if (!objPush.Move(direction))
                        return true; // kalau box tidak bisa geser → blocked
                }
            }
        }

        return false;
    }
}
