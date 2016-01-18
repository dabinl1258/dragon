using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DAfterImage : MonoBehaviour {
    RenderTexture rTex;
    public GameObject spritePre = null;
    public Transform bornPos; // 생성하는 위치
    Texture2D texture2d;
    public int x = 100;
    public int y = 100;
    float time;
    public float timerMax = 0.3f;
    public Vector3 offset;

    List<SpriteRenderer> pool = new List<SpriteRenderer>();


    SpriteRenderer Request()
    {
        for(int i =0 ; i< pool.Count ; i++)
        {
            if(pool[i].color.a <= 0.0f)
            {
                pool[i].color = Color.white;
                return pool[i];
            }
        }
        SpriteRenderer temp = (Instantiate(spritePre) as GameObject).GetComponent<SpriteRenderer>() ;
        pool.Add(temp);
        return temp;
    }

    void Add()
    {
        rTex = new RenderTexture(x, y, 10);
        texture2d = new Texture2D(x, y, TextureFormat.ARGB32, false);
        camera.targetTexture = rTex;
        camera.Render();
        RenderTexture.active = rTex;
        texture2d.ReadPixels(new Rect(0, 0, x, y), 0, 0);
        texture2d.Apply();
        
        RenderTexture.active = null;
        Sprite spr = Sprite.Create(texture2d, new Rect(0, 0, x, y), Vector2.one);

        SpriteRenderer temp = Request();
        temp.sprite = Sprite.Create(texture2d, new Rect(0, 0, x, y), Vector2.one);
        temp.transform.position = bornPos.position + offset;
    }

    void Timer()
    {
        if (time + timerMax < Time.time)
        {
            time = Time.time;
            Add();
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Timer();
        for (int i = 0; i < pool.Count; i++)
        {
                pool[i].color -= new Color(0,0,0,Time.deltaTime );
        }
       
	}
}
