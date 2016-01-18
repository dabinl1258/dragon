using UnityEngine;
using System.Collections;
public class DCreateCoin : MonoBehaviour {
    public static DCreateCoin instance = null;
    public DCoin []coinArray;
    private DObjectPool[] objectPool;
	// Use this for initialization
	void Start () {
        instance = this;
        objectPool = new DObjectPool[coinArray.Length];
        for (int i = 0; i < coinArray.Length; i++)
        {
            objectPool[i] = new DObjectPool(coinArray[i].gameObject);
        }
        for (int i = 0; i < coinArray.Length; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                objectPool[i].GetAble().SetActive(false);
            }

        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void CreateCoin(Vector3 _createPos, int _coin)
    {
        if(_coin >= objectPool.Length)
        {
            print("어레이를 늘려야 할듯" + _coin);
            return;
        }
        objectPool[_coin].GetAble().SendMessage("Create_Object",(_createPos + new Vector3(10.0f, 0)));
    }
}
