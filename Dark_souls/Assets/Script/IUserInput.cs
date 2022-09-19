using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{   
    
    [SerializeField] protected float Dup;
    [SerializeField] protected float Dright;
    protected float targetDup;
    protected float targetDright;
    protected float VelocityDup;
    protected float VelocityDright;
    public Vector3 Dvec;
    public float Dmag;

    //bool
    public  bool inputEnabled = true;
    public bool roll;
    public bool run;
    public bool jump;
    public bool Interactable;
    protected bool lastjump;
    public bool attack;
    protected bool lastAttack;
    public bool lockon;


    //resolve 1.414 probram
    protected Vector2 SquareToCircle(Vector2 input){

        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y)/2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x)/2.0f);

        return output;
    }

    public void UpdateDmagDvec(float Dup,float Dright){
        Dmag = Mathf.Sqrt((Dup * Dup)+(Dright * Dright));
        Dvec = Dup * transform.forward + Dright * transform.right;
    }

}
