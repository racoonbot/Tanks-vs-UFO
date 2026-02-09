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
        if (!PlayerPrefs.HasKey(PlayerPrefKey))
        {
            YG2.onGetLeaderboard += OnLeaderboardDataReceived;
            waitingForServer = true;
            YG2.GetLeaderboard(LeaderboardName);
        }
    }

    private void OnDestroy()
    {
        if (waitingForServer)
            YG2.onGetLeaderboard -= OnLeaderboardDataReceived;
    }

    private void OnLeaderboardDataReceived(LBData data)
    {
        if (!waitingForServer) return;
        waitingForServer = false;
        YG2.onGetLeaderboard -= OnLeaderboardDataReceived;

        if (data == null)
        {
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
        PlayerPrefs.SetInt(PlayerPrefKey, serverRecordForPlayer);
        PlayerPrefs.Save();
    }
}