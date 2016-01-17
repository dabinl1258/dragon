using UnityEngine;
using System.Collections;

public class DCoin : DItem
{
    static Vector3 rotateVec3 = new Vector3(0, 180, 0);
    public int coin = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        CheckMiss();
    }

    public void Create_Object(Vector3 _pos) // 오브젝트 생성
    {
        gameObject.SetActive(true);
        transform.position = _pos;
    }

    void FixedUpdate()
    {
        Rotating();
    }

    void Rotating()
    {
        transform.localEulerAngles += Time.deltaTime * rotateVec3;
    }

    public void HitWithPlayer()
    {
        DInGameScore.instance.UpCoin(coin);
        gameObject.SetActive(false);
        print("hit with coin");
    }
}
