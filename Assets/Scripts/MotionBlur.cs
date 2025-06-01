using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MotionBlur : MonoBehaviour
{
    private static float throwForce = 4.5f;
    private static float chamjilForce = 2.5f;

    
    public static bool CheckThrow()
    {
        return Input.acceleration.magnitude > throwForce;
    }

    
    public static bool CheckChamjil()
    {
        return Input.acceleration.magnitude > chamjilForce;
    }
}
