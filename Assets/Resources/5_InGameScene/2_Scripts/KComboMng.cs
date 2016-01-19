using UnityEngine;
using System.Collections;

public class KComboMng : MonoBehaviour {
    public UILabel comboLabel;

    string comboText = " Hit!";
    string comboCountText;

    [SerializeField]
    int comboCount = 0;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        
        if(comboCount == 0 )
        {
            comboLabel.gameObject.SetActive(false);
        }

        comboLabel.text = comboCountText + comboText;

        if(Input.GetKeyDown(KeyCode.Z))
        {
            Increase();
        }
	}

    public void SetComboCount(int p_Count)
    {
        comboCount = p_Count;
        comboCountText = comboCount.ToString();
        if(comboCount == 0)
        {
            if (comboLabel.gameObject.activeInHierarchy == true)
                comboLabel.gameObject.SetActive(false);
            
        }
        else if(comboCount > 0)
        {
            if(comboLabel.gameObject.activeInHierarchy == false)
                comboLabel.gameObject.SetActive(true);
        }
        
    }

    public void Increase()
    {
        SetComboCount(comboCount+1);
    }

}
