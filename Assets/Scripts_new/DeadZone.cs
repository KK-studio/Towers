using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            Debug.Log("Dead");
            Dragable pawn = other.GetComponent<Dragable>();
            GameManager.Instance.removePawn(pawn);
        }
    }
}