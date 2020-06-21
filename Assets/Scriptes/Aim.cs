using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    // must setup direction in testing maybe angels dont works correctly
    private Vector2 front = new Vector2(1, 0);

    public Vector3 minmumSize;
    public Vector3 directionVector;
    public float power = 0;
    public bool active = false;

    private Vector3 target;
    [SerializeField] private Vector3 strechScale = new Vector3(1, 0.3F, 0.3f); // just x and z is important
    [SerializeField]private bool testing = false; // you must turn this off when you want to use it in game
    [SerializeField]private float maxPower =40;

    public GameObject flesh;

    private float forWardCamZngle;

    private Vector2 forwardCamDirection = Vector3.zero;

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
        
        //this.transform.position = target; // go sprite in to the target place you must set it before setup phase
        
        float directionAngle;
        Vector3 directionVector = (new Vector2(Camera.main.transform.forward.x,Camera.main.transform.forward.z));

        
        float t1 = 0;
        float t2 = Vector2.SignedAngle((endPos-startPos).normalized,Vector2.up);
        
        
        if (Vector2.Distance(forwardCamDirection,directionVector) > 0.01f)
        {
            t1 = Vector2.Angle(new Vector2(transform.forward.x,transform.forward.z),directionVector);
            forwardCamDirection = directionVector;
            transform.Rotate(0,t1,0);
            forWardCamZngle = transform.eulerAngles.y;
        }
        
        transform.eulerAngles = new Vector3(0, forWardCamZngle+t2, 0);//setup new scale

        
       this.directionVector = transform.forward.normalized;
        
        float Stretched = Vector3.Distance(endPos ,startPos);
        this.active = true;
        power = Mathf.Clamp(Stretched*10, 5, maxPower);
        transform.localScale = minmumSize + power * strechScale; //setup new angle
        flesh.SetActive(true);
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
            setup(Vector2.down,Vector2.down);
            /*
            flesh.SetActive(true);
            float directionAngle;
            directionAngle = Vector2.SignedAngle(Vector2.right, directionVector);
            power = Mathf.Clamp(power, 0, 1);
            this.gameObject.transform.localScale = minmumSize + new Vector3(power, 0, power);
            this.gameObject.transform.eulerAngles = new Vector3(0, directionAngle, 0);
            */
        }
    }
#endif
    private void OnEnable()
    {
        GameManager.Instance.onChoosingCharacterAction += setTargetTransform;
        disableFlesh();
    }
}
