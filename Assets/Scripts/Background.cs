using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Transform _player;
    void Start()
    {
        //Debug.Log(transform.position.y + "!!!");
        transform.position = new Vector3(gameObject.transform.position.x, _player.position.y+2f, gameObject.transform.position.z);
    }
    private void LateUpdate()
    {
        //Debug.Log(_player.position.y);
        Debug.Log(transform.position.y + "!!!");
        transform.position = new Vector3(gameObject.transform.position.x, _player.position.y + 2f, gameObject.transform.position.z);
    }
}
