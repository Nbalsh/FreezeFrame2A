using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D otherCol)
    {
        if(otherCol.gameObject.tag == "Player")
        {
            gameObject.GetComponent<ParticleSystem>().startColor = Color.green;
            otherCol.gameObject.GetComponent<PlayerController>().SetCheckpoint(transform.position);
        }

    }
}
