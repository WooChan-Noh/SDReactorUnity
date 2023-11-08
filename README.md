# SDReactorUnity
Use Stable Diffusion in Unity (Use Extensions "Reactor")


**Original Project** : https://github.com/dobrado76/Stable-Diffusion-Unity-Integration   
I only made Reactor Scripts and Setting


If you use cloud server, Please check _**Auotomatic1111 Web UI API Documentation**_ and _**Reactor API Documentation**_ and _**Original Project Page**_

### Need list to use this project in your custom scene
1. Stable Diffusion(cloud server or local) URL and install extension "Reactor"
2. Setting your SD API mode(check webUI) 
3. Setting file _(Assets/Settings/)_
4. Scripts folder
5. Your custom album folder
> Setting file and Scripts(except Reactor scripts) are same with **Original** project


### How to use in your custom scene 
1. Type your SD URL in setting file
2. Make configuration GameObject and connect SD Configuration Script : This object load your SD Model and Sampler (Click List Models button in inspector)
3. Make Image object
4. Add script : SD t2i or SD i2i or SD Reactor
5. Use Editor Mode or Play Mode


### How to use Reactor
1. Add your sorce iamge(use only face) and target image(result)
2. Type Images Path
---

+ source
![source](https://github.com/WooChan-Noh/SDReactorUnity/assets/103042258/2ea3ff9f-220b-4f8a-9690-01a662539aee)
+ target
![target](https://github.com/WooChan-Noh/SDReactorUnity/assets/103042258/40b9ea93-7fef-4f03-963e-546e71130fe7)
+ result
![2](https://github.com/WooChan-Noh/SDReactorUnity/assets/103042258/22983f1b-eae9-4afd-bbad-37513e36b500)
