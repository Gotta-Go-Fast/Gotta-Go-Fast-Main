using UnityEngine;
using System.Collections;

public class CountdownManagerScript : MonoBehaviour
{
    private IngameMenuScript IMS;

    public void CountDownDone()
    {
        IMS = GameObject.Find("IngameMenuScript").GetComponent<IngameMenuScript>();
        IMS.CountDown();
    }
}
