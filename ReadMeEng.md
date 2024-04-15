# SDReactorUnity
Use Stable Diffusion in Unity (Use Extensions **Reactor**)

## Overview
**Original Project** : https://github.com/dobrado76/Stable-Diffusion-Unity-Integration   
+ _I only made **StableDiffusionReactor.cs** and add class for **Reactor API**(in SDsetting.cs)_
+ Please check _**Auotomatic1111 WebUI API Documentation**_ and _**Reactor API Documentation**_ and _**Original Project Page**_

## Preparation
1. Stable Diffusion(cloud server or local) URL and install extension **Reactor**
2. Source image and Target image
> Setting file and Scripts(except Reactor scripts) are same with **Original** project


## How to use Stable Diffusion in Unity
1. Type your SD URL in `setting file`
2. Add script : SD t2i or SD i2i or SD Reactor
3. Use Editor Mode or Play Mode

## How to use Reactor
1. Add your sorce iamge(use only face) and target image(result) in this Unity Project
2. Type images path in script
> Check out the **Reacotor git page** for good results
## Known Issue
1. In **SDSetrings.cs**, You must modify  ```public string face_restorer = "1"``` to ```public string face_restorer = "CodeFormer"```
2. Communication is **NOT** Async. `HTTPWebRequest` should probably be replaced with `UnityWebRequest` (in StableDiffusionReactor, t2i, i2i.cs)
***

+ Source image(left) and Target image(right)    
<img src="https://github.com/WooChan-Noh/SDReactorUnity/assets/103042258/2ea3ff9f-220b-4f8a-9690-01a662539aee" width="256" height="256"/><img src="https://github.com/WooChan-Noh/SDReactorUnity/assets/103042258/40b9ea93-7fef-4f03-963e-546e71130fe7" width="256" height="256"/>
+ result
![2](https://github.com/WooChan-Noh/SDReactorUnity/assets/103042258/22983f1b-eae9-4afd-bbad-37513e36b500)
