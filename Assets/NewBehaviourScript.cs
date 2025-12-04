using UnityEngine;
using ClassLibrary1; 

public class LibraryTest : MonoBehaviour
{
    private void Start()
    {
      
        Class1 class1Instance = new Class1();
         string message = class1Instance.GetMessage();
        Debug.Log(message);
    }

    
}