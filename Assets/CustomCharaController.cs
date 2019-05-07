using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharaController : MonoBehaviour
{

    Animator animator;
    public SpriteRenderer renderer;

    //Directions
    float xDirection = 0;
    float yDirection = 0;
    [SerializeField] float speed = 1f;

    public bool flipped = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        renderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("H Axis Input = " + Input.GetAxis("Horizontal"));

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            //Walking animator
            animator.SetBool("isWalking", true);

            //GetDirection
            xDirection = Input.GetAxisRaw("Horizontal") * speed;
        }
        else
        {
            animator.SetBool("isWalking", false);
            xDirection = 0;
        }

        //Move!!
        Vector3 direction = new Vector3(xDirection, yDirection, 0);
        transform.position += direction * speed * Time.deltaTime;

        //Flip sprite
        if (direction.x < -.01f)
            renderer.flipX = true;
        else if (direction.x > .01f)
            renderer.flipX = false;

    }
}
