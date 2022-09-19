using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public Slider slider;
    public float Finish = 100f;
    private void Awake() {
        slider = GetComponentInChildren<Slider>();
    }

    private void FixedUpdate() {
        LoadGameScene();
    }
    private void LoadGameScene(){

        float progress = Random.Range(0,5);

        if(slider.value < Finish){
            slider.value += progress;
        }
        else if(slider.value >= Finish){
            SceneManager.LoadScene("Scene1");
        }
    }

}
