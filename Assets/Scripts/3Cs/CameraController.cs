using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject currentTarget;
    [SerializeField] bool VerticalConstraint = true;
    [SerializeField] float xOffset = .5f;
    [SerializeField] float[] XLimits = new float[2];

    //Directions
    float xDirection = 0;
    float yDirection = 0;
    [SerializeField] float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaultTarget();
    }

    void SetDefaultTarget ()
    {
        currentTarget = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
    
        if (currentTarget != null)
        {
            //Set Offset corresponding to where the target is looking
            if (currentTarget.GetComponent<CustomCharaController>().renderer.flipX)
                //Get direction to target
                xDirection = (currentTarget.transform.position.x - xOffset) - transform.position.x;
            else
                xDirection = (currentTarget.transform.position.x + xOffset) - transform.position.x;



            if (!VerticalConstraint)
                yDirection = currentTarget.transform.position.y - transform.position.y;
            else
                yDirection = 0;

            Vector3 direction = new Vector3(xDirection, yDirection, 0);

            //Move toward target
            Vector3 targetPosition = transform.position + direction * Time.deltaTime * speed;
            if (targetPosition.x > XLimits[0])
                transform.position = targetPosition;
        }



    }
}
