using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public GameObject[] items;
    public float Speed=60f;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < items.Length; i++) {
            items[i].transform.Rotate(Vector3.up, Speed * Time.deltaTime,Space.World);
        }
    }
}
