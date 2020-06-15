using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    // must setup direction in testing maybe angels dont works correctly
    private Vector2 front = new Vector2(1, 0); 
    public Vector2 direction;
    public float power = 0;
    public bool active = false;

    public GameObject flesh;

    // Start is called before the first frame update
    public void setup(Vector2 direction,float power , bool active)
    {
        this.direction = direction;
        this.power = power;
        this.active = active;
    }


    private void Start()
    {
        flesh.SetActive(false);
    }
}
