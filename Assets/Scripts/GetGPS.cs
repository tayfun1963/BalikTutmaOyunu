using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGPS : MonoBehaviour
{
    bool gpsInit = false;
    LocationInfo currentGPSPosition;
    int gps_connect = 0;
    double detailed_num =1.0;

    

    void Start()

    {
        Input.location.Start(0.5f);
        int wait = 1000;

        
        if (Input.location.isEnabledByUser)
        {
            while (Input.location.status == LocationServiceStatus.Initializing && wait > 0)
            {
                wait--; 
            }

            
            if (Input.location.status != LocationServiceStatus.Failed)
            {
                gpsInit = true;
                
                InvokeRepeating("RetrieveGPSData", 0.0001f, 1.0f);
            }
        }
        else
        {
            Debug.Log("GPS izin verilmedi");
        }

    }

    void RetrieveGPSData()

    {
        currentGPSPosition = Input.location.lastData;
        double latitude = currentGPSPosition.latitude * detailed_num;
        double longitude = currentGPSPosition.longitude *detailed_num;

        
        
        gps_connect++;
        double refreshCnt = gps_connect;
        

    }

}

