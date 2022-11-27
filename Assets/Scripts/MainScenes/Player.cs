using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 移动
    public float mMoveSpeed;
    private Rigidbody rigidbody;
    #endregion
    #region 旋转
    public float mRotateSpped;
    #endregion
    #region 动画
    public Animator animator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (h == 0 && v == 0)
        {
            animator.SetBool("bMove", false);
            return;
        }
        animator.SetBool("bMove", true);


        Vector3 dir = new Vector3(-h, 0, -v);
        Quaternion targetQ = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetQ, mRotateSpped * Time.deltaTime);

        //transform.position =  Vector3.MoveTowards(transform.position, transform.position  + transform.forward * mMoveSpeed * Time.deltaTime, mMoveSpeed * Time.deltaTime);
        rigidbody.velocity = transform.forward * mMoveSpeed;

    }
}
