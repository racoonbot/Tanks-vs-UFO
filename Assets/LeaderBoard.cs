using UnityEngine;
using YG;

public class LeaderBoard : MonoBehaviour
{
    private TankHealth tankHealth;
    private Wallet wallet;

    private const string LeaderboardName = "testlb";

    private void Awake()
    {
        tankHealth = FindObjectOfType<TankHealth>();
        wallet = FindObjectOfType<Wallet>();
    }

    private void OnEnable()
    {
        if (tankHealth != null)
        {
            tankHealth.OnDeathPlayer -= SaveMyScore;
            tankHealth.OnDeathPlayer += SaveMyScore;
        }
        else
        {
            Debug.LogWarning("LeaderBoard: TankHealth not found.");
        }

        if (wallet == null)
            Debug.LogWarning("LeaderBoard: Wallet not found.");
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
        if (wallet == null)
        {
            Debug.LogError("LeaderBoard: Cannot save score — wallet is null.");
            return;
        }

        int currentMoney = wallet.totalMoney;
        int recordMoney = PlayerPrefs.GetInt("recordMoney", 0);

        if (currentMoney <= 0)
        {
            Debug.Log("LeaderBoard: currentMoney is zero or negative, skipping save.");
            return;
        }

        if (recordMoney < currentMoney)
        {
            PlayerPrefs.SetInt("recordMoney", currentMoney);
            PlayerPrefs.Save();

            if (YG2.isSDKEnabled)
            {
                YG2.SetLeaderboard(LeaderboardName, currentMoney);
                Debug.Log($"LeaderBoard: New local record {currentMoney} saved and sent to leaderboard '{LeaderboardName}'.");
            }
            else
            {
                Debug.LogWarning($"LeaderBoard: SDK not ready. Local record {currentMoney} saved, but not sent to YG.");
            }
        }
        else
        {
            Debug.Log($"LeaderBoard: Current money ({currentMoney}) is not greater than record ({recordMoney}).");
        }
    }
}
