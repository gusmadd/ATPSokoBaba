using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class PlayerMovement : MonoBehaviour
{
    private GameObject[] obstacles;
    private GameObject[] objects;
    private bool readyToMove = true;
    public float moveDelay = 0.15f;

    void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("Obstacles");
        objects = GameObject.FindGameObjectsWithTag("Objects");
    }

    void Update()
    {
        // Input gerak
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (moveInput.sqrMagnitude > 0.5f && readyToMove)
        {
            readyToMove = false;

            if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
                moveInput = new Vector2(Mathf.Sign(moveInput.x), 0);
            else
                moveInput = new Vector2(0, Mathf.Sign(moveInput.y));

            Move(moveInput);

            Invoke("ResetMove", moveDelay);
        }

        // 🔄 Restart level kalau tekan R
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name); // reload scene yang sedang aktif
        }
    }


    void ResetMove()
    {
        readyToMove = true;
    }

    public void Move(Vector2 direction)
    {
        Vector2 targetPos = new Vector2(
            Mathf.Round(transform.position.x + direction.x),
            Mathf.Round(transform.position.y + direction.y)
        );

        if (!Blocked(transform.position, direction))
        {
            transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);
        }
    }

    public bool Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 targetPos = new Vector2(
            Mathf.Round(position.x + direction.x),
            Mathf.Round(position.y + direction.y)
        );

        foreach (var obj in obstacles)
        {
            Vector2 obsPos = new Vector2(Mathf.Round(obj.transform.position.x), Mathf.Round(obj.transform.position.y));
            if (targetPos == obsPos) return true;
        }

        foreach (var objToPush in objects)
        {
            if (objToPush == null) continue; // ⬅️ Skip kalau sudah Destroy

            Vector2 objPos = new Vector2(
                Mathf.Round(objToPush.transform.position.x),
                Mathf.Round(objToPush.transform.position.y)
            );

            if (targetPos == objPos)
            {
                Push objPush = objToPush.GetComponent<Push>();
                if (objPush != null && !objPush.Move(direction)) return true;
            }
        }
        GameObject[] holes = GameObject.FindGameObjectsWithTag("Hole");
        foreach (var h in holes)
        {
            Hole holeScript = h.GetComponent<Hole>();
            Vector2 holePos = new Vector2(Mathf.Round(h.transform.position.x), Mathf.Round(h.transform.position.y));

            // Kalau hole kosong → treat sebagai obstacle
            if ((Vector2)targetPos == holePos && holeScript != null && !holeScript.isFilled)
                return true;
        }


        return false;
    }
}
