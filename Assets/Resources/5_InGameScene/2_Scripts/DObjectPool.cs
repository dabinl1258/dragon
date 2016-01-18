using UnityEngine;
using System.Collections.Generic;

public class DObjectPool
{
    GameObject type;
    List<GameObject> pool = new List<GameObject>();

    public DObjectPool(GameObject _type)
    {
        type = _type;
        for (int i = 0; i < 5; i++)
        {
            GameObject temp = MonoBehaviour.Instantiate(type) as GameObject;
            pool.Add(temp);
            temp.SetActive(false);
        }
    }

    public GameObject GetAble()// 사용가능한 오브젝트
    {
        int count = 0;
        for (int i = 0; i < pool.Count; i++)
        {
            count++;
            bool active = pool[i].activeInHierarchy;
            if (!active)
            {
                Debug.Log("재활용");
                pool[i].SetActive(true);
                return pool[i];
            }

        }
        GameObject temp = MonoBehaviour.Instantiate(type) as GameObject;
        Debug.Log("생성 ");
        pool.Add(temp);
        return temp;
    }

    public void SetType(GameObject _type)
    {
        type = _type;
    }
}