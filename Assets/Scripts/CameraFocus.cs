using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{

    public Transform target;
    Vector3 offset = new Vector3(0,0,-5);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * 2);
    }
}
