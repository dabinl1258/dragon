using UnityEngine;
using System.Collections;

public class DFlashingEffect : MonoBehaviour {
    public Material[] material = null;
    public SkinnedMeshRenderer meshRenderer = null;
    public float timerMax =0.3f;
    Material[] setMaterial = new Material[1];
    int currentMaterial = 0; // 0 번 마테리얼이 기본
    bool enAble = false;
    float timer = 0;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(enAble )
        {
            EnAbleUpdate();
        }
	}

    void EnAbleUpdate()
    {
        if(Time.time > timer + timerMax)
        {
            timer = Time.time;
            ChangeMaterial();
        }
    }


    void ChangeMaterial()
    {
        currentMaterial = (currentMaterial + 1) % 2;
        setMaterial[0] = material[currentMaterial];
        meshRenderer.materials = setMaterial;
    }

    public void Reset()
    {
        currentMaterial = 0;
        print(currentMaterial);
        setMaterial[0] = material[currentMaterial];
        meshRenderer.materials = setMaterial;
        enAble = false;
    }

    public void EnableEffect()
    {
        enAble = true;
    }
}
