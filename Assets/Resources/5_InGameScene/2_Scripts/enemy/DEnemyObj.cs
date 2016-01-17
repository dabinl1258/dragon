using UnityEngine;
using System.Collections;

public class DEnemyObj : MonoBehaviour {
    // 최상위 오브젝트
    #region Variable
    public float restEnergy = 10.0f;
    public int health = 5;
    private int realHealth;
    private int score = 1; // 죽이고 얻을 수 있는 스코어
    bool life = true; 
    #endregion

    #region Virtual Function
    // Use this for initialization
    void Start()
    {
        realHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (DMainCamera.instance.GetCollision() > transform.position.x)
        {
            gameObject.SetActive(false);
            Miss();
        }
    }


    #endregion

    #region Custum Function

    public void Create_Object(Vector3 _pos) // 오브젝트 생성
    {
        gameObject.SetActive(true);
        transform.position = _pos;
        realHealth = health;
        gameObject.SendMessage("Reset");
        life = true;
    }

    virtual public void HitWithPlayer()
    {
        if (!life)
            return;
        DPlayers.instance.UpCombo();
        realHealth -= 1;
        if (realHealth <= 0)
        {
            forceDead();
            return;
        }
        gameObject.SendMessage("Push");
    }
    

    virtual public void forceDead()
    {
        life = false;
        gameObject.SendMessage("EnableEffect");
        gameObject.SendMessage("StopAuto");
        DInGameScore.instance.UpScore(score);
        DCreateCoin.instance.CreateCoin(transform.position, health);
     }


    protected virtual void Miss()
    {
        if (!life)
            return;
        DPlayers.instance.RestEnegy(restEnergy);
        gameObject.SetActive(false);
        Debug.Log("miss");
    }

    protected void CheckMiss()
    {
        if (DPlayers.instance.transform.position.x + 5 >= transform.position.x)
            Miss();
    }


    #endregion 


}
