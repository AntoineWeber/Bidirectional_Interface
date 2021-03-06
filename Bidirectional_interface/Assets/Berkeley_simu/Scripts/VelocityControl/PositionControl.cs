﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VelocityControl))]
public class PositionControl : MonoBehaviour
{
    [Tooltip("Target position that the drone will try to reach.")]
    public Transform target;

    [Tooltip("If true, the drone will rotate to reach the targetYaw angle. Otherwise, the yaw angle is not affected.")]
    public bool controlYaw = false;

    [Tooltip("Target yaw angle in degrees.")]
    public float targetYaw;

    private VelocityControl vc;

    // PD controller gains
    private float positionTimeConstant = 1.0f;
    private float yawTimeConstant = 10.0f;
    private float dFactor = 0.1f;
    private float dFactorYaw = 0.01f;

    private Vector3 lastPositionError = Vector3.zero;
    private float lastYawError = 0.0f;

    // Use this for initialization
    void Start ()
    {
        vc = GetComponent<VelocityControl>();
    }

    void FixedUpdate ()
    {
        if (target != null)
        {
            Vector3 positionError = target.position - transform.position;

            // Since the speed control is done in the reference frame of the drone,
            // we need to convert the global error vector to drone-local coordinates
            positionError = transform.InverseTransformDirection(positionError);

            vc.desiredVx = positionError.x / positionTimeConstant + dFactor * (positionError.x - lastPositionError.x) / Time.fixedDeltaTime;
            vc.desiredVz = positionError.z / positionTimeConstant + dFactor * (positionError.z - lastPositionError.z) / Time.fixedDeltaTime;
            vc.desiredHeight = target.position.y;

            lastPositionError = positionError;
        }

        if (controlYaw)
        {
            float yawError = Mathf.DeltaAngle(transform.eulerAngles.y, targetYaw);

            // P controller on angular rate
            vc.desiredYawRate = yawError / yawTimeConstant + dFactorYaw * (yawError - lastYawError) / Time.fixedDeltaTime;

            lastYawError = yawError;
        }
    }
}
