using UnityEngine;
using System.Collections;

public class DeathArea : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D otherCol)
    {
        if(otherCol.gameObject.tag == "Player")
        {
            otherCol.gameObject.GetComponent<PlayerController>().Die();
        }

    }
}
