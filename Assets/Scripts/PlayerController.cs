using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    

    public float power = 7f;
    public float maxDrag = 5f;
    public Rigidbody2D rb;
    public LineRenderer lr;

    Vector3 dragStartPos;
    Touch touch;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
            }
            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            if (touch.phase == TouchPhase.Ended)
            {
                DragRealease();
            }
        }

        if (transform.position.y < CameraController.instance.deadYPosition)
        {
            GameManager.instance.failed = true;
            GameManager.instance.InstantiateSound(0, Vector3.zero);
            Destroy(this.gameObject, 0f);
        }
    }
    private void DragStart()
    {
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);
    }
    private void Dragging()
    {
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
    }
    private void DragRealease()
    {
        lr.positionCount = 0;

        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;

        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
        rb.AddForce(clampedForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "arrow")
        {
            rb.AddForce(collision.gameObject.transform.right * power * 3f, ForceMode2D.Impulse);
            GameManager.instance.InstantiateSound(1, transform.position);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform" || collision.gameObject.tag == "arrow")
        {
            GameManager.instance.InstantiateSound(1, transform.position);
        }
    }
}
