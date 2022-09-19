using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerInput playerinput;
    public PlayerState playerState;
    AnimatorController animatorController;
    
    PlayerInventory playerInventory;
    UIManager uIManager;
    public InteractableUI interactableUI;
    public Animator animator;

    [Header("===== Animation cost Stamina =====")]
    public float RollStamina = 15.0f;
    public float RunStamina = 1.0f;
    public float JumpStamina = 15.0f;
    
    [Header("===== Player flags =====")]
    public bool isInteracting;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;
    public bool isInvulnerable;
    [Header("===== Game flags =====")]
    public bool isDiedPlayAudio;

    private void Start() {
        playerinput = GetComponent<PlayerInput>();
        playerState = GetComponent<PlayerState>();
        animatorController = GetComponent<AnimatorController>();    
        playerInventory = GetComponent<PlayerInventory>();
        interactableUI = FindObjectOfType<InteractableUI>();
        uIManager = FindObjectOfType<UIManager>();
        animator = GetComponentInChildren<Animator>();
    }


    private void OnTriggerStay(Collider col) {
        if(col.CompareTag("Interactable")){
            Interactable interactable = col.GetComponent<Interactable>();
            if(interactable != null){
                //interactable UI
                interactableUI.Interactabletext.text = interactable.InteractableText; 
                interactableUI.SetActiveInteractable(true);

                playerinput.InputInteractable();
                if(playerinput.Interactable){
                    interactableUI.SetActiveInteractable(false);
                    if(interactableUI.ActiveItemUI == false){
                        interactableUI.SetActiveItemInteractable(true);  
                        uIManager.UpdateUI();
                    }
                    col.GetComponent<Interactable>().Interact(this);
                }
            }
        }
    }

    private void Update() {
        
        if(interactableUI.ActiveItemUI == true){
            playerinput.InputInteractable();
            if(playerinput.Interactable){
                interactableUI.SetActiveItemInteractable(false);
                animatorController.CloseInteractable = false;
            }
        }

        isUsingLeftHand = animator.GetBool("isUsingLeftHand");
        isUsingRightHand = animator.GetBool("isUsingRightHand");
        isInvulnerable = animator.GetBool("isInvulnerable");
        isInteracting = animator.GetBool("isInteracting");
        isDiedPlayAudio = animator.GetBool("isDiedPlayAudio");

        playerState.RecoverStamina();
        
    }

    private void OnTriggerExit(Collider col) {
        if(col.CompareTag("Interactable")){
            interactableUI.SetActiveInteractable(false);
        }
    }
}
