# SD-dev-portfolio

---

## Menos (C#) / Personal Project
Menos is a personal project that I've been working on during Spring 2026, and it is a Greek Murder Mystery Puzzle game. I worked on implementing the Journal UI System, where the player is able to take notes, check collected evidences, tag relevant suspects to each evidence, and check each character's relationship status with the main character. There are still features that need to be added, such as map and settings panel, and also bug fixes. Also I worked on writing the baseline and the events of the story, providing the foundation our narrative designer can work. 

[Video used for MAGIC Maker Application](https://drive.google.com/file/d/1HSTR84nmVCCXKuZ508zF2Bev0CGL8jr_/view?usp=drive_link)

---

## The Adventure of the Speckled Band (C#) / Academic Project
In this project, I tried to make a walk simulator into a game by integrating environmental storytelling where the player has to go through the environment and discover evidences as Dr. Watson, uncovering the secrets behind a mysterious death. The custom character controller script is not as perfect as the Unity character controller script, as it has some issues of tilting when colliding with the wall, but I learnt a lot while debugging this. Also I designed the mansion with two floors and two rooms, where evidences are hidden. Overall, this was not only an academic project but a passion project, but I do need to be better with scoping next time.

---

## Custom Game Engine (C++) / Personal Project
Unity and Unreal felt too comfortable, and since I want to know what is happening under-the-hood, I'm attempting to build a custom game engine from scratch in C++ to deepen my understanding of low-level systems programming and real-time application architecture. By this, I get to have full controls of the systems I'm working on!

- Building using the Win32 API and custom rendering loop(in-progress)
- Attempting to implement an component-based-arhitecture
- Focused on modularity and extendability for future physics and rendering systems
- Current milestone: Window Management, Creating Game Loop, Application and Game Object Class

[View Repository](https://github.com/SD0249/Demon-Engine)

---

## Sweet Dreams (C#) / Academic Project
A Team Project with an objective to make a Top-Down Shooter game with the given time frame, adapting to Game Development Processes. Handled Drawing Start and User Interface buttons, and implemented my first camera class, which leads to my passion to understand the matrices to better understand how camera works.

[View Repository](https://github.com/SD0249/Sweet-Dreams_SugarRush)

---

## Mistique Menu (C#) / Personal Project
An on-going project for purposes to stay in-touch with my friend during the summer. At the previous project, Sweet Dreams, I have developed a Camera Class that was tied to the screen of the viewport, which was a bad design choice. Here, I worked with my friend who handled the render logic which rescales what the camera sees, and I have handled the Camera portion to only show the portion of the world, *decoupling the camera from the screen size*. That way, we were able to make the game run in full screen on any device!

![TechnicalDemoGIF of Camera Class](src/ScrollZoomCamera.gif)

However, we had to import all the work to Unity, because the MonoGame 3.8.4 was having problems with its content builder. We haven't moved much content from the old repository to the new repository yet.

[View Repository](https://github.com/SD0249/MysticMenu)

---

## Plinko (Unity - C#) / Academic Project
Here, I wanted to utilized the skills I have learned from my 2D Animation and Assets Production Class. Besides some pegs and wall sprites, I have drawn all of the sprites including the bucket(FlyTrap) and the Fly(Chip). It was a nice project to get used to Unity's Component-based System, and seeing my own animation in result was rewarding!

[Play Game in Web!](https://igme-202-2251.github.io/202-work-SD0249/Project_01/)

---

## Scream Jam 2025(GameMaker/GML) - Ticket To Terror Station
I took the role of product management and main character artist in this Game Jam, as I wanted to try some of the other responsibilities in a team development environment. Doing the main character art was fun and rewarding, and I think we made a decent progress in a week using Game Maker to make the game!

[View Repository](https://github.com/SD0249/Ticket-To-Terror-Station/tree/New-Branch)

![MainCharacterWalk](src/WalkGIF.gif)
![MainCharacterClimb](src/ClimbGIF.gif)

---

## Algorithms & Systems (C#) / Academic Projects
A collection of algorithm and systems programming exercises exploring foundational CS topics.

Includes:
- Graph search algorithms (BFS, DFS)
- Sorting and Pathfinding implementations
- Data structure experiments (queues, stacks, trees)
- Designed for problem-solving and systems design understanding

Located in this repository's [Algorithms and Systems](AlgorithmsAndSystems) file

---

## Current Project Focus

**Cat Punch**
- For RITxKCG Game Jam. 
- Implementing the UI Menu Layout in Unity
- Making a 'Mouse' Entity in Unity with its basis on Autonomous Agents, 

**OpenGL**
- Learning the Graphics API with a goal to understand how graphics are generated
- Learning to use libraries such as GLFW and GLAD
- Located in this repository's [OpenGL](OpenGL) file

**Custom Game Engine**
- Implementing a component-based architecture to handle multiple game objects efficiently
- Working on input handling and basic rendering using Win32 API

---

*This Portfolio is continuously updated as new systems and features are developed.
