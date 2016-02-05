using UnityEngine;
using System.Collections;

public class PressureButton : MonoBehaviour
{
    public GameObject objToMove;
    Vector2 position;
    bool isActive;

    void Start()
    {
        position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        isActive = true;
    }

    void FixedUpdate()
    {
        objToMove.SetActive(isActive);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isActive = false;
        }
    }

    // concurrency? this runs before dead player is added on pressure button...
    void OnTriggerExit2D(Collider2D col)
    {
        if (!hasDeadPlayer(position) && col.gameObject.tag == "Player")
        {
            isActive = true;
        }
    }

    /*
        - returns boolean if there is at least 1 dead player in the collider
    */
    bool hasDeadPlayer(Vector2 pos)
    {
        Collider2D[] hitColliders = getCollidersForPressureButton();
       // Debug.Log("firstVector: " + firstVector + " SecondVector: " + secondVector);

        var isDeadPlayer = false;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "DeadPlayer")
                isDeadPlayer = true;
        }
        Debug.Log("hasDeadPlayer: " + isDeadPlayer);
        return isDeadPlayer;
    }

    /*
        called when deleting a freeze frame 'DeadPlayer' block
        needs to do: see if given position overlaps with pressureButton gameObject collider
        if more than two, item remains, if 1 then item disappears ( if 0 - stays same)
    */
    public void shouldBeTriggered()
    {
        Collider2D[] hitColliders = getCollidersForPressureButton(); 
        int count = 0;
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "DeadPlayer")
            {
                count++;
            }
        }
        
       if(!hasDeadPlayer(position))
        // if(count == 1)
        {
            Debug.Log("isActive: " + isActive);
            isActive = true;
        } else
        {
            isActive = false;
        }
        
    }


    private Collider2D[] getCollidersForPressureButton()
    {
        Bounds b = gameObject.GetComponent<Collider2D>().bounds;
        var halfSizeX = b.size.x / 2;
        var halfSizeY = b.size.y / 2;
        Vector2 firstVector = new Vector2(gameObject.transform.position.x - halfSizeX, gameObject.transform.position.y + halfSizeY);
        Vector2 secondVector = new Vector2(gameObject.transform.position.x + halfSizeX, gameObject.transform.position.y - halfSizeY);
        return Physics2D.OverlapAreaAll(firstVector, firstVector + secondVector);
    }

}


/*
 void Start()
    {
        position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        isActive = true;
    }

    void FixedUpdate()
    {
        objToMove.SetActive(isActive);
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "DeadPlayer")
        {
            isActive = false;
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (!hasDeadPlayer(position) && col.gameObject.tag == "Player")
        {
            isActive = true;
        }
    }

    bool hasDeadPlayer(Vector2 pos)
    {
        Bounds b = gameObject.GetComponent<Collider2D>().bounds;
        var halfSizeX = b.size.x / 2;
        var halfSizeY = b.size.y / 2;
        Vector2 firstVector = new Vector2(gameObject.transform.position.x - halfSizeX, gameObject.transform.position.y + halfSizeY);
        Vector2 secondVector = new Vector2(gameObject.transform.position.x + halfSizeX, gameObject.transform.position.y - halfSizeY);
        Collider2D[] hitColliders = Physics2D.OverlapAreaAll(firstVector, firstVector + secondVector);
        Debug.Log("firstVector: " + firstVector+ " SecondVector: " + secondVector);

        var isDeadPlayer = false;
        for(int i = 0; i < hitColliders.Length; i++)
        {
            if(hitColliders[i].tag == "DeadPlayer")
                return isDeadPlayer = true;
        }
        return isDeadPlayer;
    }

    /*
        called when deleting a freeze frame 'DeadPlayer' block
        needs to do: see if given position overlaps with pressureButton gameObject collider
    
public void shouldBeTriggered(Collider2D col)
{
    Bounds b = gameObject.GetComponent<Collider2D>().bounds;
    var halfSizeX = b.size.x / 2;
    var halfSizeY = b.size.y / 2;
    Vector2 firstVector = new Vector2(gameObject.transform.position.x - halfSizeX, gameObject.transform.position.y + halfSizeY);
    Vector2 secondVector = new Vector2(gameObject.transform.position.x + halfSizeX, gameObject.transform.position.y - halfSizeY);
    Collider2D[] hitColliders = Physics2D.OverlapAreaAll(firstVector, firstVector + secondVector);
    //Debug.Log("shouldBeTriggered hitColliders.length: " + hitColliders.Length);
    int count = 0;
    for (int i = 0; i < hitColliders.Length; i++)
    {
        if (hitColliders[i].tag == "DeadPlayer")
        {
            //Debug.Log("Got a dead player");
            count++;
        }
    }
    Debug.Log("count: " + count);
    if (count == 1)
    {
        isActive = true;
    }
}

    */