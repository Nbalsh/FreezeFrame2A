using UnityEngine;
using System.Collections;

public class ChangePlayerColour : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            col.gameObject.GetComponent<SpriteRenderer>().color = this.GetComponent<ParticleSystem>().startColor;
        }
    }
}
