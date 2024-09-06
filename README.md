# OneMonthTask
한 달 인턴 과제


https://github.com/user-attachments/assets/1f70a399-0386-4e59-b40f-592a1bac529b

- Canvas 하나만 두어 DrawCall 최소화
- Enemy마다 Canvas를 두지 않고 메인 Canvas에서 체력바가 Enemy 위치를 따라다니도록 하여 Cavas 개수 1개만 사용 -> DrawCall 최소화
- 오브젝트 풀링 기법 사용으로 오브젝트 재사용 -> GC가 빈번하게 호출되지 않도록 성능 최적화
- Enemy는 프리팹 하나만 사용하고, Animator만 바꿔서 사용 -> 리소스 재사용
- 각 Enemy마다 Sprite 크기가 달라서 Init할 때마다 콜라이더 크기를 Sprite크기에 맞추도록 설정
- 애니메이터에 Add Behaviour에 스크립트를 넣어서 애니메이션의 각 상태에 맞게 스크립트 실행
- CSV 내용 파싱하여 Enemy 데이터 설정
- Physics.Overlap 사용 적 감지 및 공격
- Physics.Overlap나 몬스터 스폰 위치를 보기 위해 에디터 상에서만 기즈모가 보이도록 하여 개발 편의성 증가
- 몬스터 체력이 0이 되면 다음 몬스터가 등장, 몬스터를 죽이지 못할 시 끝지점까지 이동하며 끝지점 도달시 다음 몬스터 등장
- 몬스터 선택시 팝업창 띄워 정보를 보여줌, 아무곳이나 클릭시 팝업창이 닫혀짐
- Aseprite 활용법 숙지함 -> 애니메이션 클립과 Sprite가 따로 되어있는 것이 아닌 묶음이어서 추출하여 사용
  
