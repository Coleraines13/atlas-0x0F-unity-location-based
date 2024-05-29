using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class GPSLocation : MonoBehaviour
{
    public Text GPSStatus;
    public Text LatitudeValue;
    public Text LongitudeValue;
    public Text AltitudeValue;
    public Text HorizontalAccuracyValue;
    public Text timestampValue;

   void Start()
   {
        StartCoroutine(GPSLoc());
   }

   IEnumerator GPSLoc()
   {
       if (!Input.location.isEnabledByUser)
        yield break;

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing  && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            GPSStatus.text = "Time out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "Unable to Determine Device location";
            yield break;
        }
        else
        {
            GPSStatus.text = "Running";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
            
        }

    }


private void UpdateGPSData()
{
    if (Input.location.status == LocationServiceStatus.Running)
    {
        GPSStatus.text = "Running";
        LatitudeValue.text = Input.location.lastData.latitude.ToString();
        LongitudeValue.text = Input.location.lastData.longitude.ToString();
        AltitudeValue.text = Input.location.lastData.altitude.ToString();
        HorizontalAccuracyValue.text = Input.location.lastData.horizontalAccuracy.ToString();
        timestampValue.text = Input.location.lastData.timestamp.ToString();

    }
    else
    {
        //stopped
        GPSStatus.text = "Stop";
    }
}
}
