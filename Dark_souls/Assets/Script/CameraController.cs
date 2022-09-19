using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject model;
    [SerializeField] private GameObject camHandle;
    [SerializeField] private float mouseXSpeed = 200.0f;
    [SerializeField] private float mouseYSpeed = 200.0f;
    [SerializeField] private GameObject actor;
    [SerializeField] private LockTarget lockTarget;
    [SerializeField] private Image lockpoint;
    
    [SerializeField] private ActorManager actorManager;
    private UIManager uiManager;
    
    public bool lockState;
    private float xRotation;

    void Awake(){

        camHandle = this.gameObject;
        model = this.transform.parent.gameObject;
    }

    void FixedUpdate() {

    }
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        lockpoint.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        lockState = false;

        actor = model.GetComponent<AnimatorController>().actor;
        actorManager = model.GetComponent<ActorManager>();
    }
    void MouseCamera()
    {
        if(!actorManager.IsAI){

            if(lockTarget == null){
            Vector3 tempModeEuler = actor.transform.eulerAngles;

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            model.transform.Rotate(Vector3.up, mouseX * mouseXSpeed * Time.deltaTime);

            xRotation -= mouseY * mouseYSpeed * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -40f, 30f);

            camHandle.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            actor.transform.eulerAngles = tempModeEuler;
        }
            else{
                Vector3 tempForward = lockTarget.obj.transform.position - actor.transform.position;
                tempForward.y = 0;
                model.transform.forward = tempForward;
                Vector3 lockTargetTransform = new Vector3
                (lockTarget.obj.transform.position.x,lockTarget.obj.transform.position.y * 0.25f,lockTarget.obj.transform.position.z);
                camHandle.transform.LookAt(lockTargetTransform);

            }
        }
    }

    void Update(){
        
        MouseCamera();

        if(lockTarget != null){
            if(!actorManager.IsAI){
                lockpoint.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0,lockTarget.halfHeight,0));
            }
            
            if(Vector3.Distance(actor.transform.position,lockTarget.obj.transform.position) >=10.0f){
                LockProcessA(null,false,false,actorManager.IsAI);
            }
        }

        if(uiManager.IsOPenSelectWindow){
            Cursor.lockState = CursorLockMode.None;
        }
        else{
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
    private void LockProcessA(LockTarget _lockTarget,bool _lockpointEnable,bool _lockState,bool _IsAI){
        lockTarget = _lockTarget;
        if(!_IsAI){
           lockpoint.enabled = _lockpointEnable;
        }
        lockState = _lockState;
    }

    //OverlapBox...
    public void LockUnLock(){
        if(lockTarget == null){
            Vector3 modelOrigin1 = actor.transform.position;
            Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0,1,0);
            Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
            Collider[] cols = Physics.OverlapBox(boxCenter,new Vector3(0.5f,0.5f,5f),actor.transform.rotation,LayerMask.GetMask("Enemy"));
                
            if(cols.Length == 0){
                LockProcessA(null,false,false,actorManager.IsAI);
            }
            else{
                foreach (var col in cols){
                    if(!actorManager.IsAI){
                        lockTarget = new LockTarget(col.gameObject, col.bounds.extents.y);
                    }
                    break;
                }
                if(!actorManager.IsAI){
                    lockpoint.enabled = true;
                    lockState = true; 
                }
            }

        }
        else{
            // lockTarget = null;
            // lockpoint.enabled = false;
            // lockState = false;
            LockProcessA(null,false,false,actorManager.IsAI);
        }
    }

    private class LockTarget{
        public GameObject obj;
        public float halfHeight;
        
        public LockTarget(GameObject _obj,float halfHeight){
            obj = _obj;
            //this.obj = obj;
            this.halfHeight = halfHeight;
        }
    }

}