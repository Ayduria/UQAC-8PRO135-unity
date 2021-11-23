using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    private int totalCount = 0;
    public Text lawnmowerCount;


    private void Update()
    {
        lawnmowerCount.text = "Tondeuses: " + totalCount;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Lawnmower")
        {
            totalCount++;
            Destroy(hit.gameObject);
        }
    }
}
