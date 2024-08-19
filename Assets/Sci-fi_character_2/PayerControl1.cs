using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayerControl1 : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        //click the key
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            //use the animator
            animator.SetBool("move",true);
        }
        else
        {
            animator.SetBool("move", false);
        }
        // E for touch
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("touch");
        }
    }
}
