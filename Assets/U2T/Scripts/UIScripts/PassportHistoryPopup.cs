using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassportHistoryPopup : MonoBehaviour
{
    public void ShowSign(string nameSign)
    {
        GameObject.Find(nameSign).GetComponent<Image>().enabled = true;
    }

    public void DisableSign(string nameSign)
    {
        GameObject.Find(nameSign).GetComponent<Image>().enabled = false;
    }
}
