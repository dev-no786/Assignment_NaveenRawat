# Assignment_NaveenRawat
## Unity Coding Challenge - Outscal

 Snake 2d Game in Unity

![](https://i.imgur.com/UPz9Tu2.gif)

Simple Unity Game Demo 

# Table of Content

- [Setup](#setup)
- [Assets](#assets)
  * -[Sprites](#sprites)
  * -[Scripts](#scripts)
- [Core GamePlay Loop](https://giant.gfycat.com/GentleGenuineKob.webm)
## Setup

Firstly the project is made up of 2 Scenes

1. Scene 1 - Start  

  ![](https://i.imgur.com/bcX7s1d.png)




2. Scene 2 - Game

  ![](https://i.imgur.com/R5BUu73.png)


### Start Scene

![](https://i.imgur.com/2BHmHCU.png)

### Game Scene

![](https://i.imgur.com/XRJdOCi.png)

### Game Over Screen

![](https://i.imgur.com/7w4ypJE.png)
## Assets

### Sprites

**Snake**  
![](https://i.imgur.com/7UzGvy8.png)  
![](https://i.imgur.com/m0u4F94.png)  

Our **Food Prefab** Pick  
![](https://i.imgur.com/ceNVJgb.png)

**Tail Prefab**  
![](https://i.imgur.com/MvyN9NR.png)



### Scripts

#### TAGS.cs
* **enum PlayerDirection** - For storing and accessing the direction and metric of distance between intermediate nodes 

#### PlayerController.cs
This the script responsible for our snake movement and controls triggers when colliding with Walls or Pick ups.

It is attached to our main Player object "Snake" which is the one our player is controlling.

Objectives of this scripts are -
* Initialize our first set of head,body and tail nodes and Player Snake

  **InitSnakeNodes()** - Initializing the the nodes as an array of child rigidbodies (to control their motion in physics world) with head being very first node.
  
  **InitPlayer()** - Initializing the position of our player snake at the start by checking the current direction and changing the poistion of the original body and tail node the according to it and with respect to the head.
  
     **Player moves Right** 
     ![](https://i.imgur.com/ei06Jsy.png)
     
     
     **Player moves Up** 
     ![](https://i.imgur.com/F1T1pAY.png)
     
     **Player moves Down** 
     ![](https://i.imgur.com/NgMmHfX.png)
     
     **Player moves Left** 
     ![](https://i.imgur.com/7JjT19J.png)
     
 * Actual Movement of our Player Snake
   
   **delta_position** - _List_ of Vector2d XY coordiantes coressponding to direction enum by which the position/direction of our Head node will increment (where to move our Head  and by how much next time)
   
   **Move()** - primary function which uses the delta_position(i.e next XY position of head) to update Head node/main body position and using the previous Head postion we iterates through other nodes replacing [i] node position with the previous position of [i+1] node.
   When the snake eat a pick up the tail prefab gameobject gets instantiated at the last node position and is set as child to the Player Snake gameobject and added to list of nodes.
   
   **Check Movement Frequency & Force Move methods** - to control the frequency of the movement update of Player Snake by __keeping count of how much time has passed since a number of frame and then triggering Move()__ otherwise our Snake can be too fast or slow to handle but also we dont want our Player Snake to wait for some frames when user presses button to change the movement so we call ForceMove() to directly trigger Move()
   
   **Setting Player Direction** - with **PlayerInput.cs** we fetch input from Player axis with corresponding direction we compare the direction of own direction of Player Snake and move.
   
   **Triggering Collisions with Pick ups & GameOver** - food objects are tagged with "item" and colliding with one will update score and trigger Spawn method from the food spawn script to generate the next food item.
   
   Colliding with wall and tail prefab objects ("Wall" & "Tail" tag respectively) will stop our game display score and check if its a new highscore.
   
### PlayerInput.cs
Fetch user input and check the current axis of our PLayer Snake whether horizontal and vertical

The script is also attached to our Player Snake
  * Get value of the horizontal/vertical axis from **GetKeyboardInput()** and check if only single axis is enabled to avoid simultaneous motion in both axis resulting zigzag moving.
   
    Only these 4 axis values are inputted one at a time
    
    **Horzinotal = 1**    //Right direction +ve X  
    **Horzinotal = -1**   //Left direction -ve X  
    **Vertical = 1**      //Up direction +ve Y  
    **Vertical = -1**     //Down direction -ve Y  
    
    **GetAxis(Axis)** is used to fetch axis value with regard to **user key input**   
    **SetMovement()** method to used to check which one of the 4 value of is inputted and pass them to PlayerController for comparing direction with Player Snake.
    
 ### SpawnFood.cs
 Instantiate food item randomly ,maintain streak and load their parameter from our **data file**
 
 It is attached to an empty gameobject acting as our Food Manager  
  **Spawn()** - Read the data file with the help of **FileLines()** method to pick the color and score parameter of the item at random and load them to the current food prefab.  The method is called again from PlayerController.cs when Snake Player eats the current food  We store the Color of the previous food and compare it to the current Color to keep the streak.
  
  **FileLines()** - manipulate strings and store the paramaters

### Data File
Color value(R value<space>B value<space>G value) and Score separated by ;



![](https://i.imgur.com/4otVvcp.png)


## [Core GamePlay Loop](https://giant.gfycat.com/GentleGenuineKob.webm)

![](https://giant.gfycat.com/GentleGenuineKob.webm)
