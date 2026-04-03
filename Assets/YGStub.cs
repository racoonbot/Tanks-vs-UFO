using UnityEngine;
using System;
using System.Collections.Generic;

namespace YG
{
    namespace Utils
    {
        namespace LB
        {
            public class LBData 
            { 
                public List<LBData> players = new List<LBData>();
                public string name;
                public int score;
                public int rank;
                public string uniqueID = "0"; 
            }
        }
        public class LBData : LB.LBData { }
    }

    public class YG2 : MonoBehaviour
    {
        public static string lang = "ru";
        public static bool isSDKEnabled = true;
        
        public static Envir envir = new Envir();
        public static Player player = new Player();

        // --- СОБЫТИЯ (Actions) ---
        
        // Добавлено для исправления ошибок в InputSwitcher
        public static Action onGetSDKData = delegate { }; 
        
        // Добавлено для LeaderBoardSync
        public static Action<Utils.LB.LBData> onGetLeaderboard = delegate { }; 

        // ИСПРАВЛЕНО: Добавлен параметр <string>, чтобы подойти под метод ApplyLanguage(string lang)
        // Если твой метод не принимает строку, просто удали <string> ниже
        public static Action<string> onCorrectLang = delegate { }; 

        // --- МЕТОДЫ ---

        public static void MetricaSend(string name) { }
        public static void Message(string message) { }
        public static void InterstitialAdvShow() { }

        public static void GetLeaderboard(string name = "", int maxQuantity = 1, int quantityTop = 1, string quantityAround = "", string photoSize = "") { }
        public static void SetLeaderboard(string name, int score) { }

        public static string GetFlag(string name) { return "true"; }

        // --- ВСПОМОГАТЕЛЬНЫЕ КЛАССЫ ---

        public class Envir
        {
            public bool isMobile = false;
            public bool isDesktop = true;
            public bool isTablet = false; 
            public string deviceType = "desktop"; 
        }

        public class Player
        {
            public bool auth = false;
            public string name = "Player";
            public string id = "0"; 
        }
    }
}