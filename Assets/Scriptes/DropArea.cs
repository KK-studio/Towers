using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameManager.Instance.OnDrop(collision.gameObject.GetComponent<TowerComponent>().PlayerID);
    }
}
