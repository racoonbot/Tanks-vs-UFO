using UnityEngine;
using YG;
using YG.Utils.LB;

public class LeaderBoard : MonoBehaviour
{
    private TankHealth tankHealth;
    private Wallet wallet;

    private int pendingScoreToSave;
    private bool isCheckingScore;

    private const string LeaderboardName = "testlb";

    private void Start()
    {
        tankHealth = FindObjectOfType<TankHealth>();
        wallet = FindObjectOfType<Wallet>();
        
        /*
        YG2.onGetLeaderboard += OnLeaderboardDataReceived; // 1*/
        if (tankHealth != null)
        {
            tankHealth.OnDeathPlayer += SaveMyScore;
        }
    }

    private void OnDisable()
    {
        /*YG2.onGetLeaderboard -= OnLeaderboardDataReceived; // 1*/
        
        if (tankHealth != null)
        {
            tankHealth.OnDeathPlayer -= SaveMyScore;
        }
    }
    private void SaveMyScore()
    {
        int recordMoney = PlayerPrefs.GetInt("recordMoney", 0);
        if (recordMoney < wallet.totalMoney)
        {
            PlayerPrefs.SetInt("recordMoney", wallet.totalMoney);
            YG2.SetLeaderboard(LeaderboardName, wallet.totalMoney);
        }
        
        /*
        pendingScoreToSave = wallet.totalMoney;
        isCheckingScore = true;
        YG2.GetLeaderboard(LeaderboardName);
        // 3 строки сверху это 1
        */
         
        //ниже 6 строк это 2
       
    }

    /*private void OnLeaderboardDataReceived(LBData data) // 1
    {
        if (!isCheckingScore || data.technoName != LeaderboardName) return;

        isCheckingScore = false; 
        int oldRecord = 0;
        if (data.players != null)
        {
            foreach (var entry in data.players)
            {
                if (entry.uniqueID == YG2.player.id)
                {
                    oldRecord = entry.score; 
                    break; 
                }
            }
        }*/

        /*if (pendingScoreToSave > oldRecord)
        {
            YG2.SetLeaderboard(LeaderboardName, pendingScoreToSave);
        }
    }*/
}