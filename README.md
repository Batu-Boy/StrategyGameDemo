# StrategyGameDemo
 
INFORMATION

You can find windows build in Windows Build folder.

You can move your camera with W,A,S,D and SHIFT for speed boost.

You can open main menu with ESC.

You can save and load level from main menu. If you don't have any saves you cannot load. And if you dont have any loaded game you cannot save.

You can Chase an Enemy with right click on an enemy after select one unit. (Try it with ranged archery Units :))

Since this is demo build, You can select and control any unit or building in the game. But you can only constructs Green Team buildings.

There is no win fail condition according to demo. Last team survives wins.

You can create an unreachable area with buildings. And if you try to move any unit to that area unit will go to the nearest reachable position.(Still you can attack with range)

ISSUES AND SOLUTIONS

-There is a one issue about unit construction. Since there is only one spawn point for each building(where the bottom of the building). If you construct your buildings vertically snapped each other the costructed unit may spawn on the other building.

Solution(Not Implemented due to lack of time): Each building can check wheter the spawn position is empty or not and if so check for other neighbor positions. If every neighbors are full, then you cannot construct unit. Note: It is easy to implement.

LATER FEATURES AND SOLUTIONS

-Selecting more than one unit.(the code is ready for it but due to the lack of time I was not be able to implement)
 Solution: Easy box casting while mouse down. Add first unit to the list and check other unit's team within the box. If the same team add to the selected list. If not, do nothing.
 
 -Saving Versions(not implemented for the same reason)
 Solution: Simply hold seperate data with overriding option.
