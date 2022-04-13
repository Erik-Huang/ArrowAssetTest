using UnityEngine;

public class Example : MonoBehaviour
{
    private void Start()
    {
        GameObject go1 = new GameObject();
        go1.name = "go1";
        go1.AddComponent<Rigidbody>();

        GameObject go2 = new GameObject("go2");
        go2.AddComponent<Rigidbody>();

        GameObject go3 = new GameObject("go3", typeof(Rigidbody), typeof(BoxCollider));
    }
}