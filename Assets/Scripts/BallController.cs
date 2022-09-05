using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    Camera mainCamera;

    [Header("Ball Settings")]
    public float stopVelocity;
    public float shotPower;
    public float maxPower;

    bool isAiming;
    bool isIdle;
    bool isShooting;

    Vector3? worldPoint;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        rb.maxAngularVelocity = 1000;

        isAiming = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.magnitude < stopVelocity)
        {
            ProcessAim();

            if (Input.GetMouseButtonDown(0))
            {
                if (isIdle)
                {
                    isAiming = true;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isShooting = true;
            }
        }
    }

    // calculatre physics
    void FixedUpdate()
    {
        if (rb.velocity.magnitude < stopVelocity)
        {
            Stop();
        }

        if(isShooting)
        {
            Shoot(worldPoint.Value);
            isShooting=false;
        }
    }

    private void ProcessAim()
    {
        if (!isAiming && !isIdle)
        {
            return;
        }

        worldPoint = CastMouseClickRay();

        if (worldPoint.HasValue == false)
        {
            return;
        }

    }

    private Vector3? CastMouseClickRay()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        else
        {
            return null;
        }
    }

    private void Stop()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        isIdle = true;
    }

    private void Shoot(Vector3 point)
    {
        isAiming = false;

        Vector3 HorizontalWorldPoint = new Vector3(point.x, transform.position.y, point.z);

        Vector3 direction = (HorizontalWorldPoint - transform.position).normalized;
        float strength = Vector3.Distance(transform.position, HorizontalWorldPoint);
        rb.AddForce(direction * strength * shotPower);
    }
}
