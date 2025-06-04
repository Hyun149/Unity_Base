# 🧱 Unity Base Package Template

Unity 프로젝트의 구조화된 시작을 위한 범용 패키지입니다.  
반복되는 시스템 구현 없이 바로 기능 개발에 집중할 수 있도록  
**싱글톤 패턴, 이벤트 시스템, 씬 매니저, 오브젝트 풀링 시스템** 등  
게임 개발에서 자주 사용되는 구조를 미리 포함하고 있습니다.

---

## 📦 포함된 핵심 기능

### ✅ 범용 싱글톤 클래스 (`Singleton<T>`)
- `MonoBehaviour` 기반 싱글톤 구현
- 인스턴스 자동 생성 및 `DontDestroyOnLoad` 적용
- 종료 시점 안전 처리 (`isShuttingDown`)

```csharp
public class GameManager : Singleton<GameManager> { ... }
```

## ✅ 범용 이벤트 매니저 (EventManager)

- 문자열 키 기반으로 이벤트 등록/해제/호출
- 싱글톤 기반 어디서든 접근 가능
- 씬 전환 시 `ClearAllEvents()` 호출로 리스너 정리 가능

```csharp
EventManager.StartListening("OnPuzzleSolved", MyHandler);
EventManager.TriggerEvent("OnPuzzleSolved");
```

## ✅ Enum 기반 씬 매니저 (SceneLoader)

- `SceneType` 열거형으로 씬 전환 → 문자열 하드코딩 제거
- 유지보수와 리팩토링에 유리한 구조 제공

```csharp
SceneLoader.Load(SceneType.MainScene);
```

## ✅ 제네릭 오브젝트 풀링 시스템

- `ObjectPool<T>`: 프리팹 재사용을 위한 제네릭 풀 클래스
- `IPoolable`: 풀 오브젝트가 구현해야 할 인터페이스
    - `OnSpawn()`: 오브젝트가 풀에서 나올 때 호출
    - `OnDespawn()`: 오브젝트가 풀로 돌아갈 때 호출
- `ObjectPoolManager`: string 키 기반 풀 생성 및 관리

**사용 예시:**
```csharp
ObjectPoolManager.Instance.CreatePool("Explosion", explosionPrefab, 10);
var obj = ObjectPoolManager.Instance.Spawn<Explosion>("Explosion", pos, rot);
ObjectPoolManager.Instance.Despawn("Explosion", obj);
```

✅ 게임 상태 및 게임 매니저 (GameManager)
- GameState 열거형 기반 상태 제어
- Singleton<GameManager> 구조로 전역 상태 흐름 관리
- QuitGame() 메서드 포함 (에디터/빌드 분기 처리)

```csharp
GameManager.Instance.ChangeState(GameState.InGame);
```

---
🔧 적용 방법
1. 본 패키지를 Assets/에 통째로 복사
2. 필요에 따라 SceneType, GameState, 프리팹 등 커스터마이징
3. 각 매니저 클래스를 자유롭게 활용하여 프로젝트 구조 구성

---
💡 향후 확장 예시
- DOTween 기반 UI 애니메이션 모듈 추가
- 커스텀 Save/Load 시스템 (PlayerPrefs 또는 JSON 기반)
- FSM 기반 캐릭터 상태 시스템
- Input System 통합 매니저 등

---
📝 버전 정보
- Unity 버전: 2022.3.17f
- 패키지 상태: 초기 배포용 (기초 구조 세팅 중심)

---
📄 라이선스
MIT License
자유롭게 사용, 수정, 배포 가능하며 사용 시 출처 표기 권장
---
