using UnityEngine;
using System.Collections;
using System.Collections.Generic;
private class DAfterImagePool
{
    public SpriteRenderer spriteRenderer;
    RenderTexture rTex;
    
}
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
    public Color startColor;
    public Color dColor;
    public float distance  = 0.5f;// 잔상 사이의 최소 거리
    Vector3 beforePos; // 이전에 잔상이 출현한 포지션

    #region  Line Render
    private LineRenderer lineRender;
    public Transform lineRenderPos;
    private Color endColor; 
    #endregion 
    List<SpriteRenderer> pool = new List<SpriteRenderer>();
    


    SpriteRenderer Request()
    {
        for(int i =0 ; i< pool.Count ; i++)
        {
            if(pool[i].color.a <= 0.0f)
            {
                pool[i].color = startColor;
                return pool[i];
            }
        }
        SpriteRenderer temp = (Instantiate(spritePre) as GameObject).GetComponent<SpriteRenderer>() ;
        temp.color = startColor;
        pool.Add(temp);
        return temp;
    }

    void Add()
    {
        
        
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
        beforePos = temp.transform.position;
    }

    void Timer()
    {
        if (time + timerMax < Time.time &&
            Vector3.Distance(beforePos ,bornPos.position + offset) > distance)
        {
            
            time = Time.time;
            Add();
        }
    }

	// Use this for initialization
	void Start () {
        rTex = new RenderTexture(x, y, 10);
        texture2d = new Texture2D(x, y, TextureFormat.ARGB32, false);
        lineRender = GetComponent<LineRenderer>();
        transform.parent = transform.parent.parent;
        
	}
	


	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(bornPos.position.x, bornPos.position.y, transform.position.z);
        Timer();
        for (int i = 0; i < pool.Count; i++)
        {
            pool[i].color -= dColor * Time.deltaTime;
        }
        lineRender.SetPosition(1, lineRenderPos.position);
        endColor -= dColor * Time.deltaTime;
        lineRender.SetColors(Color.clear, endColor);
	}

    public void Line(Vector3 _pos)
    {
        endColor = Color.white/2;
        lineRender.SetPosition(0, _pos);
    }
}
