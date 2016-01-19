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
    public Color startColor;
    public Color dColor;
    public float distance  = 0.5f;// 잔상 사이의 최소 거리
    Vector3 beforePos; // 이전에 잔상이 출현한 포지션

    
    List<SpriteRenderer> spritePool = new List<SpriteRenderer>();
    List<Texture2D> text2dPool = new List<Texture2D>();

    #region  Line Render
    private LineRenderer lineRender;
    public Transform lineRenderPos;
    private Color endColor;
    public Color endColorD;
    public Color endColorDefalut;
    private bool notSetPosition = true ;
    #endregion 
    int  Request()
    {
        for (int i = 0; i < spritePool.Count; i++)
        {
            if (spritePool[i].color.a <= 0.0f)
            {
                spritePool[i].color = startColor;
                return i;
            }
        }
        SpriteRenderer temp = (Instantiate(spritePre) as GameObject).GetComponent<SpriteRenderer>() ;
        temp.color = startColor;
        spritePool.Add(temp);
        texture2d = new Texture2D(x, y, TextureFormat.ARGB32, false);
        text2dPool.Add(texture2d);
        return spritePool.Count -1 ;
    }

    void Add()
    {

        int index = Request();
        SpriteRenderer temp = spritePool[ index];
        texture2d = text2dPool[index];
        camera.targetTexture = rTex;
        camera.Render();
        RenderTexture.active = rTex;
        texture2d.ReadPixels(new Rect(0, 0, x, y), 0, 0);
        texture2d.Apply();
        
        RenderTexture.active = null;
        Sprite spr = Sprite.Create(texture2d, new Rect(0, 0, x, y), Vector2.one);

        
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
        lineRender = GetComponent<LineRenderer>();
        

        transform.parent = transform.parent.parent;
        
	}
	


	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(bornPos.position.x, bornPos.position.y, transform.position.z);
        Timer();
        for (int i = 0; i < spritePool.Count; i++)
        {
            spritePool[i].color -= dColor * Time.deltaTime;
        }
        
        endColor -= endColorD * Time.deltaTime;
        
        if(notSetPosition)
        {
            endColor -= endColorD * Time.deltaTime * 5.0f;
        }else 
            lineRender.SetPosition(1, lineRenderPos.position);
        lineRender.SetColors(Color.clear, endColor);
	}

    public void Line(Vector3 _pos)
    {
        endColor = endColorDefalut;
        lineRender.SetPosition(0, _pos);
        notSetPosition = false;
    }

    public void NotSetPos()
    {
        notSetPosition = true;
    }
}
