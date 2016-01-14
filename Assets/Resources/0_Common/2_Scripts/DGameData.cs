using UnityEngine;
using System.Collections;

public class DGameData : MonoBehaviour {
    //캐릭터 왼쪽순서대로 적용
    public float [] hp = new float[4]{10,10,10,10};
    public float enemyHp = 10;
    public float enemyAttack = 4;
    public string []skillName  = new string[4] {"skill","skill","skill","skill"};
    public float []skillDemage  = new float[4] {2,2,2,2};
    public float[] skillCoolTime = new float[4] {1,1,1,1};
    public float[] AttackDemage = new float[4]{1,1,1,1};
    public float[] skillParticleCollTime = new float[4] { 0.1f, 0.1f, 0.1f, 0.1f };
    public string[] skilParticleDirectory = new string[4];

    static DGameData instance = null;
    public static DGameData GetInstance()
    {
        if (instance == null)
        {
            instance = new DGameData();
        }
        return instance;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
