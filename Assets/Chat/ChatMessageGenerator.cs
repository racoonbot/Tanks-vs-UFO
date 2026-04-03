using UnityEngine;

public class ChatMessageGenerator : MonoBehaviour
{
   private string[] phrases = new string[]
{
    // --- Group 1: Standard Shouts ---
    "Ouch!", "That hurts!", "What for?!", "Oh!", "Stop it!", 
    "Go away!", "Don't hit me!", "Help!", "Mommy!", "Save me!",
    "Ow!", "Why you bully me?", "I'm empty!", "No!", "Stop!",
    "Enough!", "Leave!", "Shoo!", "Leave me alone!", "It hurts!",

    // --- Group 2: Cheating Accusations ---
    "Cheater!", "Wallhack?", "Turn off aim", "Reported", "Scripts!",
    "I recorded everything!", "Enjoy your ban", "Admin, check him", "Triggerbot?", "Aim assist!",
    "Where's the recoil?", "Wallhack off", "Caught you!", "Nice soft, bro", "VAC is coming",
    "Report sent", "My uncle works at Valve", "Turn off cheats", "Through walls?", "How?!",

    // --- Group 3: Lag and Hardware Complaints ---
    "I'm lagging!", "Ping 900!", "I have freezes...", "Packet loss", "Mouse died",
    "Spilled drink on keys", "Low FPS", "Internet is trash", "Stuck in textures", "Teleporting...",
    "Server lag", "It's not me, it's the ping", "I'm laggy!", "Frozen!", "Screen went black",
    "Cat on keyboard", "Mom called me", "Alt-tabbed", "Glitch!", "Desync!",

    // --- Group 4: Balance and Game Complaints ---
    "Imba!", "Needs a nerf!", "Where's the balance?", "What's this damage?", "Broken!",
    "Wait for the patch", "Bug abuser!", "Game is broken!", "OP class", "Insane damage",
    "Only crits...", "RNG!", "Why me?", "Too strong", "My armor is made of paper",

    // --- Group 5: Team and Healing ---
    "Where's the heal?!", "Healer is asleep!", "Trash team", "Where's support?", "I'm playing solo",
    "Zero help", "Tank it!", "Get him off me!", "Lost aggro", "Where's the party?",

    // --- Group 6: Slang and Emotions ---
    "GG WP", "FF", "I'm leaving", "AFK", "OMG", 
    "LOL", "WTF?", "Kek", "Noob", "Potato",
    "Lucker", "Skill issue?", "Lucky", "Rat!", "Backstabber",
    "1v1 me!", "PvP or scared?", "Weak", "Easy for you", "Sigh...",

    // --- Group 7: Funny and Ridiculous ---
    "Don't touch, it's for New Year!", "I'm just looking!", "I have kids!", "Give me a medkit!", "Missclick!",
    "Hands are shaking", "My eye is itchy", "I'm a pacifist!", "I surrender!", "Not the face!",
    "Left my armor at home", "I was counting clouds", "Bad luck", "Mercy!", "Game Over"
};

    public string Generate(string nick, Color color)
    {
        int sayOrNotChance = Random.Range(0, 7);
        if (sayOrNotChance <= 3)
        {
            string msg = phrases[Random.Range(0, phrases.Length)];
            string colorHex = ColorUtility.ToHtmlStringRGB(color);
            return $"<color=#{colorHex}><b>{nick}:</b></color> {msg}";  
        }
       return null;
    }
}