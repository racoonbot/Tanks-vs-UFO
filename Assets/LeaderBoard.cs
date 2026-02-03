using UnityEngine;
using YG;

public class LeaderBoard : MonoBehaviour
{
    private TankHealth tankHealth;
    private Wallet wallet;

    private const string LeaderboardName = "testlb";
    private const string PlayerPrefKey = "recordMoney";

    private void Start()
    {
        Debug.Log("[LeaderBoard] Start()");
        tankHealth = FindObjectOfType<TankHealth>();
        wallet = FindObjectOfType<Wallet>();
        Debug.Log($"[LeaderBoard] Found tankHealth={(tankHealth!=null)}, wallet={(wallet!=null)}");

        if (tankHealth != null)
        {
            tankHealth.OnDeathPlayer += SaveMyScore;
            Debug.Log("[LeaderBoard] Subscribed to tankHealth.OnDeathPlayer");
        }
    }

    private void OnDisable()
    {
        Debug.Log("[LeaderBoard] OnDisable()");
        if (tankHealth != null)
        {
            tankHealth.OnDeathPlayer -= SaveMyScore;
            Debug.Log("[LeaderBoard] Unsubscribed from tankHealth.OnDeathPlayer");
        }
    }

    private void SaveMyScore()
    {
        Debug.Log("[LeaderBoard] SaveMyScore() called");
        if (wallet == null)
        {
            Debug.LogWarning("[LeaderBoard] wallet is null — aborting SaveMyScore");
            return;
        }

        int current = wallet.totalMoney;
        int localRecord = PlayerPrefs.GetInt(PlayerPrefKey, 0);
        Debug.Log($"[LeaderBoard] current={current}, localRecord={localRecord}");

        if (current > localRecord)
        {
            PlayerPrefs.SetInt(PlayerPrefKey, current);
            PlayerPrefs.Save();
            Debug.Log($"[LeaderBoard] Updated PlayerPrefs {PlayerPrefKey} -> {current}");

            // Убедимся, что YG2 готов (опционально) — если YG2.player == null, всё равно попытаемся отправить
            Debug.Log($"[LeaderBoard] Sending to YG2. YG2.player != null: {(YG2.player!=null)}");
            YG2.SetLeaderboard(LeaderboardName, current);
            Debug.Log("[LeaderBoard] YG2.SetLeaderboard called");
        }
        else
        {
            Debug.Log("[LeaderBoard] Current score is not higher than local record — not sending");
        }
    }
}
