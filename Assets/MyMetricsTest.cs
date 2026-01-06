using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MyMetricsTest : MonoBehaviour
{
    public void TutorMetrica()
    {
        YG2.MetricaSend("Tutorial");
    }
}
