using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // 유니티에서 접어서 볼 수 있도록 하는거.
    [System.Serializable]

    // pool 데이터
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    /*
     * pools = 실제로 pool을 넣는 부분.
     * 선언후 할당이 되어있지 않으면 유니티 외부로 꺼내오지 않는듯.
     */
    public List<Pool> pools = new List<Pool>();
    public Dictionary<string, Queue<GameObject>> PoolDictionary;


    private void Awake()
    {
        // Dictionary 생성
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        /*  
            pools에 담겨져있는 데이터를 가지고 queue를 생성함.
            여기서 queue가 각 오브젝트 풀이 되고
            Dictionary는 오브젝트 풀을 모아놓는 역할.
            pools는 그냥 데이터 저장용.
        */
        foreach(var pool in pools)
        {
            // pools에 담겨져 있는 데이터를 불러와서 queue에 대입함.
            Queue<GameObject> queue = new Queue<GameObject>();
            for(int i=0; i<pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);

                // setActive 한상태로 넣음.
                obj.SetActive(false);
                queue.Enqueue(obj);
            }

            // pool tag로 이름짓고, queue를 dict에 집어넣음.
            PoolDictionary.Add(pool.tag, queue);
        }
    }

    // tag를 받아와서 gameobject를 준다.
    public GameObject SpawnFromPool(string tag)
    {
        if(!PoolDictionary.ContainsKey(tag))
        {
            return null;
        }

        // 1개 꺼내옴. (깊은복사)
        GameObject obj = PoolDictionary[tag].Dequeue();

        // 다시 넣음.
        PoolDictionary[tag].Enqueue(obj);

        // 꺼낼 때 setActive true로
        obj.SetActive(true);
        return obj;
    }
}
