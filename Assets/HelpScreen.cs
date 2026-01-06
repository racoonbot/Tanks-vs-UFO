using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScreen : MonoBehaviour
{
   public GameObject HelpScreenUI;
   public void ShowHelp()
   {
      HelpScreenUI.SetActive(true);
   
   }

   public void HideHelp()
   {
      HelpScreenUI.SetActive(false);
      
   }
}
