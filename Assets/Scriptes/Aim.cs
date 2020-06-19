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
    [SerializeField] private Vector3 strechScale = new Vector3(1, 0.3F, 0.3f); // just x and z is important
    [SerializeField]private bool testing = false; // you must turn this off when you want to use it in game
    [SerializeField]private float maxPower =40;

    public GameObject flesh;

    public void setTargetTransform(Transform goal) // dan u must call this  :)
    {
        disableFlesh();
        transform.position = goal.position;
        // transform.parent = goal;
        //  transform.localPosition = Vector3.zero;
    }


    public void setup(Vector2 startPos,Vector2 endPos) 
    {
        DebugHandler.Instance.showTextMassage("-->" + startPos.ToString() +"  " + endPos.ToString());
        flesh.SetActive(true);
        //this.transform.position = target; // go sprite in to the target place you must set it before setup phase
        float directionAngle;
        directionVector = 
            (
                (endPos - startPos).normalized + 
             (new Vector2(Camera.main.transform.forward.x,Camera.main.transform.forward.z).normalized).normalized);
        float Stretched = Vector3.Distance(endPos ,startPos);
        this.active = true;
        directionAngle = Vector2.SignedAngle(directionVector,Vector2.right); //targe euler angle
        power = Mathf.Clamp(Stretched*10, 5, maxPower);
        this.gameObject.transform.localScale = minmumSize + power * strechScale; //setup new angle
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
    private void OnEnable()
    {
        GameManager.Instance.onChoosingCharacterAction += setTargetTransform;
        disableFlesh();
    }
}
