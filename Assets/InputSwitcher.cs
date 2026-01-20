using UnityEngine;
using YG;

public class InputSwitcher : MonoBehaviour
{
    [Header("Джойстик для мобилок")]
    public GameObject mobileJoystick; 

    private void Start()
    {
        if (YG2.isSDKEnabled)
        {
            ApplyControlSettings();
        }
    }
    
    private void OnEnable() => YG2.onGetSDKData += ApplyControlSettings;
    private void OnDisable() => YG2.onGetSDKData -= ApplyControlSettings;

    void ApplyControlSettings()
    {
        bool isTouchDevice = YG2.envir.isMobile || YG2.envir.isTablet;
        if (mobileJoystick != null)
        {
//mobileJoystick.SetActive(isTouchDevice);
        }
        if (isTouchDevice)
        {
            Cursor.visible = false; 
            Cursor.lockState = CursorLockMode.None;
        }
       
    }
}