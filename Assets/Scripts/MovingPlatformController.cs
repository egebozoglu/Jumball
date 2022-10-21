using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    bool left = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (left)
        {
            transform.Translate(-Time.deltaTime * 1.5f, 0f, 0f);
            if (transform.position.x<=-2.4f)
            {
                left = false;
            }
        }
        else
        {
            transform.Translate(Time.deltaTime * 1.5f, 0f, 0f);
            if (transform.position.x>=2.4f)
            {
                left = true;
            }
        }
    }
}
