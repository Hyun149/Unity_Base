# 🧱 Unity Base Structure Package

범용 Unity 프로젝트에 바로 적용 가능한 **구조화된 개발 유틸리티 패키지**입니다.  
재사용성과 유지보수를 고려하여 싱글톤, 이벤트 시스템, 오브젝트 풀링, 씬 전환 관리 등을 **모듈화**하여 제공합니다.

---

## 📌 주요 목적

- 팀 프로젝트에서 반복적으로 사용하는 기능들을 사전 패키지화
- **유지보수성과 확장성**을 고려한 구조 정립
- 유니티 초/중급 개발자가 구조 설계 없이도 빠르게 시작할 수 있도록 구성

---

## 🚀 포함 기능

| 기능 항목 | 설명 |
|-----------|------|
| `Singleton<T>` | MonoBehaviour 기반 싱글톤 구조. 자동 생성, 중복 제거, DontDestroyOnLoad 지원 |
| `EventManager` | 문자열 키 기반 이벤트 등록/호출/해제 지원. 전역 접근 가능한 싱글톤 |
| `SceneLoader` | Enum 기반 씬 전환. 문자열 하드코딩 제거, 유지보수성 향상 |
| `ObjectPool<T>` | 제네릭 기반 오브젝트 풀링 시스템. 재사용 가능한 GameObject 관리 |
| `ObjectPoolManager` | 다양한 타입의 풀을 string 키로 중앙 관리하는 풀 매니저 |
| `GameManager` | 게임 전체 흐름 관리. GameState/SceneType Enum 사용 |
| `IPoolable` | 풀링 오브젝트가 반드시 구현해야 하는 인터페이스 (OnSpawn / OnDespawn) |

---

## 🗂️ 디렉토리 구조

📦 Scripts
┣ 📂 Patterns # 재사용 가능한 유틸리티 패턴 모음
┃ ┣ 📜 Singleton.cs
┃ ┣ 📜 EventManager.cs
┃ ┗ 📜 GameManager.cs
┣ 📂 SceneManagement # 씬 전환 관련 스크립트
┃ ┣ 📜 SceneType.cs
┃ ┣ 📜 SceneLoader.cs
┃ ┗ 📜 GameState.cs
┣ 📂 Pooling # 오브젝트 풀링 시스템
┃ ┣ 📜 IPoolable.cs
┃ ┣ 📜 ObjectPool.cs
┃ ┗ 📜 ObjectPoolManager.cs
┗ 📜 README.md


---

## 💡 사용 예시

### 📍 싱글톤 접근
```csharp
GameManager.Instance.QuitGame();

📍 이벤트 등록/호출
EventManager.StartListening("OnPuzzleSolved", SomeMethod);
EventManager.TriggerEvent("OnPuzzleSolved");

📍 씬 전환
SceneLoader.Load(SceneType.MainScene);

📍 오브젝트 풀 사용
ObjectPoolManager.Instance.CreatePool<Bullet>("Bullet", bulletPrefab, 10);
var bullet = ObjectPoolManager.Instance.Spawn("Bullet");
ObjectPoolManager.Instance.Despawn("Bullet", bullet);

⚠️ 주의사항
- manifest.json, ProjectSettings.asset 변경 사항이 포함되어 있으니, 기존 프로젝트에 통합 시 덮어쓰기 주의
- SceneType.cs에 Enum을 추가할 경우 SceneLoader.cs의 sceneMap도 함께 수정 필요
- IPoolable 인터페이스를 상속하지 않은 오브젝트는 풀링에 사용할 수 없습니다.

📄 라이선스
MIT License
본 구조 패키지는 상업적/비상업적 프로젝트 모두에서 자유롭게 사용할 수 있습니다.
사용 시 출처 표기는 선택 사항입니다.
