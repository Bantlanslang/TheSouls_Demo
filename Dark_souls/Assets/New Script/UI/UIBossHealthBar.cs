using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBossHealthBar : MonoBehaviour
{
    public Text BossName;
    Slider BossHealthSlider;

    private void Awake() {
        BossHealthSlider = GetComponentInChildren<Slider>();
        BossName = GetComponentInChildren<Text>();
    }
    private void Start(){
        BossHealthSlider.gameObject.SetActive(false);
    }
    public void SetBossName(string name){
        BossName.text = name;
    }

    //BossHealthBar with Boss Fight began
    public void SetUIHealthToActive(){
        BossHealthSlider.gameObject.SetActive(true);
    }
    public void SetBossMaxHealth(int maxhealth){
        BossHealthSlider.maxValue = maxhealth;
        BossHealthSlider.value = maxhealth;
    }
    public void SetBossCurrentHealth(int currentHealth){
        BossHealthSlider.value = currentHealth;
    }
}
