using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LootLocker.Requests;

public class LoseScreenScript : MonoBehaviour
{

    private Animator animator;
    private GameManager gameManager;

    [SerializeField]
    private TMP_Text score;

    [SerializeField]
    private TMP_Text localLeaderboard;

    [SerializeField]
    private TMP_Text globalLeaderboad;

    private void Start()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        animator = GetComponent<Animator>();
    }

    public void getHighLeaderboard()
    {
        string leaderboard = "";
        int leaderboardLoad = 50;

        LootLockerSDKManager.GetScoreList(gameManager.getLeaderboardKey(), leaderboardLoad, (response) =>
        {
            if (response.statusCode == 200)
            {
                for (int i = 0; i < response.items.Length; i++)
                {
                    LootLockerLeaderboardMember currentItem = response.items[i];
                    leaderboard += currentItem.rank + ". " + currentItem.metadata + " - " + currentItem.score + "\n";
                }
                globalLeaderboad.text = leaderboard;
                Debug.Log("Get scores success!");
            }
            else
            {
                Debug.Log("Score failed to upload" + response.errorData.message);
            }
        });
    }

    public void getLocalLeaderboard()
    {
        string leaderboard = "";
        LootLockerSDKManager.GetMemberRank(gameManager.getLeaderboardKey(), gameManager.getGuestID(), (response) =>
        {
            if (response.statusCode == 200)
            {
                int rank = response.rank;
                int count = 10;
                int after = rank < 6 ? 0 : rank - 5;

                LootLockerSDKManager.GetScoreList(gameManager.getLeaderboardKey(), count, after, (response) =>
                {
                    if (response.statusCode == 200)
                    {
                        for (int i = 0; i < response.items.Length; i++)
                        {
                            LootLockerLeaderboardMember currentItem = response.items[i];
                            if(currentItem.rank == rank)
                            {
                                leaderboard += "<color=yellow>";
                            }
                            leaderboard += currentItem.rank + ". " + currentItem.metadata + " - " + currentItem.score + "\n";
                        }
                        localLeaderboard.text = leaderboard;
                        Debug.Log("Get scores success!");
                    }
                    else
                    {
                        Debug.Log("failed: " + response.errorData.message);
                    }
                });
            }
            else
            {
                Debug.Log("failed: " + response.errorData.message);
            }
        });
    }

    public void gameOverScreen()
    {
        animator.SetTrigger("Game Over");
        score.text = gameManager.totalScore.ToString();
    }

}
