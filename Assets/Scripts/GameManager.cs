using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Loot Locker Settings")]
    [SerializeField][Tooltip("Get leader board key from Loot Locker")]
    private string leaderboardKey = "scoreleaderboards";
    [SerializeField]
    [Tooltip("Get leader board ID from Loot Locker")]
    private int leaderboardID = 17681;

    //  The ID for guest accounts, use for testing
    [SerializeField]
    private string guestID;

    [SerializeField]
    private bool primeGameManager = false;

    private GameManager[] managerCheck;

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

    public void uploadScoretoLeaderboards(string memberID, int score, string name)
    {
        string metadata = Application.systemLanguage.ToString();



        LootLockerSDKManager.SubmitScore(memberID, score, leaderboardKey, name, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Score successfully uploaded!");
            }
            else
            {
                Debug.Log("Score failed to upload" + response.errorData.message);
            }
        });
    }

    public void playerDies()
    {
        EnemyManager enemyManager = (EnemyManager)FindObjectOfType(typeof(EnemyManager));
        enemyManager.enabled = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Destroy(GameObject.FindGameObjectWithTag("Macrophage"));
        for(int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
        uploadScoretoLeaderboards(guestID, totalScore, "Guest");
        loseScreenScript.gameOverScreen();
    }

    public void restartLevel()
    {
        totalScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    [System.Obsolete]
    public string getHighLeaderboard()
    {
        string leaderboard = "";
        int leaderboardLoad = 50;
        LootLockerSDKManager.GetScoreList(leaderboardID, leaderboardLoad, (response) =>
        {
            if (response.statusCode == 200)
            {
                for(int i = 0; i < leaderboardLoad; i++)
                {
                    LootLockerLeaderboardMember currentItem = response.items[i];
                    leaderboard += currentItem.rank + ". " + currentItem.metadata + " - " + currentItem.score + "\n";
                }
                Debug.Log("Get scores success!");
            }
            else
            {
                Debug.Log("Score failed to upload" + response.errorData.message);
            }
        });


        return leaderboard;
    }

    private void Awake()
    {
        if (primeGameManager == false)
        {
            managerCheck = (GameManager[])FindObjectsOfType(typeof(GameManager));
            if (managerCheck.Length >= 2)
            {
                Destroy(this.gameObject);
            }
            else
            {
                primeGameManager = true;
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    private void Start()
    {
        guestLogin();
        loseScreenScript = (LoseScreenScript)FindObjectOfType(typeof(LoseScreenScript));
    }
}
