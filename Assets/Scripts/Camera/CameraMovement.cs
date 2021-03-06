﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform target;
    public float targetHeight = 1.7f;
    public float distance = 12.0f;
    public float offsetWall = 0.1f;
    public float maxDistance = 20;
    public float minDistance = 0.6f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public float yMinLimit = -60f;
    public float yMaxLimit = 80f;
    public float zoomRate = 40;
    public float rotationDamping = 3.0f;
    public float zoomDamping = 5.0f;
    public LayerMask collisionLayers = -1;
    public bool lockToRearOfTarget = false;
    public bool allowMouseInputX = true;
    public bool allowMouseInputY = true;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private float correctedDistance;
    private bool rotateBehind = false;





    // Use this for initialization
    void Start () {
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 angles = transform.eulerAngles;
        xDeg = angles.x;
        yDeg = angles.y;
        currentDistance = distance;
        desiredDistance = distance;
        correctedDistance = distance;

        if (rb) rb.freezeRotation = true;

        if (lockToRearOfTarget) rotateBehind = true;


    }
	
	// Only move camera after everything else has been upated
	void LateUpdate () {
        if (!target) return;

        Vector3 vTargetOffset;

        if (Input.GetMouseButton(1))
        {
            Cursor.visible = false;

            //Check to see if mouse input is allowed on the axis
            if (allowMouseInputX)
                xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            else
                RotateBehindTarget();
            if (allowMouseInputY)
                yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            //Interrupt rotating behind if mouse wants to control rotation
            if (!lockToRearOfTarget)
                rotateBehind = false;
        }

        // otherwise, ease behind the target if any of the directional keys are pressed
        else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 || rotateBehind)
        {
            RotateBehindTarget();
        }

        Cursor.visible = true;
        yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(yDeg, xDeg, 0);

        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        correctedDistance = desiredDistance;

        vTargetOffset = new Vector3(0, -targetHeight, 0);
        Vector3 position = target.position - (rotation * Vector3.forward * desiredDistance + vTargetOffset);

        RaycastHit collisionHit;
        Vector3 trueTargetPosition = new Vector3(target.position.x, target.position.y + targetHeight, target.position.z);

        bool isCorrected = false;
        if (Physics.Linecast(trueTargetPosition, position, out collisionHit, collisionLayers))
        {
            // Calculate the distance from the original estimated position to the collision location,
            // subtracting out a safety "offset" distance from the object we hit.  The offset will help
            // keep the camera from being right on top of the surface we hit, which usually shows up as
            // the surface geometry getting partially clipped by the camera's front clipping plane.
            correctedDistance = Vector3.Distance(trueTargetPosition, collisionHit.point) - offsetWall;
            isCorrected = true;
        }

        if(!isCorrected || correctedDistance > currentDistance)
        {
            currentDistance = Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * zoomDamping);
        }

        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
        position = target.position - (rotation * Vector3.forward * currentDistance + vTargetOffset);

        transform.rotation = rotation;
        transform.position = position;
    }

    void RotateBehindTarget()
    {
        float targetRotationAngle = target.eulerAngles.y;
        float currentRotationAngle = transform.eulerAngles.y;

        xDeg = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDamping * Time.deltaTime);

        // Stop rotating behind if not completed
        if (targetRotationAngle == currentRotationAngle)
        {
            if (!lockToRearOfTarget)
                rotateBehind = false;
        }
        else
            rotateBehind = true;

    }

    static  float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}

