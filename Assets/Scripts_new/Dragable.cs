using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private bool throwable = true;
    [SerializeField] private float height = 1f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float throwForce = 500f;
    [SerializeField] private String _guid;

    // consts
    private const float ACTIVE_TSH = 0.5f;
    private const float DEACTIVE_TSH = 5f;


    private Vector3 mousePos;
    private Rigidbody _rigidbody;
    private bool isDragging = false;
    private Vector3 startDragPos;
    private Vector3 throwDirection;
        
        

    private void Start()
    {
        this._rigidbody = this.GetComponent<Rigidbody>();
        this._guid = Guid.NewGuid().ToString();
        highlightOffMaterial();
    }


    void OnMouseDown()
    {
        highlightMaterial();
        isDragging = true;
        startDragPos = Input.mousePosition;
        startDragPos.z = 20;
        startDragPos = Camera.main.ScreenToWorldPoint(startDragPos);
    }

    private void OnMouseDrag()
    {
        if (throwable)
        {
            int layerMask = 1 << 8;
            layerMask += 1 << 9;
            // layerMask = ~layerMask;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, layerMask = layerMask))
            {
                Debug.Log(hit.transform.name);
                Vector3 newPos = hit.point;
                newPos.y += height;
                throwDirection = transform.position - newPos ;
                throwDirection.y = 0; //no need move throw sky :)
            }
        }
        else
        {
            int layerMask = 1 << 8;
            // layerMask = ~layerMask;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, layerMask = layerMask))
            {
                Debug.Log(hit.transform.name);
                // Debug.Log("hit");
                // Debug.Log(hit.point);
                Vector3 newPos = hit.point;
                newPos.y += height;
                transform.position = newPos;
                // StartCoroutine(moveSmooth(newPos));
            }
        }
    }


    private void OnMouseUp()
    {
        if (throwable)
        {
            // Vector3 pos = Input.mousePosition;
            // pos.z = 20;
            // pos = Camera.main.ScreenToWorldPoint(pos);
            // // Debug.Log("end :" + pos);
            //
            // isDragging = false;
            // throwDirection = startDragPos - pos;
            // // Debug.Log("throw direction :" + throwDirection);
            // throwDirection.y = 0; // never jump :)
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
        }
        
        highlightOffMaterial();
        _rigidbody.useGravity = true;
    }


    private void highlightMaterial()
    {
        Debug.Log("highlighted");
        renderer.material.SetFloat("_Amount", ACTIVE_TSH);
    }


    private void highlightOffMaterial()
    {
        renderer.material.SetFloat("_Amount", DEACTIVE_TSH);
    }

    public String getGUID()
    {
        return this._guid;
    }

    // IEnumerator moveSmooth(Vector3 tartgetPos)
    // {
    //     _rigidbody.useGravity = false;
    //     Debug.Log("get it");
    //     Vector3 direction = (transform.position - tartgetPos).normalized;
    //     while (Vector3.Distance(this.mousePos, tartgetPos) > threshold)
    //     {
    //         _rigidbody.MovePosition(_rigidbody.position + direction * Time.fixedDeltaTime * speed);
    //         yield return new WaitForFixedUpdate();
    //     }
    //
    //     _rigidbody.useGravity = true;
    // }
}