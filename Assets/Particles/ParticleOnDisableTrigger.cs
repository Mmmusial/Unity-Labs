using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnDisableTrigger : MonoBehaviour
{

    public ParticleSystem particleSystem;
    private void OnDisable()
    {
        if (particleSystem)
        {
            var system = Instantiate(particleSystem, transform.position,transform.rotation);
            system.Play();
        }
    }

   
}
