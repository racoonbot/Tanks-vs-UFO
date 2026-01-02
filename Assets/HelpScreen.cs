using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScreen : MonoBehaviour
{
   public GameObject HelpScreenUI;
   public void ShowHelp()
   {
      gameObject.SetActive(true);
      Debug.Log(HelpScreenUI.active);
   }

   public void HideHelp()
   {
      gameObject.SetActive(false);
      Debug.Log(HelpScreenUI.active);
   }
}
