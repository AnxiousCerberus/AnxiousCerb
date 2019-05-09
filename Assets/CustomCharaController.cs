using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharaController : MonoBehaviour
{

    Animator animator;
    public SpriteRenderer renderer;

    //Directions
    float xDirection = 0;
    [SerializeField] float speed = 1f;

    //Physic
    public float accelX;
    public float decelX;
    public float gravity;
    public float groundVelY;

    private Vector3 velocity;
    private Character character;

    public bool flipped = false;

    // Start is called before the first frame update
    void Start()
    {
        character = this.GetComponent<Character>();
        animator = this.GetComponent<Animator>();
        renderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("H Axis Input = " + Input.GetAxis("Horizontal"));

        xDirection = Input.GetAxisRaw("Horizontal");
        Debug.Log("xDirection = " + xDirection);
        //Move!!
        if (Mathf.Abs(xDirection) > 0f)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * xDirection, Time.deltaTime * accelX);

            //Walking animator
            animator.SetBool("isWalking", true);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0f, Time.deltaTime * decelX);

            animator.SetBool("isWalking", false);
        }

        //Just well... Applying gravity, y'know.
        velocity.y += gravity * .5f * Time.deltaTime;

        character.MoveX(velocity.x * Time.deltaTime, Character.Slide.Perpendicular);
        Debug.Log("Velocity.x = " + velocity.x);

        if (!character.MoveY(velocity.y * Time.deltaTime))
            velocity.y = groundVelY;
        else
            velocity.y += gravity * .5f * Time.deltaTime;

        //Flip sprite
        if (xDirection < -.01f)
            renderer.flipX = true;
        else if (xDirection > .01f)
            renderer.flipX = false;

    }
}
