using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LocationLogger : MonoBehaviour
{
    public Text latitudeText;
    public Text longitudeText;
    public Text altitudeText;

    void Start()
    {
        // this start's the location service
        StartCoroutine(StartLocationService());
    }

    IEnumerator StartLocationService()
    {
        // this check's if the user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("Location services not enabled by user.");
            yield break;
        }

        // this start's the location service
        Input.location.Start();

        // this wait's until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // if the service didn't initialize in 20 seconds, fail
        if (maxWait <= 0)
        {
            Debug.Log("Timed out.");
            yield break;
        }

        // if the connection failed, log an error
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location.");
            yield break;
        }
        else
        {
            InvokeRepeating("UpdateLocation", 0f, 1f);
        }
    }

    void UpdateLocation()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            // this retrieve's and display's the device's location coordinates
            LocationInfo location = Input.location.lastData;
            latitudeText.text = "Latitude: " + location.latitude;
            longitudeText.text = "Longitude: " + location.longitude;
            altitudeText.text = "Altitude: " + location.altitude;
        }
        else
        {
            Debug.Log("Location service is not running.");
        }
    }
}