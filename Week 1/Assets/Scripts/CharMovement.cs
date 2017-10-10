using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMovement : MonoBehaviour {

    Transform trans;
    Rigidbody body;

    [SerializeField]
    int vertRotationSpeed;
    [SerializeField]
    int horaRotationSpeed;

    void Start()
    {
        trans = GetComponent<Transform>();
        body = GetComponent<Rigidbody>();
        //body.velocity = new Vector3(0, 0, 5);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            trans.Rotate(Vector3.right * vertRotationSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            trans.Rotate(-Vector3.right * vertRotationSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            trans.Rotate(Vector3.forward * horaRotationSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            trans.Rotate(-Vector3.forward * horaRotationSpeed * Time.deltaTime);
    }
}
