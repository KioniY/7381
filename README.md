## Title
TimeDrift: Echoes of Eternity - Learning History in VR for dyslexics

## Overview
Our product prototype is a VR educational game designed to help users (especially dyslexic users) learn history in an immersive and engaging way. By harnessing the power of VR technology, we provide users with an interactive experience that reduces reliance on text and makes learning more accessible. The game places users in historically accurate virtual environments, allowing them to explore ancient civilisations such as Rome through visual and auditory storytelling with minimal text. This approach helps dyslexic users engage with historical content in a more intuitive and relaxed way, utilising visual cues and interactions as an alternative to traditional reading methods.
  
## Installation and Setup
### Open via Unity oculus simulator
#### 1. Set Up the Project in Unity
   - Open Unity Hub.
   - Click on **Add** and navigate to the project folder on your desktop (or the location where the project is stored).
   - Once the project is added, open it in Unity.
#### 2. Active simulator
  - Select **Oculus**
  - Switch simulator to **Active**

### Open via VR headset
There are two ways to open our project in VR device:
#### 1. Set Up the Project in Unity
   - Open Unity Hub.
   - Click on **Add** and navigate to the project folder on your desktop (or the location where the project is stored).
   - Once the project is added, open it in Unity.
#### 2. Link VR Headset (Meta Quest)
   - Connect VR headset to computer
   - allow USB connect in your VR headset   
#### 3. Build and Run the Project
   - In Unity, open **Oculus > OVR Build and Run**.     
#### 4. Put on the Headset
   - Once the build is completed, put on the Meta Quest headset to experience the VR application.
   - Use the controllers to navigate and interact within the VR environment.

#### 1. Upload apk file to Meta Quest Link or SideQuest 
#### 2. Install APK file from folder on computer
#### 3. Click the filter dropdown list on top right
  - Find app inside Quest 2's APP library
  - Select Unknown Sources

## Features
  ### 1. Move via Controller
  - right and left sensings to move and turn
  ### 2. Grab interaction
  - using Grib button on the controller to grab things
  ### 3. Target-Based Exploration
  - Interactive targets that guide players through the environment by appearing sequentially as players approach.
  ### 4. Real-Time Feedback
  - Visual and sound feedback when interacting with objects, providing immersive user interaction.
    
## Code Structure
In this section, we provide an overview of the main scripts used in the project and their respective functions. Each script contributes to the core gameplay mechanics and interactions.
### 1. **Main Scene**
   - **Location**: [Main Scene](https://github.com/KioniY/7381/tree/main/Assets/Scenes)
   - **Description**: This folder contains the main scene of the VR project. The scene serves as the primary environment where users interact with targets, objects, and experience the core gameplay mechanics, including video playback and guided exploration.

### 2. **AudioAndCameraController.cs**
   - **Location**: [AudioAndCameraController.cs](https://github.com/KioniY/7381/blob/main/Assets/C%23/AudioAndCameraController.cs)
   - **Description**: Manages audio playback and camera movements, including transitions triggered by player actions or environmental events, ensuring smooth camera transitions and immersive sound effects.

### 3. **HandControllerMovement.cs**
   - **Location**: [HandControllerMovement.cs](https://github.com/KioniY/7381/blob/main/Assets/C%23/HandControllerMovement.cs)
   - **Description**: Manages player movement and rotation using the VR controller's thumbstick. The script moves the camera rig based on the head's forward direction and allows for rotational control via the secondary thumbstick.

### 4. **HornProximity.cs**
   - **Location**: [HornProximity.cs](https://github.com/KioniY/7381/blob/main/Assets/C%23/HornProximity.cs)
   - **Description**: Plays and stops a horn sound based on the proximity of the player's left or right hand to the horn object. The sound is triggered when either hand is within a specified distance and stops when both hands move
     
### 5. **SceneController.cs**
   - **Location**: [SceneController.cs](https://github.com/KioniY/7381/blob/main/Assets/C%23/SceneController.cs)
   - **Description**: Manages the display and fade-out of a whiteboard UI, handles video playback, and controls camera movement and rotation. The script fades out the whiteboard and video after playback ends, then moves and rotates the camera before returning it to its original position.

### 6. **TargetGuide.cs**
   - **Location**: [TargetGuide.cs](https://github.com/KioniY/7381/blob/main/Assets/C%23/TargetGuide.cs)
   - **Description**: Manages target-based guidance, detecting the proximity of the player's hands to guide them through sequential targets. At the fourth target, the script plays a video on a plane, which hides after the video finishes playing.

### 7. **DialogueTrigger.cs**
   - **Location**: [DialogueTrigger.cs](https://github.com/KioniY/7381/blob/main/Assets/NPC/NPC2/DialogueTrigger.cs)
   - **Description**: Triggers NPC dialogue when the player's hand approaches a specified target within a set distance. The dialogue is triggered sequentially, with a second line of dialogue from another NPC following after a delay.

## Authors
- **Qihong Yang** - [GitHub Profile](https://github.com/KioniY)
- **Xinyi Liao** - [GitHub Profile](https://github.com/lxy02230423)
- **Xinyue Li** - [GitHub Profile](https://github.com/XanaOvO)
- **Shiyue Zhang** - [GitHub Profile](https://github.com/candyshiyue)
- **Kirana Alivia** - [GitHub Profile](https://github.com/kiranaalivia)
- **Xiya Xie** - [GitHub Profile](https://github.com/s4833900)

## Acknowledgments
- Thanks to the Unity community for providing helpful tutorials.
- Inspired by [Assassin's Creed](https://www.ubisoft.com) for its interactive historical storytelling.
- Influenced by [Final Fantasy XIV](https://www.finalfantasyxiv.com) for its item fragments and rewards system.
- Inspired by [Dark Souls 3](https://www.bandainamcoent.com) for its exploration mechanics and fragmented storytelling approach.
- Background music inspired by [Civilization VI](https://civilization.com/), enhancing the immersive atmosphere of the game.



