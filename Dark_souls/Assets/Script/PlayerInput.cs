using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IUserInput
{
    [Header("======  keyCode  ======")]
    [SerializeField] private string up = "w";
    [SerializeField] private string down = "s";
    [SerializeField] private string left = "a";
    [SerializeField] private string right = "d";
    public string keyroll = "space";
    public string keyJump = "left ctrl";
    public string keyRun = "left shift";
    public string keyAttack = "mouse 0";
    public string keyrightMouse = "mouse 1";
    public string keyLockon = "r";
    public string keyChangeRightWeapon = "x";
    public string keyChangeLeftWeapon = "z";
    public string keyInteractable = "f";
    public string keyOpenSelectWindow = "escape";
    public string keyUseprops = "e";


    public MyButton ButtonRun = new MyButton();
    public MyButton ButtonJump = new MyButton();
    public MyButton ButtonAttack = new MyButton();
    public MyButton ButtonRoll = new MyButton();
    public MyButton ButtonLockOn = new MyButton();
    public MyButton ButtonPickup = new MyButton();
    public MyButton ButtonRightMouse = new MyButton();

    private PlayerInventory playerInventory;
    private PlayerState playerState;
    private PlayerEffectManager playerEffectManager;
    private PlayerManager playerManager;
    private UIManager uiManager;
    private WeaponSlotManager weaponSlotManager;
    //private Animator animator;

    //[Header("======  value  ======")]
    // [SerializeField] private float Dup;
    // [SerializeField] private float Dright;
    // private float targetDup;
    // private float targetDright;
    // private float VelocityDup;
    // private float VelocityDright;
    // public Vector3 Dvec;
    // public float Dmag;

    // //bool
    // public  bool inputEnabled = true;
    // public bool roll;
    // public bool run;
    // public bool jump;
    // private bool lastjump;
    // public bool attack;
    // private bool lastAttack;

    void Awake(){
        playerInventory = GetComponent<PlayerInventory>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        uiManager = FindObjectOfType<UIManager>();
        playerState = GetComponent<PlayerState>();
        playerManager = GetComponent<PlayerManager>();
        playerEffectManager = GetComponentInChildren<PlayerEffectManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ButtonRun.Tick(Input.GetKey(keyRun));
        ButtonJump.Tick(Input.GetKey(keyJump));
        ButtonAttack.Tick(Input.GetKey(keyAttack));
        ButtonRoll.Tick(Input.GetKey(keyroll));
        ButtonLockOn.Tick(Input.GetKey(keyLockon));

        InputQuickSlot();
        InputSelectWindow();
        InputRun();
        Inputjump();

        Useprops();

        targetDup = (Input.GetKey(up) ? 1.0f : 0) - (Input.GetKey(down) ? 1.0f : 0);
        targetDright = (Input.GetKey(right) ? 1.0f : 0) - (Input.GetKey(left) ? 1.0f : 0);

        if(inputEnabled == false){
            targetDup = 0;
            targetDright = 0;
        }
        
        Dup = Mathf.SmoothDamp(Dup,targetDup,ref VelocityDup,0.1f);
        Dright = Mathf.SmoothDamp(Dright,targetDright,ref VelocityDright,0.1f);

        Vector2 tempAxis = SquareToCircle(new Vector2(Dright,Dup));

        float Dright2 = tempAxis.x;
        float Dup2 = tempAxis.y;

        UpdateDmagDvec(Dup2,Dright2);
        // Dmag = Mathf.Sqrt((Dup2 * Dup2)+(Dright2 * Dright2));
        // Dvec = Dup2 * transform.forward + Dright2 * transform.right;

        //boolean...
        lockon = ButtonLockOn.OnPressed;
        Interactable = ButtonPickup.OnPressed;
        if(!uiManager.IsOPenSelectWindow){
            InputLightAttack();
        }
        if(!uiManager.IsOPenSelectWindow){
            InputRoll();
        }
    }
    //Weapon quick Slot
    //武器快捷
    private void InputQuickSlot(){

        if(Input.GetKeyDown(keyChangeRightWeapon)){
            playerInventory.ChangeRightWeapon();
        }
        else if(Input.GetKeyDown(keyChangeLeftWeapon)){
            playerInventory.ChangeLeftWeapon();
        }
    }
    //Roll click
    private void InputRoll(){
        if(roll = ButtonRoll.OnPressed){
            if(playerState.currentStamina > 0 && inputEnabled == true){
                AudioManager.instance.Play("RollAudio");
                playerManager.animator.SetBool("isInteracting",true);
                playerState.TakeStamina(playerManager.RollStamina);
            }
            else{
                //roll = false;
            }
        }
    }
    private void InputRun(){

        if(run = ButtonRun.IsPressing && playerManager.animator.GetFloat("forward") >= 0.9f){
            if(playerState.currentStamina > 0){
                playerState.TakeStamina(playerManager.RunStamina);
                playerManager.animator.SetBool("isInteracting",true);
            }
            else{
                run = false;
                playerManager.animator.SetBool("isInteracting",false);
            }
        }
        else{
            run = false;
            playerManager.animator.SetBool("isInteracting",false);
        }
    }
    private void Inputjump(){
        if(jump = ButtonJump.OnPressed && inputEnabled == true){
            if(playerState.currentStamina > 0){
                playerState.TakeStamina(playerManager.JumpStamina);
                playerManager.animator.SetBool("isInteracting",true);
            }
            else{
                jump = false;
            }
        }
    }
    //Mouse0 click
    private void InputLightAttack(){
        if(attack = ButtonAttack.OnPressed){
            if(playerState.currentStamina > 0){
                HandleLightAttack(playerInventory.rightWeapon);
                playerManager.animator.SetBool("isUsingLeftHand",true);
                playerManager.animator.SetBool("isInteracting",true);
            }
            else{
                attack = false;
            }
        }
    }
    private void HandleLightAttack(WeaponItem weapon){
        weaponSlotManager.weaponItem = weapon;
    }
    public void Useprops(){
        if(Input.GetKeyDown(keyUseprops)){
            if(playerInventory.currentConsumable.currentItemAmount > 0 && inputEnabled == true){
                playerManager.animator.SetTrigger("Drink");
                playerInventory.currentConsumable.AttempTOConsumTime(this,weaponSlotManager,playerEffectManager);
            }
        }
    }

    public void InputInteractable(){
        ButtonPickup.Tick(Input.GetKey(keyInteractable));
        uiManager.UpdateUI();
    }
    public void InputSelectWindow(){
        if(Input.GetKeyDown(keyOpenSelectWindow)){
            uiManager.OpenSelectWindow();
        }
    }



    // resolve 1.414 probram
    // private Vector2 SquareToCircle(Vector2 input){

    //     Vector2 output = Vector2.zero;
    //     output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y)/2.0f);
    //     output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x)/2.0f);

    //     return output;
    // }
}
