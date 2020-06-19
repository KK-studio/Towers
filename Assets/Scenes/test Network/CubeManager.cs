using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Generated;

public class CubeManager : CubeTestBehavior
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void NetworkStart()
    {
        base.NetworkStart();

        // If this networkObject is actually the **enemy** Player
        // hence not the one we will control and own
        if (!networkObject.IsOwner)
        {
            // Don't render through a camera that is not ours
            // Don't listen to audio through a listener that is not ours
     //       transform.GetChild(0).gameObject.SetActive(false);

            // Don't accept inputs from objects that are not ours
            GetComponent<MovmentEasy>().enabled = false;

        }

        // Assign the name when this object is setup on the network
     
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if we are NOT the owner of this player
        if (!networkObject.IsOwner)
        {
            // Set this object's transform.position
            // to the position that is syndicated across the network
            // In simpler words, its position is updated via the network.
            transform.position = networkObject.pos;
            return;
        }

        networkObject.pos = transform.position;
    }
}
