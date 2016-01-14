using UnityEngine;
using System.Collections;

public class DEnemyObj : MonoBehaviour {
    // 최상위 오브젝트
    #region Variable
    public float restEnergy = 10.0f;
    public int health = 5;
    private int realHealth;
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
    }

    virtual public void HitWithPlayer()
    {
        realHealth -= 1;
        if (realHealth <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        
        gameObject.SendMessage("Push");
    }

    virtual public void forceDead()
    {
    }


    protected virtual void Miss()
    {
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
