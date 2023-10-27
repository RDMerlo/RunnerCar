using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Settings.posPlayer) > 400){
            Destroy(gameObject);
        }
    }
}
