
using UnityEngine;
using System.Collections;
using MHomiLibrary;

public class MJoystickMng : HSingleton<MJoystickMng>
{
    /// <summary>
    /// 조이스틱에 사용되는 Sprite를 관리하는 클래스 입니다.
    /// </summary>
    public MJoystickSpriteControl JoystickSpriteCtrl_Script = null;

    /// <summary>
    /// 조이스틱의 컨트롤 부분을 관리하는 클래스 입니다.
    /// </summary>
    public MJoystickControl JoystickCtrl_Script = null;

    /// <summary>
    /// 조이스틱의 이펙트 부분을 관리하는 클래스 입니다.
    /// </summary>
    public MJoystickEffectTech JoystickEffect_Script = null;

    void Start()
    {
        // 코루틴 업데이트
        StartCoroutine("UpdateCoroutine");
    }

    IEnumerator UpdateCoroutine()
    {
        while(true)
        {
            yield return null;

            // 이벤트 부분 먼저 업데이트 합니다.
            JoystickEffect_Script.HideEffect();
            JoystickEffect_Script.FocusingEffect();
            JoystickEffect_Script.MoveEffect();

            // 좌표 및 각도 업데이트 합니다.
            JoystickSpriteCtrl_Script.UpdateObject(JoystickCtrl_Script.JoystickControler_Vec3);
        }
    }

    /// <summary>
    /// 각도를 가져오는 함수
    /// </summary>
    /// <returns> 0 ~ 360 도를 반환 합니다. </returns>
    public int GetAngle()
    {
        return JoystickCtrl_Script.GetAngle();
    }

    /// <summary>
    /// 힘 세기를 가져오는 함수
    /// </summary>
    /// <returns> 0.0 ~ 1.0 을 반환함 </returns>
    public float GetPower()
    {
        return JoystickCtrl_Script.GetPower();
    } 
}
