using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Coroutine isHoldingWithTouch = null;
    private bool isAiming = false;
    private Vector2 endPos;
    private Vector2 startPos;
    
    [SerializeField] private Aim Aim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (isAiming)
            {
                if (touch.phase.Equals(TouchPhase.Moved) || (touch.phase.Equals(TouchPhase.Stationary)))//continue aiming
                {
                    endPos = touch.position;
                    Aim.setup(endPos, startPos);
                }
                else//release aiming
                {
                    Aim.disableFlesh();
                    GameManager.Instance.OnShoot((startPos - endPos).normalized,1);//TODO power setter with stamina
                }
            }
            else if (touch.phase == TouchPhase.Began)
            {
                //Selection system
                RaycastHit hit = new RaycastHit();
                Ray ray = new Ray(touch.position,Vector3.down);
                
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    if (hit.collider.CompareTag("ClientTower"))
                    {
                        GameManager.Instance.OnChoosingCharacter(hit.collider.gameObject);
                    }
                }
                /////////////////////////
                else
                {
                    /*
                    isAiming = true;
                    startPos = touch.position;
                    endPos = startPos;
                    Aim.setup(endPos,startPos); 
                    */
                }

            }
            else if(touch.phase == TouchPhase.Stationary)//start aiming
            {
                isAiming = true;
                startPos = touch.position;
                endPos = startPos;
                Aim.setup(endPos,startPos);                
            }
        }
    }
}
