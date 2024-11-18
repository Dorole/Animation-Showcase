# Animation-Showcase
A demo project where I put together stuff I learn about animation in Unity.

### I - The Basics 
This part was all about deep diving into concepts I already knew the basics of, but have never bothered to take a closer, more detailed look.<br>
I have created a basic scene with one character and downloaded some animations from Mixamo. I learned and/or re-learned about:
* humanoid rigs
* troubleshooting humanoid rigs
* importing animations, adjusting the properties and customizing them
* moving the character via root motion
* setting up blend trees and blending animations via code

#### Cinemachine
It's not the main focus, but I also explore this package in more detail whenever opportunity presents itself.<br>
For now, the camera's pitch and yaw are controlled by a pointer (mouse/gamepad). The camera follows the player and it's values are taken into account when calculating the player's rotation.

[See demo](https://1drv.ms/v/s!AiJtXPPGXKuz0ACJspNvIsaDYmZS?e=HvOv8x)

### II - Interactions Basics
This part was more about setting up the command logic for future work with animations. <br>
Currently the only available interaction is sitting. The player can click on the chair and the character will walk up to it, rotate appropriately and sit on it.<br>
There is still work to be done on the logic, but it is good enough for now. I will adjust it as I go. <br>
I use NavMesh for automatic movement and input + root motion for player-controlled movement.

[See demo](https://1drv.ms/v/s!AiJtXPPGXKuz0Di7zNtBdvOUUbm8?e=Dv2ten)


