using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    // must setup direction in testing maybe angels dont works correctly
    private Vector2 front = new Vector2(1, 0);

    public Vector3 minmumSize;
    public Vector2 directionVector;
    public float power = 0;
    public bool active = false;
    public bool testing = false; // you must turn this off when you want to use it in game

    public GameObject flesh;

    
    public void setup(Vector2 startPos,Vector2 endPos) 
    {
        flesh.SetActive(true);
        float directionAngle;
        directionVector = directionVector = (endPos - startPos).normalized;
        float Stretched = Vector3.Distance(endPos ,startPos);//TODO @kash check
        this.active = true;
        directionAngle = Vector2.SignedAngle(Vector2.right, directionVector); //targe euler angle
       // power = Mathf.Clamp(power, 0, 1);TODO @kash check
        this.gameObject.transform.localScale = minmumSize + new Vector3(Stretched,0,Stretched/2f); //setup new angle
        this.gameObject.transform.eulerAngles = new Vector3(0, directionAngle, 0);//setup new scale
    }

    public void disableFlesh()
    {
        flesh.SetActive(false);
    }
#if UNITY_EDITOR
    private void Update()//just for testing never use
    {
        if (testing)
        {
            flesh.SetActive(true);
            float directionAngle;
            directionAngle = Vector2.SignedAngle(Vector2.right, directionVector);
            power = Mathf.Clamp(power, 0, 1);
            this.gameObject.transform.localScale = minmumSize + new Vector3(power, 0, power);
            this.gameObject.transform.eulerAngles = new Vector3(0, directionAngle, 0);
        }
    }
#endif
    private void Start() 
    {
        disableFlesh();// disable ui at the start
    }

}
