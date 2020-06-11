# Gov24Crawler
정부24 토지대장 발급 자동화

## 개요
* 친구 요청으로 만듬. 토지의 공부면적과 소유자를 알아내는 것이 목적.
* 토지대장 1000개를 일일이 클릭해서 발급, 이 과정에만 며칠이 소요.
* 자동화 시 한개 발급에 대략 30초 소요, 시간도 줄이고 사람도 편해짐.

### Selenium, ChromeDriver 사용.
### Microsoft Excel 라이브러리 사용.
### 사이트 요구사항인 AnySign For PC 사전 설치 필요.

## 사용법
* 번지가 적힌 엑셀 파일을 선택 후, 세부사항을 파일에 맞게 적어줌
<img width="500" src="https://user-images.githubusercontent.com/59993347/84016795-c6a4ff00-a9b8-11ea-9e8e-a964bfce5436.png">

* 자신의 정부24 아이디와 비밀번호를 입력후, 작동 시작 
<img width="500" src="https://user-images.githubusercontent.com/59993347/84016861-dd4b5600-a9b8-11ea-942f-98a00e96d487.png">


## 엑셀 형식, 결과물
* 위의 예제에서 사용한 엑셀 파일 내용
<img width="500" src="https://user-images.githubusercontent.com/59993347/84016855-dae8fc00-a9b8-11ea-9e14-f9a3fc014312.png">

* 완료된 폴더, 토지대장 전체 스크린샷과 필요한 내용이 추출된 엑셀 파일이 저장됨.
<img width="500" src="https://user-images.githubusercontent.com/59993347/84016864-de7c8300-a9b8-11ea-9601-2096dd302382.png">

