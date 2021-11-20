using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow: MonoBehaviour
{
    Vector3 offet;
    public GameObject player;
    private float posy;
    
    void Start()
    {
        offet = transform.position - player.transform.position;
        posy = transform.position.y;
    }

    
    void Update()
    { 
        transform.position = offet + player.transform.position;
        transform.position = new Vector3(transform.position.x, posy,-10);
    }
}
