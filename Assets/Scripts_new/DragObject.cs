using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private bool isDragging = false;
    [SerializeField]private float throwForce = .5f;

    private Vector3 start_pos;
    

    void OnMouseDown()
    {
        isDragging = true;
        start_pos = Input.mousePosition;
        start_pos.z = 20;
        start_pos = Camera.main.ScreenToWorldPoint(start_pos);
        Debug.Log("Start : " + start_pos.ToString());
    }

    void OnMouseDrag()
    {
        //TODO 
        // Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        // Vector3 objectPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        // objectPosition.y = transform.position.y;
        // transform.position = objectPosition;
    }

    void OnMouseUp()
    {
        // Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 25);
        // Debug.Log(mousePosition);
        // Vector3 endPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        // Debug.Log(endPosition);
        
        Vector3 pos = Input.mousePosition;
        pos.z = 20;
        pos = Camera.main.ScreenToWorldPoint(pos);
        Debug.Log("end :" + pos);
        
        isDragging = false;
        Vector3 throwDirection = start_pos - pos;
        Debug.Log("throw direction :" + throwDirection);
        throwDirection.y = 0; // never jump :)
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);
    }
}