using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private string playerTag;

    public Transform Player { get; private set; }

    public ObjectPool ObjectPool { get; private set; }

    private void Awake()
    {
        if(instance != null) Destroy(instance);
        instance = this;

        Player = GameObject.FindGameObjectWithTag(playerTag).transform;
        ObjectPool = GetComponent<ObjectPool>();
    }
}
