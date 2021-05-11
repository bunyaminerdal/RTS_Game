using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickMarkerController : MonoBehaviour
{

    void Update()
    {
        if (transform.childCount > 1)
        {
            foreach (Transform child1 in transform)
            {
                foreach (Transform child2 in transform)
                {
                    if (child2!=child1)
                    {
                        if ((child2.transform.position-child1.transform.position).sqrMagnitude<0.1f)
                        {
                           
                            if(child1.gameObject.activeSelf && child2.gameObject.activeSelf)
                            {
                                child1.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }        
        }
    }
}
 