using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton
{
    public bool IsPressing = false;
    public bool OnPressed = false;
    public bool OnReleased = false;
    public bool IsExending = false;
    public bool IsDelaying = false;


    public float extendingDuration = 0.15f;
    public float delayingDuration = 0.15f;

    private bool CurState = false;
    private bool lastState = false;

    private MyTimer extTimer = new MyTimer();
    private MyTimer delayTimer = new MyTimer();
    
    public void Tick(bool input){
        
        extTimer.Tick();
        StartTimer(extTimer,1.0f);

        CurState = input;
        IsPressing = CurState;
        
        OnPressed = false;
        OnReleased = true;
        IsExending = false;
        IsDelaying = false;

        if(CurState != lastState){
            if(CurState == true){
                OnPressed = true;
                StartTimer(delayTimer,delayingDuration);
            }
            else{
                OnReleased = true;
                StartTimer(extTimer,extendingDuration);
            }
        }
        lastState = CurState;

        if(extTimer.state == MyTimer.STATE.RUN){
            IsExending = true;
        }
        if(delayTimer.state == MyTimer.STATE.RUN){
            IsDelaying = true;
        }
    }
    private void StartTimer(MyTimer timer,float duration){
        timer.duration = duration;
        timer.GO();
    }

}
