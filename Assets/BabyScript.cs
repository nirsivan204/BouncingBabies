using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb.AddForce(Vector2.right*50);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
