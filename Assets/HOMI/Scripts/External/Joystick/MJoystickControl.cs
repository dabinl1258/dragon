using UnityEngine;
using System.Collections;

public class MJoystickControl : MonoBehaviour
{
    /// <summary>
    /// 조이스틱이 보여질 좌표 카메라
    /// </summary>
    public Camera JoystickTarget_Cam = null;

    /// <summary>
    /// 전체 조이스틱의 위치
    /// </summary>
    [HideInInspector]
    public Vector3 JoystickControl_Vec3;

    /// <summary>
    /// 조이스틱 바의 위치
    /// </summary>
    [HideInInspector]
    public Vector3 JoystickControler_Vec3;
    Vector3 TempJoystickControler_Vec3;

    /// <summary>
    /// 전체 조이스틱 충돌영역의 크기
    /// </summary>
    public Vector2 JoystickControl_Rect;

    /// <summary>
    /// 조이스틱 바의 충돌영역의 크기
    /// </summary>
    public Vector2 JoystickControler_Rect;

    /// <summary>
    /// 조이스틱 바가 최대로 나갈 수 있는 거리
    /// </summary>
    public float   fMaximumDistance = 0.0f;

    [HideInInspector]
    public float   fDistance = 0.0f;                // 거리
    [HideInInspector]
    public float   fDegree = 0.0f;                  // 디그리 각도
    [HideInInspector]
    public float   fRadius = 0.0f;                  // 라디안 각도

    [HideInInspector]
    public bool    bDown = false;                   // 눌렸는가? = true 안눌렸는가? = false

    public bool    bJoystickHandle = false;         // 조이스틱을 잡고 나서 움직여야 조이스틱 이동 가능 = true
                                                    // 조이스틱을 안잡고 다른 곳을 눌러도 조이스틱이 자동 이동 = false

    /// <summary>
    /// 기본 초기화
    /// </summary>
                                                        
    public void InitializeControler()
    {
        JoystickControl_Vec3 = transform.localPosition;
    }

    /// <summary>
    /// 사용시 초기화 해주는 구문들
    /// </summary>
    void Reset()
    {
        JoystickControler_Vec3 = new Vector3(0, 0);
        fDistance = 0.0f;
        fDegree = 0.0f;
        fRadius = 0.0f;  
    }

    void Start()
    {
        InitializeControler();
        Reset();
    }

    void Update()
    {
        // 마우스 값 받아오는 부분
        TempJoystickControler_Vec3 = JoystickTarget_Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y)) * 240 - JoystickControl_Vec3;

        if(!bDown)
        {
            // 터치 포인트 체크
            // 누르는 중 일시 바깥 영역 충돌 체크는 되지 않는다
            if (TempJoystickControler_Vec3.x <= -(JoystickControl_Rect.x / 2) || TempJoystickControler_Vec3.y <= -(JoystickControl_Rect.y / 2)
            || TempJoystickControler_Vec3.x >= JoystickControl_Rect.x / 2 || TempJoystickControler_Vec3.y >= JoystickControl_Rect.y / 2)
                return;
        }


        if(Input.GetMouseButtonDown(0))
        {
            // 조이스틱을 꼭 눌러서 움직이지 않아도 될 경우에는 영역에만 손이 가 있으면 인식
            if (bJoystickHandle == false)
                bDown = true;
            // 조이스틱을 꼭 눌러서 움직여야 할 경우에는 조이스틱 영역 충돌 체크
            else
            {
                if(TempJoystickControler_Vec3.x >= -(JoystickControler_Rect.x / 2)
                && TempJoystickControler_Vec3.y >= -(JoystickControler_Rect.y / 2)
                && TempJoystickControler_Vec3.x <= (JoystickControler_Rect.x / 2)
                && TempJoystickControler_Vec3.y <= (JoystickControler_Rect.y / 2) )
                {
                    bDown = true;
                }
            }
        }

        // 손을 땟을 때
        if(Input.GetMouseButtonUp(0))
        {
            Reset();
            bDown = false;
        }

        // 손을 누르고 있을때
        if(bDown)
        {
            // 각도 및 거리 연산
            fDistance = Vector3.Distance(new Vector3(0, 0, 0), TempJoystickControler_Vec3);
            fRadius = (Mathf.Atan2(TempJoystickControler_Vec3.y, TempJoystickControler_Vec3.x));
            fDegree = 180 - fRadius * (180 / Mathf.PI);

            // 영역보다 작을 경우 
            if (fMaximumDistance > fDistance)
            {
                JoystickControler_Vec3 = TempJoystickControler_Vec3;
            }
            // 영역보다 클 경우 최대의 거리에서 각도만 변경한다
            else
            {
                float fValue = fMaximumDistance / fDistance;

                JoystickControler_Vec3 = new Vector3(TempJoystickControler_Vec3.x * fValue, TempJoystickControler_Vec3.y * fValue);
            }
        }
    }

    /// <summary>
    /// 각도를 가져오는 함수
    /// </summary>
    /// <returns> 0 ~ 360 도를 반환 합니다. </returns>
    public int GetAngle()
    {
        return (int)fDegree;
    }

    /// <summary>
    /// 힘 세기를 가져오는 함수
    /// </summary>
    /// <returns> 0.0 ~ 1.0 을 반환함 </returns>
    public float GetPower()
    {
        if (fMaximumDistance > fDistance)
            return 1.0f * (fDistance / fMaximumDistance);
        else
            return 1.0f;
    }
}
