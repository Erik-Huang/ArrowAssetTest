using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[AddComponentMenu("Scripts/MyTransform")]
public class MyTransform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float speed = 0.1f;
    private Vector3 direction = new Vector3(0.0f, 0.0f, 30.0f);

    public global::System.Single Speed { get => speed; set => speed = value; }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        transform.Rotate(direction * Time.deltaTime);
    }
}
