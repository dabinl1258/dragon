using UnityEngine;
using System.Collections;

public class MJoystickEffectTech : MonoBehaviour
{
    /// <summary>
    /// 조이스틱에 사용되는 Sprite를 관리하는 클래스 입니다.
    /// </summary>
    public MJoystickSpriteControl JoystickSpriteCtrl_Script = null;

    /// <summary>
    /// 조이스틱의 컨트롤 부분을 관리하는 클래스 입니다.
    /// </summary>
    public MJoystickControl JoystickCtrl_Script = null;

    public bool bHideEffect = false;        // 해당 범위를 터치 안할시 안보이는 이벤트
    public bool bFocusingEffect = false;    // 해당 범위를 터치 안할시 흐리게 보이는 이벤트
    public bool bMoveEffect = false;        // 최대 범위 내 터치 시 터치 오브젝트가 그 위치에서 나타나는 이벤트

    bool bMoveChk = false;
    Vector3 tempVec;

    /// <summary>
    /// 숨기는 기능
    /// </summary>
    public void HideEffect()
    {
        if (bHideEffect == true)
        {
            if (JoystickCtrl_Script.bDown)
                JoystickSpriteCtrl_Script.SetColor(new Color(1, 1, 1, 1));
            else
                JoystickSpriteCtrl_Script.SetColor(new Color(1, 1, 1, 0));
        }
    }

    /// <summary>
    /// 움직이는 기능
    /// </summary>
    public void MoveEffect()
    {
        // 한번만 체크 가능하도록 수정
        if (bMoveEffect == true)
        {
            // 조이스틱을 누르지 않았을 때
            if (!JoystickCtrl_Script.bDown)
            {
                // 처음 위치로
                if (bMoveChk)
                {
                    transform.localPosition = tempVec;
                    JoystickCtrl_Script.InitializeControler();
                }

                bMoveChk = false;
                return;
            }

            // 조이스틱을 누르고 있을때 한번 기존 위치를 바꾸게 해준다
            if (bMoveChk == false)
            {
                tempVec = transform.localPosition;
                transform.localPosition = JoystickCtrl_Script.JoystickTarget_Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)) * 240;
                JoystickCtrl_Script.InitializeControler();
                bMoveChk = true;
            }
        }
    }

    /// <summary>
    /// 포커싱 될 시 연해지는 기능
    /// </summary>
    public void FocusingEffect()
    {
        if (bFocusingEffect == true)
        {
            if (JoystickCtrl_Script.bDown)
                JoystickSpriteCtrl_Script.SetColor(new Color(1, 1, 1, 1));
            else
                JoystickSpriteCtrl_Script.SetColor(new Color(1, 1, 1, 0.5f));
        }
    }
}