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

        if (tankHealth != null)
        {
            tankHealth.OnDeathPlayer += SaveMyScore;
        }
    }

    private void OnDisable()
    {
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
    }
}