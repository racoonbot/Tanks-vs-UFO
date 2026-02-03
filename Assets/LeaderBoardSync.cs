using UnityEngine;
using YG;
using YG.Utils.LB;

public class LeaderBoardSync : MonoBehaviour
{
    private const string PlayerPrefKey = "recordMoney";
    private const string LeaderboardName = "testlb";
    private bool waitingForServer;

    private void Start()
    {
        Debug.Log("[LeaderBoardSync] Start()");
        if (!PlayerPrefs.HasKey(PlayerPrefKey))
        {
            Debug.Log("[LeaderBoardSync] No local record found — requesting server leaderboard");
            YG2.onGetLeaderboard += OnLeaderboardDataReceived;
            waitingForServer = true;
            YG2.GetLeaderboard(LeaderboardName);
        }
        else
        {
            Debug.Log("[LeaderBoardSync] Local record exists: " + PlayerPrefs.GetInt(PlayerPrefKey));
        }
    }

    private void OnDestroy()
    {
        if (waitingForServer)
            YG2.onGetLeaderboard -= OnLeaderboardDataReceived;
    }

    private void OnLeaderboardDataReceived(LBData data)
    {
        Debug.Log("[LeaderBoardSync] OnLeaderboardDataReceived()");
        if (!waitingForServer) return;
        waitingForServer = false;
        YG2.onGetLeaderboard -= OnLeaderboardDataReceived;

        if (data == null)
        {
            Debug.LogWarning("[LeaderBoardSync] Received null leaderboard data");
            return;
        }

        int serverRecordForPlayer = 0;
        if (data.players != null)
        {
            foreach (var entry in data.players)
            {
                if (entry.uniqueID == (YG2.player != null ? YG2.player.id : null))
                {
                    serverRecordForPlayer = entry.score;
                    break;
                }
            }
        }

        Debug.Log($"[LeaderBoardSync] Server record for player = {serverRecordForPlayer}");
        PlayerPrefs.SetInt(PlayerPrefKey, serverRecordForPlayer);
        PlayerPrefs.Save();
        Debug.Log("[LeaderBoardSync] Saved server record into PlayerPrefs");
    }
}