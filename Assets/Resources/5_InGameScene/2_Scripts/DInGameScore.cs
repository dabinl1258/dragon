using UnityEngine;
using System.Collections;

public class DInGameScore : MonoBehaviour {

    public static DInGameScore instance = null;

    private float distance = 0; // 거리
    [SerializeField]
    private int scoreRate; // 거리당 점수
    private int score;



	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        SettingDistance();
	}

    void SettingDistance() // 최대거리로 갱신
    {
        if (transform.position.x > distance)
            distance = transform.position.x;
    }

    public void UpScore(int _score)
    {
        score += _score;
    }

    public int GetTotalScore()
    {
        return score + (int)Mathf.Round(distance * scoreRate);
    }

  
}
