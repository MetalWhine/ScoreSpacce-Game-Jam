using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Loot Locker Settings")]
    [SerializeField][Tooltip("Get leader board key from Loot Locker")]
    private string leaderboardKey = "playerleaderboard";
    [SerializeField]
    [Tooltip("Get leader board ID from Loot Locker")]
    private int leaderboardID = 17697;

    //  The ID for guest accounts, use for testing
    [SerializeField]
    private string guestID;

    public static string playerName = "Guest";
    
    public int totalScore;

    private LoseScreenScript loseScreenScript;

    public string getGuestID()
    {
        return guestID;
    }

    private void guestLogin()
    {
        if(guestID == "")
        {
            LootLockerSDKManager.StartGuestSession((response) =>
            {
                if (!response.success)
                {
                    Debug.Log("Guest login failed: " + response.errorData.message);
                }
                else
                {
                    Debug.Log("Guest login success");
                    guestID = response.player_id.ToString();
                }
            });
        }
    }

    public string getLeaderboardKey()
    {
        return leaderboardKey;
    }

    public void uploadScoretoLeaderboards(string memberID, int score, string name)
    {
        string metadata = Application.systemLanguage.ToString();

        LootLockerSDKManager.SubmitScore(memberID, score, leaderboardKey, name, (response) =>
        {
            if (response.success)
            {
                loseScreenScript.getHighLeaderboard();
                loseScreenScript.getLocalLeaderboard();
                Debug.Log("Score successfully uploaded!");
            }
            else
            {
                Debug.LogError("Score failed to upload" + response.errorData.message);
            }
        });
    }

    public void playerDies()
    {
        EnemyManager enemyManager = (EnemyManager)FindObjectOfType(typeof(EnemyManager));
        enemyManager.enabled = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] Macrophages = GameObject.FindGameObjectsWithTag("Macrophage");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        for (int i = 0; i < Macrophages.Length; i++)
        {
            Destroy(Macrophages[i]);
        }
        GameObject[] protein = GameObject.FindGameObjectsWithTag("Protein");
        for (int i = 0; i < protein.Length; i++)
        {
            Destroy(protein[i]);
        }
        loseScreenScript.gameOverScreen();
        uploadScoretoLeaderboards(guestID, totalScore, playerName);
        FindObjectOfType<AudioManager>().StopPlaying("Play Music");
    }

    public void restartLevel()
    {
        totalScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void goToGame()
    {
        FindObjectOfType<AudioManager>().Play("Play Music");
        FindObjectOfType<AudioManager>().StopPlaying("Menu Music");
        SceneManager.LoadScene("Game Scene");
    }

    public void quitApplication()
    {
        Application.Quit();
    }

    private void Start()
    {
        guestLogin();
        loseScreenScript = (LoseScreenScript)FindObjectOfType(typeof(LoseScreenScript));
    }
}
