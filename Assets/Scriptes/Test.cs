using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private AnimationCurve curveX;
    [SerializeField] private AnimationCurve curveY;

    [SerializeField] private float timer;
    [SerializeField] private bool isMoving;
    [SerializeField] private float lengthOfMovement = 1;


    [SerializeField] private AnimationCurve xyCurve = new AnimationCurve();
    

    [SerializeField] private float power;

    [SerializeField] private Vector2 forwardVector2d = new Vector2(0, 1);
    [SerializeField] private Vector2 fingerDirection = new Vector2(0, 1);

    private LineRenderer lineRenderer;
    private Rigidbody rigidbody;


    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        lineRenderer = this.GetComponent<LineRenderer>();
        StartCoroutine(runRpeatedly());
    }

    private Vector2 rotate(Vector2 inputVector2)
    {
        // float alpha = 90f;
        float alpha = Vector2.SignedAngle(forwardVector2d, fingerDirection);
        if (alpha < 0)
            alpha += 360;

        // Define the rotation axis as the Y-axis
        Vector3 axis = Vector3.up;

        // Create a quaternion representing the desired rotation
        Quaternion rotation = Quaternion.AngleAxis(alpha, axis);

        // Rotate the vector using the quaternion
        Vector3 rotatedV = rotation * inputVector2;

        // Discard the y-component to obtain a 2D vector
        Vector2 rotated2DV = new Vector2(rotatedV.x, rotatedV.z);
        return rotated2DV;
    }

    public void calculateCurve()
    {
        while (xyCurve.length > 0)
        {
            xyCurve.RemoveKey(0);
        }

        float duration = 1;
        float timeStep = 0.005f;

        for (float t = 0; t < duration; t += timeStep)
        {
            float xValue = curveX.Evaluate(t); // x
            float yValue = curveY.Evaluate(t); // y

            float inTan = 0f;
            float outTan = 0f;
            Keyframe keyframe;

            if (xyCurve.length > 0)
            {
                int prev_id = xyCurve.length - 1;
                Keyframe lastKeyFrame = xyCurve.keys[prev_id];
                inTan = (yValue - lastKeyFrame.value) / (xValue - lastKeyFrame.time);
                // keyframe.inTangent = -inTan;

                keyframe = new Keyframe(xValue, yValue, inTan, 0f);

                lastKeyFrame =
                    new Keyframe(lastKeyFrame.time, lastKeyFrame.value, lastKeyFrame.inTangent,
                        inTan); // outTan = - inTan

                xyCurve.MoveKey(prev_id, lastKeyFrame);
            }
            else
            {
                keyframe = new Keyframe(xValue, yValue, 0f, 0f);
            }


            xyCurve.AddKey(keyframe);
            
        }
    }


    public void startMove(Vector2 fingerDirection)
    {
        Debug.Log("started");
        isMoving = true;
        timer = lengthOfMovement;
        this.fingerDirection = fingerDirection;
    }

    public void startMove()
    {
        Debug.Log("started");
        isMoving = true;
        timer = lengthOfMovement;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (timer > 0)
            {
                float t = lengthOfMovement - timer;
                float Xspeed = (curveX.Evaluate(t + Time.fixedDeltaTime) - curveX.Evaluate(t)) * power /
                               Time.fixedDeltaTime;
                float Zspeed = (curveY.Evaluate(t + Time.fixedDeltaTime) - curveY.Evaluate(t)) * power /
                               Time.fixedDeltaTime;
                Vector2 movement = new Vector2(Xspeed, Zspeed);
                movement = rotate(movement);

                Vector3 speed = new Vector3(movement.x, rigidbody.velocity.y, movement.y);

                rigidbody.velocity = speed;
                timer -= Time.fixedDeltaTime;
            }
        }
        else
        {
            isMoving = false;
        }
    }

    public void turnOffLineRenderr()
    {
        lineRenderer.enabled = false;
    }

    public void drawLineRenderer()
    {
        lineRenderer.enabled = true;
        calculateCurve();

        // Create a new Vector3 array with the same length as the AnimationCurve
        Vector3[] positions = new Vector3[xyCurve.length];

        // Iterate through the keys of the AnimationCurve and set the x-coordinate of each position to the time value
        for (int i = 0; i < xyCurve.length; i++)
        {
            Vector2 movement = new Vector2(xyCurve.keys[i].time, xyCurve.keys[i].value);
            movement = rotate(movement);
            positions[i] = new Vector3(movement.x, 0, movement.y) * power + transform.position;
        }

        // Set the positions of the Line Renderer
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    private IEnumerator runRpeatedly()
    {
        while (true)
        {
            startMove();
            drawLineRenderer();
            yield return new WaitForSeconds(3);
        }
    }
}