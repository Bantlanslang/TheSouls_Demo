using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public Button startButton;
    public Button continueButton;
    public Button outButton;
    void Start(){
        
        startButton.onClick.AddListener(delegate (){
            this.convertLoadingScene();
        });
    }

    public void convertLoadingScene(){
        SceneManager.LoadScene("Loading");
    }

}
