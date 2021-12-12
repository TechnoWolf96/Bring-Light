using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Side { Left, Right, Up, Down }



public class AnimatorAgent : MonoBehaviour
{
    [HideInInspector] public Animator anim;
    [HideInInspector] public Transform follow;

    protected void Start()
    {
        anim = GetComponent<Animator>();
    }


    public void SetAnimation(Side side, string nameAnimation)
    {
        switch(side)
        {
            case Side.Left:
                anim.SetTrigger("Left");
                anim.SetTrigger(nameAnimation);
                break;
            case Side.Right:
                anim.SetTrigger("Right");
                anim.SetTrigger(nameAnimation);
                break;
            case Side.Up:
                anim.SetTrigger("Up");
                anim.SetTrigger(nameAnimation);
                break;
            case Side.Down:
                anim.SetTrigger("Down");
                anim.SetTrigger(nameAnimation);
                break;
        }
    }





}
