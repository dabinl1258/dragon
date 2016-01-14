using UnityEngine;
using System.Collections;

public class DMeteoriteArlet : MonoBehaviour {


    public float time; // 생성으로 부터 이시간 뒤에  생성
    public GameObject meteorite = null;
    public float makePosX; //만들어 지는 위치 보정 값
    
	// Use this for initialization
	void Start () {
        StartCoroutine("CreateTimer");
	}
	
	// Update is called once per frame
	void Update () {

	}

    void CreateMeteorite()
    {
        GameObject temp = Instantiate(meteorite) as GameObject;
        temp.SendMessage("Create_Object", new Vector3(DPlayers.instance.transform.position.x + makePosX, 
            DPlayers.instance.transform.position.y));
    }

    IEnumerator CreateTimer()
    {
        yield return new WaitForSeconds(time);
        CreateMeteorite();
        gameObject.SetActive(false);
    }
}
