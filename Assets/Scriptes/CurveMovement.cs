using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CurveMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve curveX;
    [SerializeField] private AnimationCurve curveY;
    [SerializeField] private AnimationCurve xyCurve = new AnimationCurve();
    
    [SerializeField] private float timer;
    [SerializeField] private bool isMoving; // is moving by controll of shoot
    [SerializeField] private float lengthOfMovement = 1;


    

    [SerializeField] private Vector2 forwardVector2d = new Vector2(0, 1);
    [SerializeField] private Vector2 fingerDirection = new Vector2(0, 1);

    private LineRenderer lineRenderer;
    private Rigidbody rigidbody;
    private float power = 10;
    
    //consts
    private const float TIME_STEP = 0.05f;
    
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        calculateCurve();
        // StartCoroutine(runRpeatedly());
    }

    //wrong code 
    private Vector2 rotate(Vector2 inputVector2)
    {
        //way 2 
        float angle = Vector2.SignedAngle(forwardVector2d, fingerDirection);
        Debug.Log("angle : " + angle );
        float x = math.cos(angle) * inputVector2.x - math.sin(angle) * inputVector2.y;
        float y = math.sin(angle) * inputVector2.x + math.cos(angle) * inputVector2.y;
        
        // Debug.Log("calc :" +x + " , " + y);
        return new Vector2(x,y);
    }
    
    public  Vector2 rotateVector2D(Vector2 vector)
    {
        float angleDegrees = Vector2.SignedAngle(forwardVector2d, fingerDirection);
        
        // Convert the angle from degrees to radians
        float angleRadians = angleDegrees * Mathf.Deg2Rad;

        // Calculate the sin and cos of the angle
        float sin = Mathf.Sin(angleRadians);
        float cos = Mathf.Cos(angleRadians);

        // Apply the rotation matrix to the vector
        float x = vector.x * cos - vector.y * sin;
        float y = vector.x * sin + vector.y * cos;

        // Create a new vector with the rotated values
        Vector2 rotatedVector = new Vector2(x, y);

        return rotatedVector;
    }

    public void calculateCurve()
    {
        while (xyCurve.length > 0)
        {
            xyCurve.RemoveKey(0);
        }

        

        for (float t = 0; t < lengthOfMovement; t += TIME_STEP)
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


    public void startMove(Vector2 fingerDirection , float power)
    {
        Debug.Log("started");
        isMoving = true;
        timer = lengthOfMovement;
        this.fingerDirection = fingerDirection;
        this.power = power;
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
                // movement = rotate(movement);
                movement = rotateVector2D(movement);

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

    public void drawLineRenderer(Vector2 fingerDirection ,float power)
    {
        this.fingerDirection = fingerDirection;
        this.power = power;

        lineRenderer.enabled = true;

        // Create a new Vector3 array with the same length as the AnimationCurve
        Vector3[] positions = new Vector3[xyCurve.length];

        // Iterate through the keys of the AnimationCurve and set the x-coordinate of each position to the time value
        for (int i = 0; i < xyCurve.length; i++)
        {
            Vector2 movement = new Vector2(xyCurve.keys[i].time, xyCurve.keys[i].value);
            Debug.Log("finger point : " + this.fingerDirection);
            Debug.Log("before : " +movement);
            // Vector2 rotatedMovement = rotate(movement);
            Vector2 rotatedMovement = rotateVector2D(movement);
            Debug.Log("after : " +movement);
            positions[i] = new Vector3(rotatedMovement.x, 0f, rotatedMovement.y) * this.power + transform.position;
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
            drawLineRenderer(fingerDirection,power);
            yield return new WaitForSeconds(3);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 10)  // 10 is pawn
        {
            isMoving = false;
        }
    }
}
