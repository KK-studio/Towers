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

    private Vector3 target;
    [SerializeField] private Vector3 strechScale = new Vector3(1, 0, 0.3f); // just x and z is important
    [SerializeField]private bool testing = false; // you must turn this off when you want to use it in game
    [SerializeField]private float maxPower =3;

    public GameObject flesh;

    public void setTargetTransform(Transform goal) // dan u must call this  :)
    {
        this.target = goal.position;
    }


    public void setup(Vector2 startPos,Vector2 endPos) 
    {
        flesh.SetActive(true);
        this.transform.position = target; // go sprite in to the target place you must set it before setup phase
        float directionAngle;
        directionVector = (endPos - startPos).normalized;
        float Stretched = Vector3.Distance(endPos ,startPos);
        this.active = true;
        directionAngle = Vector2.SignedAngle(Vector2.right, directionVector); //targe euler angle
        power = Mathf.Clamp(Stretched, 0, maxPower); 
        this.gameObject.transform.localScale = minmumSize + new Vector3(Stretched*strechScale.x,0,Stretched*strechScale.z); //setup new angle
        this.gameObject.transform.eulerAngles = new Vector3(0, directionAngle, 0);//setup new scale
    }

    public void disableFlesh()
    {
        flesh.SetActive(false);
    }

    public float currentPower()
    {
        return power;
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
        GameManager.Instance.onChoosingCharacterAction += setTargetTransform;
        disableFlesh();// disable ui at the start
    }

}
