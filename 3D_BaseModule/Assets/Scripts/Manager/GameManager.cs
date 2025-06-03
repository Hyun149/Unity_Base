using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

/// <summary>
/// <b>게임 전체의 흐름과 상태를 관리하는 핵심 매니저 클래스입니다.</b><br/>
/// - 싱글톤(Singleton)으로 구성되어 전역에서 접근 가능합니다.<br/>
/// - 게임 상태 전환(타이틀, 인게임, 일시정지 등)을 관리합니다.<br/>
/// - UI 및 씬 매니저와의 연결 지점 역할도 수행할 수 있습니다.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// 현재 게임의 상태를 나타냅니다.<br/>
    /// 초기 상태는 None으로 설정되며, 이후 Title → InGame 등으로 전환됩니다.
    /// </summary>
    public GameState CurrentState { get; private set; } = GameState.None;

    /// <summary>
    /// 게임 시작 시 호출되는 Unity 이벤트 함수입니다.<br/>
    /// 초기 게임 상태를 Title로 설정하여 타이틀 화면 진입 흐름을 시작합니다.
    /// </summary>
    private void Start()
    {
        ChangeState(GameState.Title);
    }

    /// <summary>
    /// 게임 상태를 새 상태로 전환합니다.<br/>
    /// 중복 전환을 방지하며, 상태별 로직을 스위치로 구분하여 실행합니다.
    /// </summary>
    /// <param name="newState">전환하고자 하는 게임 상태</param>
    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        Debug.Log($"[GameManager] 상태 전환됨 -> {newState}");

        switch (newState)
        {
            case GameState.Title:
                // 타이틀 상태 초기화 처리 (예: 타이틀 UI 활성화, 배경음악 재생 등)
                break;
            case GameState.InGame:
                // 인게임 로직 시작 처리
                break;
            /* case GameState.Pause:
                // 일시정지 처리: 게임 시간 정지
                Time.timeScale = 0f;
                break;

            case GameState.Resume:
                // 일시정지 해제: 게임 시간 재개
                Time.timeScale = 1f;
                break;

            case GameState.Clear:
                // 게임 클리어 처리 (예: 클리어 연출, 결과창 표시 등)
                break; */
        }
    }

    public void QuitGame()
    {
        Debug.Log("[GameManager] 게임 종료 요청됨");

# if UNITY_EDITOR
        // Unity 에디터 상에서 실행 중인 경우 에디터 모드 종료
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 애플리케이션에서는 실제 게임 종료
        Application.Quit();
#endif
    }
}
