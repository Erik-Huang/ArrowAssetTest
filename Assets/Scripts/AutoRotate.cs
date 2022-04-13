using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [AddComponentMenu("Scripts/AutoRotate")]
public class AutoRotate : MonoBehaviour
{
    //public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // make it a child of the current object
        Vector3 displacement = Camera.main.transform.position - transform.position;
        // place it in front of the current object
        //transform.localPosition = Vector3.forward * 5;
        // transform.rotation = Quaternion.identity;
        transform.forward = displacement.normalized;
    }
}
