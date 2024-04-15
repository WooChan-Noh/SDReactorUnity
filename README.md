[ReadMe - English](https://github.com/WooChan-Noh/SDReactorUnity/blob/main/ReadMeEng.md)     
[ReadMe - Japanese](https://github.com/WooChan-Noh/SDReactorUnity/blob/main/ReadMeJp.md)
# SDReactorUnity
유니티에서 스테이블 디퓨전의 extension - **Reactor**를 활용하는 프로젝트입니다. 
## Overview
[**참고한 원본 프로젝트**](https://github.com/dobrado76/Stable-Diffusion-Unity-Integration) 
+ 위의 프로젝트에서 다음과 같은 부분만 변경했습니다.
  + _SDsettiong.cs 파일에서 **Reactor API** 코드를 추가했습니다._
  + _**StableDiffusionReactor.cs**스크립트를 추가했습니다._
+ **Auotomatic1111의 웹 UI API문서**, **Reactor API 문서**, **원본 프로젝트 페이지**를 반드시 확인하고 사용법을 익힌 뒤 사용해주세요

## Preparation
1. 로컬, 혹은 서버에 설치된 스테이블 디퓨전의 URL (Reactor도 미리 설치) 
2. 합성에 사용할 원본 이미지와 타겟 이미지
> 위의 두 가지를 제외하고는 원본 프로젝트 사용법과 동일합니다.


## How to use Stable Diffusion in Unity
1.  `setting file` 에 본인의 스테이블 디퓨전 URL을 입력합니다.
2. 목적에 따라 SD t2i 또는 SD i2i 또는 SD Reactor 스크립트를 추가하여 사용할 수 있습니다.
3. 에디터 모드 또는 플레이 모드에서 사용합니다.

## How to use Reactor
1. 원본 이미지(_얼굴만 사용_)와 타겟 이미지(_결과물이 될 배경 이미지_)를 유니티 프로젝트에 추가합니다.
2. SD Reactor 스크립트 인스펙터에서 추가한 파일의 경로를 입력합니다.
> **Reacotor git 페이지**에서 사용법을 참고하면 좋은 결과물을 낼 수 있습니다.
## Known Issue
1. **SDSetrings.cs**에서,  ```public string face_restorer = "1"``` 라고 작성되어 있는 부분을 ```public string face_restorer = "CodeFormer"```로 변경해야 합니다.
2. 원본 프로젝트는 비동기방식으로 진행되지 않습니다. `HTTPWebRequest` 로 작성되어 있는 부분을 `UnityWebRequest` 로 변경해야 합니다. (StableDiffusionReactor, t2i, i2i.cs)
***

+ 원본 이미지(왼쪽) 과 타겟 이미지(오른쪽)    
<img src="https://github.com/WooChan-Noh/SDReactorUnity/assets/103042258/2ea3ff9f-220b-4f8a-9690-01a662539aee" width="256" height="256"/><img src="https://github.com/WooChan-Noh/SDReactorUnity/assets/103042258/40b9ea93-7fef-4f03-963e-546e71130fe7" width="256" height="256"/>
+ 결과 (고양이 이미지는 무시하세요)
![2](https://github.com/WooChan-Noh/SDReactorUnity/assets/103042258/22983f1b-eae9-4afd-bbad-37513e36b500)
