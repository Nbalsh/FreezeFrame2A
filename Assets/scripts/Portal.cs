using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
    public GameObject target;
    bool canPort = true;
 
    void OnTriggerEnter2D(Collider2D otherCol)
    {
        if (canPort)
        {

            if (otherCol.gameObject.tag == "Player" && otherCol.gameObject.GetComponent<SpriteRenderer>().color == this.gameObject.GetComponent<ParticleSystem>().startColor)  
            {
                target.GetComponent<Portal>().canPort = false;
                otherCol.gameObject.transform.position = new Vector2(target.transform.position.x, target.transform.position.y);
            } else if (otherCol.gameObject.tag == "Player" && this.gameObject.GetComponent<ParticleSystem>().startColor == Color.white)
            {
                target.GetComponent<Portal>().canPort = false;
                otherCol.gameObject.transform.position = new Vector2(target.transform.position.x, target.transform.position.y);
            }
        }

    }

    void OnTriggerExit2D(Collider2D otherCol)
    {
        if(otherCol.tag == "Player")
        {
            canPort = true;
        }
    }
}
