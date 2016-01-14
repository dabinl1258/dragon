using UnityEngine;
using System.Collections;
public class DTestExcel : MonoBehaviour {
    int index = 0;
    int BGnum = 7;
    GameObject atlas;
    GameObject atlasBG;
    UISprite sprite;
    UISprite bg;

    public GameObject uiRoot = null;

	// Use this for initialization
	void Start () {
        //atlas = Resources.Load("AtlasSpr/" + SpriteSingleton.I.sprite[index, 1] + "spr") as GameObject;
        //sprite = (Instantiate(atlas) as GameObject).GetComponent<UISprite>();
        //sprite.spriteName = SpriteSingleton.I.sprite[index, 2];
        //sprite.transform.localScale = new Vector3(float.Parse(SpriteSingleton.I.sprite[index, 3]),
        //   float.Parse(SpriteSingleton.I.sprite[index, 4]), float.Parse(SpriteSingleton.I.sprite[index,5]));
        Debug.Log("Start");
        for (int i =0 ; i < 16 ; i++)
        {
            CreateImage(i);
            Debug.Log(i);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //UISprite CreateByIndex(int _index) 
    //{
    //    UISprite sprite;
    //    Debug.Log("AtlasSpr/" + SpriteSingleton.I.Sprite[_index, 1] + "spr");
    //    atlas = Resources.Load("AtlasSpr/" + SpriteSingleton.I.Sprite[_index, 1] + "Spr") as GameObject;
    //
    //    sprite = (Instantiate(atlas) as GameObject).GetComponent<UISprite>();
    //    sprite.transform.parent = uiRoot.transform;
    //    sprite.spriteName = SpriteSingleton.I.Sprite[_index, 2];
    //    
    //    sprite.transform.localScale = Vector3.one;
    //    
    //    sprite.transform.localPosition = new Vector3(float.Parse(SpriteSingleton.I.Sprite[_index, 5]),
    //        float.Parse(SpriteSingleton.I.Sprite[_index, 6]), float.Parse(SpriteSingleton.I.Sprite[_index, 7]));
    //
    //            
    //    //sprite.localSize.Set(float.Parse(SpriteSingleton.I.Sprite[_index, 3]),
    //    //    float.Parse(SpriteSingleton.I.Sprite[_index, 4]));
    //    sprite.SetDimensions(int.Parse(SpriteSingleton.I.Sprite[_index, 3]),
    //        int.Parse(SpriteSingleton.I.Sprite[_index, 4]));
    //    return sprite;
    //}

    UISprite CreateImage(int _index)
    {
        for(int i=0;i<29;i++)
        {
            Debug.Log("AtlasSpr/" + SpriteSingleton.I.Sprite[i, i] + "spr");
            atlas = Resources.Load("AtlasSpr/" + SpriteSingleton.I.Sprite[1, i] + "spr") as GameObject;
         
            sprite = (Instantiate(atlas) as GameObject).GetComponent<UISprite>();
            sprite.transform.parent = uiRoot.transform;
            sprite.spriteName = SpriteSingleton.I.Sprite[2, i];
        
            sprite.transform.localScale = Vector3.one;
        
            sprite.transform.localPosition = new Vector3(float.Parse(SpriteSingleton.I.Sprite[5, i]),
                float.Parse(SpriteSingleton.I.Sprite[6, i]), float.Parse(SpriteSingleton.I.Sprite[7, i]));
        
            sprite.SetDimensions(int.Parse(SpriteSingleton.I.Sprite[3, i]),
                int.Parse(SpriteSingleton.I.Sprite[4, i]));
        }
        return sprite;
    }
}