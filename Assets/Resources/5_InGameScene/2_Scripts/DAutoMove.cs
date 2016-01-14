using UnityEngine;
using System.Collections;

public class DAutoMove : MonoBehaviour {
    /*
     * 기체의 역학을 관리 합니다.
     */
    #region  Speed

    bool getAttack = false;
    public float getAttackSpeed = 0.0f;

    public float speed = 0.0f; // 왼쪽으로 이동하는 속도
    public float horizontalSpeed; // 좌우 이동 속도
    public float horizontalRage; // 좌우 이동 범위
    public float verticalSpeed; // 좌우 이동 속도
    public float verticalRage; // 좌우 이동 범위

    float pushXplus; // 밀려날 포지션
    private float pushX; // 밀려날 포지션
    public float pushPower; // 밀려나는 속도

    protected float activeTime = 0.0f;// 생성된 뒤에 흐른 시간
    public bool pushAble = false;
    #endregion 

    
    
    void FixedUpdate()
    {
        Fixed_Push();
        verticalMove();
        if (!pushAble)
        {
            horizontalMove();
        }
    }

    public void Push()
    {
        pushAble = true;
        pushX = transform.position.x + pushPower;
    }

    private void Fixed_Push()
    {
        if (pushAble)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, pushX, Time.deltaTime * pushPower)
                , transform.position.y);
            Check_Push();
        }
    }

    private void Check_Push()
    {
        if (0.1f > pushX - transform.position.x)// 목표에 가까워 지면 멈춤
        {
            pushAble = false;
        }
    }

    protected virtual void horizontalMove()
    {
        transform.position = new Vector3(transform.position.x + Mathf.Sin((Time.time + activeTime) * horizontalRage) * horizontalSpeed - (speed * Time.deltaTime),
            transform.position.y);
        if (getAttack)
        {
            transform.position = new Vector3(transform.position.x + Mathf.Sin((Time.time + activeTime) * horizontalRage) * horizontalSpeed - (getAttackSpeed * Time.deltaTime),
            transform.position.y);
        }

    }

    protected void verticalMove()
    {
        transform.position = new Vector3(transform.position.x,
            Mathf.Sin((Time.time + activeTime) * verticalSpeed) * verticalRage);
    }

    public void Reset()
    {
        pushAble = false;
        getAttack = false;
        activeTime = Time.time;
    }

    public void GetAttack()
    {
        getAttack = true;
    }

}   
