using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulsCountBar : MonoBehaviour
{
    public Text soulsCount;

    public void SetSoulsCount(int souls){
        soulsCount.text = souls.ToString();
    }
}
