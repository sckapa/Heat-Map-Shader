using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHandler : MonoBehaviour
{
    float mDelay = 3;

    void Update()
    {
        mDelay -= Time.deltaTime;
        if (mDelay <= 0)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            bool hitit = Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("HeatMapLayer"));

            GameObject quad = hit.collider.gameObject;
            QuadScript quadScript = quad.GetComponent<QuadScript>();
            if (hitit)
            {
                quadScript.enabled = true; // Set QuadScript as inactive
                quadScript.GetRaycastHit(hit);
            }
            else
            {
                if (quad)
                {
                    quadScript.enabled = false; // Set QuadScript as active
                }
            }

            mDelay = 0.5f;
        }
    }
}
