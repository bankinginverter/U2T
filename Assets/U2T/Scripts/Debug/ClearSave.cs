using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSave : MonoBehaviour
{
    Save save;

    void Start()
    {
        save = new Save();
        save.SaveCheckIn1("");
        save.SaveCheckIn2("");
        save.SaveCheckIn3("");
        save.SaveCheckIn4("");
        save.SaveCheckInSuccess("");
    }
}
