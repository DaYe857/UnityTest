using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collculeground : MonoBehaviour
{
    public bool isground;
    
    
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="ground") { isground = true; }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isground = false;
        
    }
}
