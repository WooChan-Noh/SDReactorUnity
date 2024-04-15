# SDReactorUnity
UnityでStable Diffusionを使用します。（extension : **リアクタ**）

## Overview
[参考したオリジナルプロジェクト](https://github.com/dobrado76/Stable-Diffusion-Unity-Integration)
+ 上のプロジェクトで下記の部分だけ変更しました。
  + SDsettiong.csファイルで**Reactor API**コードを追加しました。
  + **StableDiffusionReactor.cs**スクリプトを追加しました。
+ **Auotomatic1111 WebUI API Documentation**とR**eactor API Documentation**と**オリジナルプロジェクト**を必ず確認して使い方を覚えてから使ってください。

## Preparation
1. ローカルやサーバーにインストールされたStable DiffusionのURL(Reactorもインストールしておく)
2. 合成に使うソースイメージとターゲットイメージ
> 上の2つを除いては、元のプロジェクトの使い方と同じです。


## How to use Stable Diffusion in Unity
1. setting fileに自分のStable DiffusionのURLを入力します。
2. 目的に応じて`SD t2i`または`SD i2i`または`SD Reactor`スクリプトを追加して使用することができます。
3. エディターモードまたはプレイモードで使用します。


## How to use Reactor
1. ソースイメージ(顔のみ使用)とターゲットイメージ(背景イメージ)をUnityプロジェクトに追加します。
2. SD Reactorスクリプトがあるインスペクターで追加したイメージのパスを入力します。
> **Reacotorのgitページ**で使い方を参考にすると良い結果が出せます


## Known Issue
1. SDSetrings.csで、`public string face_restorer = "1"`と書いてある部分を`public string face_restorer = "CodeFormer"`に変更する必要があります。
2. 元のプロジェクトは非同期方式で進行されません。`HTTPWebRequest`で書かれているコードを`UnityWebRequest`に変更する必要があります。 (StableDiffusionReactor, t2i, i2i.cs)
***

+ Source image(left) and Target image(right)    
<img src="https://github.com/WooChan-Noh/SDReactorUnity/assets/103042258/2ea3ff9f-220b-4f8a-9690-01a662539aee" width="256" height="256"/><img src="https://github.com/WooChan-Noh/SDReactorUnity/assets/103042258/40b9ea93-7fef-4f03-963e-546e71130fe7" width="256" height="256"/>
+ result
![2](https://github.com/WooChan-Noh/SDReactorUnity/assets/103042258/22983f1b-eae9-4afd-bbad-37513e36b500)
