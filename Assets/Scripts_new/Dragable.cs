using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    [SerializeField] private bool throwable = true;

    [SerializeField] private float height = 1f;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float throwForce = .5f;


    private Vector3 mousePos;
    private Rigidbody _rigidbody;
    private bool isDragging = false;
    private Vector3 startDragPos;

    private void Start()
    {
        this._rigidbody = this.GetComponent<Rigidbody>();
    }


    void OnMouseDown()
    {
        isDragging = true;
        startDragPos = Input.mousePosition;
        startDragPos.z = 20;
        startDragPos = Camera.main.ScreenToWorldPoint(startDragPos);
    }

    private void OnMouseDrag()
    {
        if (throwable)
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 20;
            pos = Camera.main.ScreenToWorldPoint(pos);
            Debug.Log("end :" + pos);
        
            isDragging = false;
            Vector3 throwDirection = startDragPos - pos;
            Debug.Log("throw direction :" + throwDirection);
            throwDirection.y = 0; // never jump :)
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
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
        _rigidbody.useGravity = true;
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