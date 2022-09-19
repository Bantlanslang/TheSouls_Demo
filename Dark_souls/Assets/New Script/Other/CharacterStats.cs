using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("===== Health =====")]
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    [Header("===== Stamina =====")]
    public int StaminaLevel = 10;
    public int maxStamina;
    public float currentStamina;
    public bool isDeath;
}
