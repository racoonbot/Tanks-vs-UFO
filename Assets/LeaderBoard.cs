using UnityEngine;
using YG;
using YG.Utils.LB; // Нужно для работы с LBData и LBPlayerData

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
        
        YG2.onGetLeaderboard += OnLeaderboardDataReceived;
        if (tankHealth != null)
        {
            tankHealth.OnDeathPlayer += SaveMyScore;
        }
    }

    private void OnDisable()
    {
        YG2.onGetLeaderboard -= OnLeaderboardDataReceived;
        
        if (tankHealth != null)
        {
            tankHealth.OnDeathPlayer -= SaveMyScore;
        }
    }
    private void SaveMyScore()
    {
        pendingScoreToSave = wallet.totalMoney;
        isCheckingScore = true;
        YG2.GetLeaderboard(LeaderboardName);
    }

    private void OnLeaderboardDataReceived(LBData data)
    {
        if (!isCheckingScore || data.technoName != LeaderboardName) return;

        isCheckingScore = false; 
        int oldRecord = 0;
        if (data.players != null)
        {
            foreach (var entry in data.players)
            {
                if (entry.name == YG2.player.id)
                {
                    oldRecord = entry.score; 
                    break; 
                }
            }
        }

        if (pendingScoreToSave > oldRecord)
        {
            YG2.SetLeaderboard(LeaderboardName, pendingScoreToSave);
        }
    }
}