using UnityEngine;
using System.Collections;

public class NextLevelTele : MonoBehaviour {
    public string next_level;

    void OnTriggerEnter2D(Collider2D otherCol)
    {
        if(otherCol.gameObject.tag == "Player")
        {
            Application.LoadLevel(next_level);
        }
    }
}
