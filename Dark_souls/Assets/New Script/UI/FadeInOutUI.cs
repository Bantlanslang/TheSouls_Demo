using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutUI : MonoBehaviour
{
    public RawImage FadeInOut;
    public bool Black;
    public int fadeSpeed = 1;

    private void Awake() {

        FadeInOut = GetComponentInChildren<RawImage>();
        FadeInOut.color = Color.clear;
    }

    public void DoFadeInOut(bool isblack){

        if(!isblack){
            FadeInOut.color = Color.Lerp(FadeInOut.color,Color.clear, Time.deltaTime * fadeSpeed);
        }
        else if(isblack){
            FadeInOut.color = Color.Lerp(FadeInOut.color,Color.black,Time.deltaTime * fadeSpeed);
        }
    }

}
