using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Lawnmower")
        {
            Debug.Log("lol");
            Destroy(hit.gameObject);
        }
    }
}
