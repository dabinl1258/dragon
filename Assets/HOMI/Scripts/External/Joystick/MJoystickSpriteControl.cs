using UnityEngine;
using System.Collections;

public class MJoystickSpriteControl : MonoBehaviour
{
    /// <summary>
    /// 터치 패드의 배경을 가져오는 UiSprite 입니다.
    /// </summary>
    public UISprite BackGround_Sprite = null;

    /// <summary>
    /// 터치 패드의 움직일 바를 가져오는 UISprite 입니다.
    /// </summary>
    public UISprite Object_Sprite = null;

    /// <summary>
    /// 처음 값을 초기화 하는 함수 입니다.
    /// </summary>
    /// <param name="localPos"> 초기화 할 값 </param>
    public void InitPosition(Vector3 localPos)
    {
        BackGround_Sprite.transform.localPosition = localPos;
        Object_Sprite.transform.localPosition = localPos;
    }

    /// <summary>
    /// 오브젝트를 업데이트 하는 함수 입니다.
    /// </summary>
    /// <param name="localPos"> 초기화 할 값 </param>
    public void UpdateObject(Vector3 localPos)
    {
        Object_Sprite.transform.localPosition = localPos;
    }

    /// <summary>
    /// 배경을 업데이트 하는 함수 입니다.
    /// </summary>
    /// <param name="localPos"> 초기화 할 값 </param>
    public void UpdateBackGround(Vector3 localPos)
    {
        BackGround_Sprite.transform.localPosition = localPos;
    }

    /// <summary>
    /// 오브젝트 위치를 초기화 하는 함수입니다.
    /// </summary>
    /// <param name="localPos"> 초기화 할 값 </param>
    public void ResetObject(Vector3 localPos)
    {
        Object_Sprite.transform.localPosition = localPos;
    }

    /// <summary>
    /// 오브젝트의 색을 설정합니다.
    /// </summary>
    /// <param name="col"></param>
    public void SetColor(Color col)
    {
        BackGround_Sprite.color = col;
        Object_Sprite.color = col;
    }
}
