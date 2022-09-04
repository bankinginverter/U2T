using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInManager : MonoBehaviour
{
    public static CheckInManager Instance;
    public static int CountCheckIn = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void Counting()
    {
        CountCheckIn++;
    }

    public int GetCounting()
    {
        return CountCheckIn;
    } 
}
