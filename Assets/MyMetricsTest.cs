using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MyMetricsTest : MonoBehaviour
{
    public void RotateMetrica()
    {
        YG2.MetricaSend("RotateUpgrade");
    }
    public void MaxHealthMetrica()
    {
        YG2.MetricaSend("MaxHealthUpgrade");
    }
 
    public void SpeedMetrica()
    {
        YG2.MetricaSend("SpeedUpgrade");
    }
}
