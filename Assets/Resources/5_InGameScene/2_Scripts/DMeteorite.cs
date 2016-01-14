using UnityEngine;
using System.Collections;

public class DMeteorite : DEnemyObj
{
    /// 떨어지는 운석
    /// 
    #region Variable
    
    #endregion 

    #region Virtual Function
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }

    
    #endregion

    #region Custum Function
    public void Create_Object(Vector3 _pos)
    {
        transform.position = _pos;
        gameObject.SetActive(true);
        
    }

    virtual public void HitWithPlayer()
    {
        DPlayers.instance.RestEnegy(restEnergy);
        gameObject.SetActive(false);
    }

    protected virtual void Miss()
    {
        gameObject.SetActive(false);
    }
    
    #endregion 
}
