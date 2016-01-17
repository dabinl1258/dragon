using UnityEngine;
using System.Collections;

public class DItem : MonoBehaviour {

    // 최상위 오브젝트
    #region Variable
    #endregion

    #region Virtual Function
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckMiss();
    }


    #endregion

    #region Custum Function

    public void Create_Object(Vector3 _pos) // 오브젝트 생성
    {
        gameObject.SetActive(true);
        transform.position = _pos;
    }

    public virtual void HitWithPlayer()
    {
    }
    protected virtual void Miss()
    {
        gameObject.SetActive(false);
    }

    protected void CheckMiss()
    {
        if (DMainCamera.instance.GetCollision() > transform.position.x)
        {
            Miss();
        }
    }
    #endregion 

}
