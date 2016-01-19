using UnityEngine;
using System.Collections.Generic;
public class DObjectPool
{
    GameObject type;
    List<GameObject> pool = new List<GameObject>();
    public List<DHitWithPlayer> poolCash = new List<DHitWithPlayer>();
    private int arrayIndex = 0;
    public DObjectPool(GameObject _type , int _arrayIndex)
    {
        arrayIndex = arrayIndex;
        type = _type;
        for (int i = 0; i < 5; i++)
        {
            
            Add().SetActive(false);
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

        return Add();
    }

    GameObject Add()
    {
        GameObject temp = MonoBehaviour.Instantiate(type) as GameObject;
        temp.name += "," + arrayIndex.ToString() + "," + pool.Count.ToString();
        pool.Add(temp);
        poolCash.Add(temp.GetComponent<DHitWithPlayer>());
        return temp;
    }

    public void SetType(GameObject _type)
    {
        type = _type;
    }

}