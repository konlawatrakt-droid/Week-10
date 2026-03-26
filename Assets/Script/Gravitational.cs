using System.Collections.Generic;
using Unity.Android.Gradle.Manifest;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Gravitation : MonoBehaviour
{

    Rigidbody rb;
    const float G = 0.006674f;
    public static List<Gravitation> otherObjList;

    [SerializeField] bool planet = false;
    [SerializeField] int orbitSpeed = 1000;
    
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (otherObjList == null)
        {
            otherObjList = new List<Gravitation>();

        }

        otherObjList.Add(this);

        if (!planet)
        { rb.AddForce(Vector3.left * orbitSpeed); }
    }
    void FixedUpdate()
    {
        foreach (Gravitation obj in otherObjList)
        {
            if (obj != this)
            { 
            Attract(obj);
            }

        }
    }
    void Attract(Gravitation other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;

        float distance = direction.magnitude;
        if (distance == 0f) return;

        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);
        Vector3 gravitationForce = forceMagnitude * direction.normalized;
        otherRb.AddForce(gravitationForce);

    }


}

