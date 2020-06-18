using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Coroutine isHoldingWithTouch = null;
    private bool isAiming = false;
    private Vector2 endPos;
    private Vector2 startPos;
    [SerializeField] private float timeToAim;
    [SerializeField] private Aim Aim;
    private float timer = 0;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
        if (Physics.Raycast(ray, out hit, 100000))
        {
            if (hit.collider.CompareTag("Tower"))
            {
                Debug.Log("selected");
                GameManager.Instance.OnChoosingCharacter(hit.collider.transform);
            }
        }
#else

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
                    isAiming = false;
                    GameManager.Instance.OnShoot(Aim.directionVector,Aim.power);//TODO power setter with stamina
                }
            }
            else if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Stationary)
            {
                timer += Time.deltaTime;
                //Selection system
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    if (hit.collider.CompareTag("Tower"))
                    {
                        Debug.Log("selected");
                        Aim.disableFlesh();
                        GameManager.Instance.OnChoosingCharacter(hit.collider.transform);
                    }
                }
                /////////////////////////
                if(timer > timeToAim)
                {
                    isAiming = true;
                    startPos = touch.position;
                    endPos = startPos;
                    Aim.setup(endPos,startPos);
                    timer = 0;
                }

            }
            else if(touch.phase == TouchPhase.Moved)
            {
                timer += Time.deltaTime;
                if(timer > timeToAim)
                {
                    isAiming = true;
                    startPos = touch.position;
                    endPos = startPos;
                    Aim.setup(endPos,startPos);
                    timer = 0;
                }
            }
            else
            {
                timer = 0;
            }
            /*
            else if()//start aiming
            {
                isAiming = true;
                startPos = touch.position;
                endPos = startPos;
                Aim.setup(endPos,startPos);                
            }*/
        }
#endif
    }
}
