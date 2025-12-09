using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Shop/Upgrade Item")]
public class UpgradeItem : ScriptableObject 
{
    public string title;
    public int price;   
    public UpgradeType type;
    public float amount;
}