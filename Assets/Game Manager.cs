using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string[] levels = { "Level 1", "Level 2", "Level 3", "Level 4", "Level 5" };
    private bool levelCompleted = false;
    private bool readyToCheck = false;

    void Start()
    {
        // Delay kecil biar box/goal stabil sebelum cek win
        Invoke("EnableCheck", 0.2f);
    }

    void EnableCheck()
    {
        readyToCheck = true;
    }

    void Update()
    {
        if (!readyToCheck || levelCompleted) return;

        Goal[] goals = GameObject.FindObjectsOfType<Goal>();
bool allOccupied = true;
foreach (var g in goals)
{
    if (!g.isOccupied)
    {
        allOccupied = false;
        break;
    }
}


        if (allOccupied)
        {
            levelCompleted = true;
            Invoke("LoadNextLevel", 0.5f); // delay 0.5 detik sebelum pindah level
        }
    }

    void LoadNextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        int index = System.Array.IndexOf(levels, currentScene);

        if (index >= 0 && index < levels.Length - 1)
        {
            SceneManager.LoadScene(levels[index + 1]);
        }
        else
        {
            Debug.Log("Semua level selesai!");
            SceneManager.LoadScene(levels[0]); // restart ke Level1
        }
    }
}
