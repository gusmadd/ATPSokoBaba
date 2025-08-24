using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    private bool levelCompleted = false;
    private bool readyToCheck = false;
    private bool gameFinished = false;
    [Header("UI")]
    public GameObject finishText;

    void Start()
    {
        Invoke("EnableCheck", 0.2f);
        if (finishText != null)
            finishText.gameObject.SetActive(false); // sembunyikan saat mulai

    }

    void EnableCheck()
    {
        readyToCheck = true;
    }

    void Update()
    {
        // kalau sudah selesai semua level, cek input Enter
        if (gameFinished)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(0); // balik ke Level 1
            }
            return; // berhenti di sini supaya tidak cek goal lagi
        }

        if (!readyToCheck || levelCompleted) return;

        Goal[] goals = GameObject.FindObjectsOfType<Goal>();
        bool allOccupied = true;

        foreach (var g in goals)
        {
            if (!g.isOccupied)
            {
                allOccupied = false;
                Debug.Log("Goal belum terisi: " + g.name);
                break;
            }
        }

        if (allOccupied)
        {
            Debug.Log("SEMUA GOAL TERISI!");
            levelCompleted = true;
            Invoke("LoadNextLevel", 0.5f);
        }
    }

    void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            gameFinished = true;
            if (finishText != null)
            {
                finishText.gameObject.SetActive(true);
            }
        }
    }
}