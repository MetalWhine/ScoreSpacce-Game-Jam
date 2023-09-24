using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

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

    public int totalScore;
    
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

    public void uploadScoretoLeaderboards(string memberID, int score)
    {
        LootLockerSDKManager.SubmitScore(memberID, score, leaderboardKey, (response) =>
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
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        guestLogin();
    }
}
