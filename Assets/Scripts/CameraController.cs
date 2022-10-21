using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float speed = 0.2f;

    public float distanceToSpeedUp = 14f;

    public float deadYPosition = -10f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.instance.distance>=distanceToSpeedUp)
        {
            speed += 0.25f;
            distanceToSpeedUp += 14f;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (GameManager.instance.gameStarted && !GameManager.instance.failed)
        {
            if (PlayerController.instance.transform.position.y > transform.position.y + 4f)
            {
                transform.Translate(0f, Time.deltaTime * 12f, 0f);
            }
            if (UIManager.instance.distance >= 10f)
            {
                transform.Translate(0f, Time.deltaTime * speed, 0f);
                deadYPosition = transform.position.y - 7f;
            }
        }
        else
        {
            transform.position = new Vector3(0f, 1f, -5f);
        }
    }
}
