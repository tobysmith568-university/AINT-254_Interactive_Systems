using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColourScript : MonoBehaviour {

    [SerializeField]
    private Vector3 customPosition;

    void Start()
    {
        GetComponent<Renderer>().sharedMaterial.color = Color.red;
    }

    void Update()
    {
        transform.position = customPosition;
    }
}