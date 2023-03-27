using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    
    public float speed = 15.0f;

    PoolContent poolContent;

    private void Start()
    {
        poolContent = transform.GetComponent<PoolContent>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(transform.localPosition.z > 120  ||
           transform.localPosition.z < -120 ||
           transform.localPosition.x > 120  ||
           transform.localPosition.x < -120)
        
        {
            poolContent.Hide();
        }

    }

}
