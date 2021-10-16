using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSight : MonoBehaviour
{
    public Rigidbody rigbod;
    public Mover mover;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Monster"))//tutaj przeniesc do sceny walki
        {
            mover.routeCounter++;
            other.gameObject.SetActive(false);
            StaticValues.Frytki++;

        }
        
       
    }

}
