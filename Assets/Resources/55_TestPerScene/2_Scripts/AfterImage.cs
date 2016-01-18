using UnityEngine;
using System.Collections;

public class AfterImage : MonoBehaviour
{
	public RenderTexture rTex;
    public GameObject spritePre = null;
	public SpriteRenderer rend;
    Texture2D texture2d;
    public int x = 100;
    public int y = 100;
    
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
        GameObject temp = Instantiate(spritePre) as GameObject;
        temp.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture2d, new Rect(0, 0, x, y), Vector2.one);
        temp.transform.position = transform.position;
    }

    void Set()
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
        rend.sprite = spr;
    }
	void Start()
	{
		
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Set();
        }
    }

}


