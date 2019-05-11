using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChildManager : MonoBehaviour
{
    public GameObject ChildObject;
    public int ChildrenCount = 5;

    public float MinSpawnX = 1.5f;
    public float MaxSpawnX = 5f;
    public float SpawnY = -4.5f;

    public KeyCode Jump = KeyCode.Space;

    private List<GameObject> _children = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < ChildrenCount; ++i)
            _children.Add(Instantiate(ChildObject, new Vector3(UnityEngine.Random.Range(MinSpawnX, MaxSpawnX), SpawnY, UnityEngine.Random.Range(-1f, 1f)), Quaternion.identity, transform));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(Jump))
           DoJump();
    }

    private void DoJump()
    {
        var firstPos = GetFirstChild().transform.position.x;

        foreach (var child in _children)
        {
            child.GetComponent<ChildController>().Jump(firstPos);
        }
    }

    private GameObject GetFirstChild()
    {
        GameObject result = null;

        foreach (var child in _children)
        {
            if (!result || child.transform.position.x > result.transform.position.x)
                result = child;
        }

        return result;
    }
}
