using UnityEngine;
using System.Collections;

public class SetCountdown : MonoBehaviour
{    
    private CountdownManagerScript CDMS;    

    public void SetCountdownNow()
    {
        CDMS = GameObject.Find("CountdownManager").GetComponent<CountdownManagerScript>();
        CDMS.CountDownDone();
    }

}
