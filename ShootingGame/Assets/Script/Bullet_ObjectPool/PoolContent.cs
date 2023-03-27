using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolContent : MonoBehaviour
{
    /*�v�[���̒��Ɋi�[����Ă�*/

    ObjectPool pool;

    private void Start()
    {
        pool = transform.parent.GetComponent<ObjectPool>();
        gameObject.SetActive(false);
    }

    private void Update()
    {

    }

    /*�e��������*/
    public void Show(Vector3 position , float angle)
    {
        transform.position = position;
        transform.eulerAngles = new Vector3(0 , angle , 0);
    }


    /*�e���B��*/
    public void Hide()
    {
        Debug.Assert(gameObject.activeInHierarchy);
        pool.Collect(this);
    }
}
