using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject actor;
    public CameraController camcon;
    public Animator anim;
    [SerializeField] private IUserInput pi;
    [SerializeField] private Rigidbody rigid;
    private CapsuleCollider col;

    private PlayerState playerState;
    private PlayerManager playerManager;
    private WeaponSlotManager weaponSlotManager;

    [Header("===== Friction Settings =====")]
    [SerializeField] private PhysicMaterial frictionOne;
    [SerializeField] private PhysicMaterial frictionZero;

    [Header("===== Value =====")]
    [SerializeField] private float jumpVelocity = 2.5f;
    [SerializeField] private float walkSpeed = 2.0f;
    private float runSpeed = 2.0f;
    private Vector3 Move;
    private bool LockPlanar = false;    //鎖死平面移動
    private bool trackDirection = false;
    public bool CloseInteractable = false;

    private Vector3 trustVec;
    private bool canAttack;
    //private float lerpTarget;
    private Vector3 deltaPos;


    //TODO Left and Right Weapon Change...
    //private bool leftIsShield = true;


    private void Awake()
    {
        pi = GetComponent<IUserInput>();
        anim = actor.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        //camcon = GetComponentInChildren<CameraController>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
    }
    public void Start()
    {

    }


    void Update()
    {
        if (pi.lockon)
        {
            camcon.LockUnLock();
        }

        if (camcon.lockState == false)
        {

            anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), ((pi.run) ? 2.0f : 1.0f), 0.3f));
            anim.SetFloat("right", 0);
        }
        else
        {
            Vector3 localDvec = transform.InverseTransformVector(pi.Dvec);
            anim.SetFloat("forward", localDvec.z * ((pi.run) ? 2.0f : 1.0f));
            anim.SetFloat("right", localDvec.x * ((pi.run) ? 2.0f : 1.0f));
        }

        //TODO Defense...

        if (camcon.lockState == false)
        {

            if (pi.Dmag > 0.1f)
            {
                //球型線性插值
                actor.transform.forward = Vector3.Slerp(actor.transform.forward, pi.Dvec, 0.5f);
            }
            if (LockPlanar == false)
            {
                //轉模型方向
                Move = pi.Dmag * actor.transform.forward * walkSpeed * ((pi.run) ? runSpeed : 1.0f);
            }
        }
        //鎖定時的動畫調整
        else
        {
            if (trackDirection == false)
            {
                actor.transform.forward = transform.forward;
            }
            else
            {
                if (Move.normalized == Vector3.zero)
                {
                    return;
                }
                actor.transform.forward = Move.normalized;
            }

            if (LockPlanar == false)
            {
                Move = pi.Dvec * walkSpeed * ((pi.run) ? runSpeed : 1.0f);
            }
        }

        if (pi.attack && (CheckState("ground") || CheckStateTag("attack")) && canAttack)
        {
            anim.SetTrigger("attack");
        }

        if (pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        if (pi.roll)
        {
            if (camcon.lockState == false)
            {
                if (anim.GetFloat("forward") <= 0.1)
                {
                    anim.SetTrigger("jab");
                }
                else
                {
                    anim.SetTrigger("roll");
                }
            }
            else
            {
                if (anim.GetFloat("right") <= 0.1 || anim.GetFloat("right") >= 0.1)
                {
                    anim.SetTrigger("roll");
                }
                else if (anim.GetFloat("right") == 0)
                {
                    anim.SetTrigger("jab");
                }
            }
            canAttack = false;
        }

        if (pi.Interactable && CloseInteractable == false)
        {
            anim.SetTrigger("Pickup");
            canAttack = false;
            CloseInteractable = true;
        }
    }
    void FixedUpdate()
    {
        // rigid.position += Move * Time.fixedDeltaTime;
        rigid.position += deltaPos;
        rigid.velocity = new Vector3(Move.x, rigid.velocity.y, Move.z) + trustVec;
        trustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }
    //查詢動畫目前狀態
    private bool CheckState(string stateName, string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);
        return result;
    }
    private bool CheckStateTag(string TagName, string layerName = "Base Layer")
    {
        int layerIndex = anim.GetLayerIndex(layerName);
        bool result = anim.GetCurrentAnimatorStateInfo(layerIndex).IsTag(TagName);
        return result;
    }

    private void LightAttack(WeaponItem weaponItem)
    {
        weaponSlotManager.weaponItem = weaponItem;
    }


    //OnAnimation Message...    
    private void OnjumpEnter()
    {
        trustVec = new Vector3(0, jumpVelocity, 0);
        pi.inputEnabled = false;
        LockPlanar = true;
        trackDirection = true;
    }
    public void isGround()
    {
        anim.SetBool("isGround", true);
    }
    public void notGround()
    {
        anim.SetBool("isGround", false);
    }

    private void OnGroundEnter()
    {
        pi.inputEnabled = true;
        LockPlanar = false;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
    }
    private void OnGroundExit()
    { 
        col.material = frictionZero;
    }
    private void OnGroundEnterResetBool()
    {
        anim.SetBool("isInteracting", false);
        anim.SetBool("isRotate",false);
        anim.SetBool("isInvulnerable",false);
        anim.SetBool("EnemyInteracting",false);
    }
    //Use props clear trigger
    private void OnUsepropsEnter()
    {
        pi.inputEnabled = false;
    }

    private void OnFallEnter()
    {
        pi.inputEnabled = false;
        LockPlanar = true;
    }
    private void OnRollEnter()
    {
        //trustVec = new Vector3(0, rollVelocity, 0);
        pi.inputEnabled = false;
        LockPlanar = true;
        trackDirection = true;
        
    }
    private void OnRollUpdate()
    {
        trustVec = actor.transform.forward * anim.GetFloat("rollVelocity");
    }

    private void OnjabEnter()
    {
        pi.inputEnabled = false;
        LockPlanar = true;
    }
    private void OnjabUpdate()
    {
        trustVec = actor.transform.forward * anim.GetFloat("jabVelocity");
    }
    private void OnAttack1hAEnter()
    {
        pi.inputEnabled = false;
        //lerpTarget = 1.0f;
    }
    private void OnAttack1hAUpdate()
    {
        trustVec = actor.transform.forward * anim.GetFloat("attack1hAVelocity");

        //Lerp Layer...
        //float CurrentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        //CurrentWeight = Mathf.Lerp(CurrentWeight,lerpTarget,0.3f);
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), CurrentWeight);
    }
    // private void OnUpdateRm(object _deltaPos){

    //     if(CheckState("attack1hC")){
    //         deltaPos += (Vector3)_deltaPos;
    //     }
    // }
    private void OnInteractable()
    {
        pi.inputEnabled = false;

    }
    private void OnHitEnter()
    {
        pi.inputEnabled = false;
        Move = Vector3.zero;
    }
    private void OnDeathEnter()
    {
        pi.inputEnabled = false;
        trackDirection = true;
    }

    public void IssueTrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }
    public void IssueDeathBool(string Ded)
    {
        anim.SetTrigger(Ded);
    }

}
