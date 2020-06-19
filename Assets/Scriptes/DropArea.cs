using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("dead");
        GameManager.Instance.OnDrop(other.gameObject.GetComponent<TowerComponent>());
    }
}
