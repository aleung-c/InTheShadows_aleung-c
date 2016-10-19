# InTheShadows_aleung-c
Unity C# project. Explore the tower and resolve the shadow puzzles !

This project is a FPS adventure game where the player must resolve shadow puzzles to advance through the level. Every interaction is handscripted in C# and the whole project is made with Unity 5.2.2. 

The interesting part of the code was about how to handle rotation of objects through the user input. In this project, I aimed to have a "natural feeling" rotation. 
To achieve it, I handled the vertical and horizontal rotations differently: the vertical rotation makes the obbject turn from its center point whereas the horizontal rotation is a "world related" rotation, meaning it moves horizontally no matter the object's vertical rotation. This is interesting because when an object takes a vertical rotation, its horizontal rotation axis changes greatly. 
This point of gameplay can make a lot of difference between a funnily resolvable puzzle and a rotational hell. 

Concerning the level design, I made intensive use of Blender, to be able to make the actual tower i wanted to have, and imported the towers parts into unity. This caused a lot of optimization issues, that I fixed through researching unity engine's optimization points.

![Alt text](./cover/light_online_demo_screen1.png "light online screen 1")

![Alt text](./cover/light_online_demo_screen2.png "light online screen 2")
