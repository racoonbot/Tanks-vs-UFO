using UnityEngine;

public class EnChatMessageGenerator : MonoBehaviour
{
    private string[] phrases = new string[]
    {
        // --- Group 1: Standard shouts ---
        "Ouch!", "That hurts!", "Why?!", "Oh!", "Stop it!",
        "Leave me alone!", "Don't hit!", "Help!", "Mom!", "Save me!",
        "Hey!", "What are you bothering me for?", "I'm empty!", "No!", "Stop!",
        "Enough!", "Go away!", "Shoo!", "Leave me!", "It hurts!",
        // --- Group 2: Accusations of cheating (classic) ---
        "Cheater!", "Wallhack?", "Turn off the aim", "Report", "Scripts!",
        "I recorded everything!", "You're banned", "Admin, check him", "Triggerbot?", "Callout!",
        "Where's the spread?", "W h off", "Got exposed", "You cheater", "VAC's coming",
        "Filed a report", "My uncle works in bans", "Cheats off", "Through walls?", "How?!",

// --- Group 3: Complaints about lag and hardware ---
        "I'm lagging!", "My ping is 900!", "I'm stuttering...", "Packet loss!", "Mouse died",
        "I spilled my keyboard", "FPS dropped", "Internet is slow", "Stuck in textures", "Teleports...",
        "Server is lagging", "Not me, it's the ping", "I'm lagging!", "Frozen!", "Screen went black",
        "Cat on the keyboard", "Mom called me", "Alt-tabbed", "Glitch!", "Desync!",

// --- Group 4: Complaints about balance and gameplay ---
        "OP!", "Needs a nerf!", "Where's the balance?", "What's this damage?", "Unbalanced!",
        "They'll fix it in a patch", "Bug abuser!", "Broken!", "Overpowered class", "Damage is insane",
        "All crits...", "Random!", "Why me?", "Too strong", "My armor is cardboard",

// --- Group 5: Team and healing ---
        "Where's the heal?!", "Healer's sleeping!", "Team is trash", "Where's support?", "I'm solo",
        "No help", "Tank it!", "Take him off me!", "Aggro lost", "Where's the party?",

// --- Group 6: Slang and emotions ---
        "GG WP", "FF", "I'm leaving", "AFK", "OMG",
        "LOL", "WTF?", "Kek", "Noob", "Rag",
        "Lucky", "Skill check?", "Got lucky", "Rat!", "Anyone can backstab",
        "Want a 1v1?", "Duel or scared?", "Weak", "Easy for you", "Sigh...",

// --- Group 7: Funny and silly ---
        "Don't touch, it's for New Year!", "I'm just looking!", "I have kids!", "Give me a medkit!", "Misclick!",
        "Hands are shaking", "My eye itched", "I'm a pacifist!", "I surrender!", "Not to the face!",
        "Left my armor at home", "I was counting clouds", "Unlucky", "Spare me!", "Game Over"
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