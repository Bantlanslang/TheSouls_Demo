using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogDoor : MonoBehaviour
{

    private void Awake() {
        gameObject.SetActive(false);    
    }
    public void ActiveFogWall(){
        gameObject.SetActive(true);
    }
    public void DeactivateFogWall(){
        gameObject.SetActive(false);
    }
}
