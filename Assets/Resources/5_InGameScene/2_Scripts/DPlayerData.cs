using UnityEngine;
using System.Collections;

public class DPlayerData : MonoBehaviour {
    public static DPlayerData instance;
    void Start()
    {
        instance = this;
    }
    public float restEnergyRate = 1.0f; // 피해량
    public float itemTimeRate = 1.0f; // 지속시간 비율
    public float uptoEnergyRate = 1.0f; // 에너지 회복 비율
    public int playerIndex = 0; // 몇번 플레이어 인지
    
}
