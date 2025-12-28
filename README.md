### Shutter Camera Plugin



Installation: simply clone the plugin through Tools -> Plugins -> Clone Project
and then copy the .git url



### Usage



Now this is way more interesting because this plugin while aiming to be like FlaxEngine's version of Unity's Cinemachine, is actually just barely the most basic functionality, and is simply my imperfect (if not flawed) interpretation of how Cinemachine functions.



So anyway, there's ShutterHeart.cs that simply follows a ShutterTripod.cs (a parent class for your custom scripted cameras, more on that later) with the highest priority with smooth switching.



All you need to switch cameras, you need to do in code is active the Actor that has a ShutterTripod script attached to it.



The plugin includes an Overhead Style and Spline Follower. Both of them have some settings, AND they are CHILD classes of ShutterTripod, and should serve as a fine enough example for your own scripts.



Some things to note about ShutterTripod:



&nbsp;   public float transitionTime = 1f;

&nbsp;   ShutterHeart ShutterHeartRef;

&nbsp;   public int priority;

&nbsp;   public float FOV = 70f;

&nbsp;   public Actor PlayerInstance;



(actually I hope it's self explanatory .w. )

ShutterHeartRef is assigned automatically upon the Tripod's activation, everything else???

Either way you can probably just understand what Shutter Camera does if you used Cinemachine before.

