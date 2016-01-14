using UnityEngine;
using System.Collections;

/// <summary>
/// 주의:
///    씬이름과 똑같이 적어줘야합니다^^
///    ex) 씬이름이 1_LogoScene ----> E_1_LogoScene 이렇게 붙여주세요^^
/// </summary>
public enum E_H_RESOURCELOAD
{
    E_0_Common,             //!< PrefabMng안에 있는 전역 Resource에서 읽어와요(경고창 같은거^^)
    E_0_TestSceneScene,
    E_3_IngameScene,
    E_1_TestSceneScene,
    E_1_TestScene,
    E_5_InGameScene,
    E_0_LogoScene,
    E_1_MenuScene,
    E_MAX
}
