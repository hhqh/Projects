### Ghost vs Slimes Game Team Project

Authors: Helen Huang, Vincent Marklynn, Fiona O'Leary, Klaudia Coronel, Charles Burr

This is a game application which is similar to a role playing game where you can select the ghost characters to fight against the slime monsters. 

Game Mechanics
*Turn-based game that starts when the player begins the game.
*The game will pick six monsters to fight.
*Only characters with the living status can participate in the fight.
*The character or monster with the highest speed will go first (ties are broken by other attributes such as highest level).
*Will continue until all characters have had a turn.
 
Experience points:
Characters: Earned when you kill a monster
Monsters: Given to character when killed
Experience earned is proportional to damage done to the monster by the player.
 
Leveling Up:
Characters: When experience points go higher than next level points, happens instantly (Max Level 20). Leveling up can occur after the end of the character’s turn and in the middle of a battle. If a character levels up, current health is offset by the new max health.
 
Attack Hits/Misses:
Hit is determined by a roll of a 20-sided die; 1 is auto miss and 20 is auto hit.
A successful hit occurs if the roll + level + attack modifier > target defense + target level.
Else, the attack misses, and the turn is over for the entity.
The lowest level character player in the list will be selected to attack.
The monster with the lowest max health will be selected by the character to be attacked.
 
Damage:
Damage is determined by a dice roll for weapon damage + level damage. Level damage is ¼ the level rounded up to nearest whole number.
Once hit, the target’s health is reduced by the damage (whole numbers).
 

#### Key Features
| No. | Area |  Description |
| --- | ---- | ------------ |
| 1 | Characters Profile | Add, Update and Delete a character's name, job, health, attack, defense and speed. Includes Index page for all created characters. |
| 2 | Monsters Profile | Add, Update and Delete a monster's name, unique item drop, health, attack, defense and speed. Includes Index page for all created monsters. |
| 3 | Items Profile | Add, Update and Delete an item's name, description, attribute, value, and equipped location. Includes Index page for all created item. |
| 4 | Manual Battle | Select up to six characters to fight against randomly generated monsters. Allows user to select the fighting character when it is the player's turn. When character/monster dies, replaced by a tombstone. Only one side wins when the other side is defeated. |
| 5 | Auto Battle | All characters are randomly selected and fights with monsters behind the scenes. Takes a couple seconds to complete battle. |
| 6	| Miracle Max | When a character's health drops under zero, revived by magic to their max health. Only occurs once per battle. |
| 7 | Boss Battle | There is a chance a boss will be generated in a battle instead of six monsters. |
| 8 | Item Steal | There is a chance that monsters will steal the dropped item when a character dies. |
| 9 | Monster's Item | Monsters are equipped with items. |
| 10 | Item Retrieval | Auto retrieve items in the cloud. |


##### Install Xamarin to run the application (run as UWP)

##### Install an Emulator to run the application on Android

##### Screenshots
Home |  Index | Read | Edit | Delete 
:----: |  :-------: | :-----: | :-------: | :-------:
![Home](https://user-images.githubusercontent.com/81440895/199650386-52c95d2d-6797-4809-a350-eed39c0cc0c3.PNG) | ![index](https://user-images.githubusercontent.com/81440895/199650894-0d298cf0-80aa-404c-bf0b-eb0c2ac689e1.PNG) | ![Read](https://user-images.githubusercontent.com/81440895/199650976-a80c119b-63a6-465d-a262-f696c33df87e.PNG) | ![Edit](https://user-images.githubusercontent.com/81440895/199651056-05ce064d-5901-46aa-afd6-660d9a677b93.PNG) | ![delete1](https://user-images.githubusercontent.com/81440895/199651131-d71ef68a-67d5-49d6-a4d5-73bc95094611.PNG)


Select Characters | Battle Overview | Battle 1 | Battle 2 | Battle 3
:-------: | :-------: | :------: | :--------: | :---------: 
![Character Select](https://user-images.githubusercontent.com/81440895/199650746-5ae623ea-5c5b-429a-937d-6915931b971d.PNG) | ![overview](https://user-images.githubusercontent.com/81440895/199651249-fa04667e-e29c-4d95-be9a-6c3990102a01.PNG) | ![Battle - treasure](https://user-images.githubusercontent.com/81440895/199651224-01b7de97-df47-4023-a030-8902176661d3.PNG) | ![Battle - Ghost hit](https://user-images.githubusercontent.com/81440895/199651232-6e29d23a-a9f8-449a-9e6a-b7c53717108d.PNG) | ![Battle - Select](https://user-images.githubusercontent.com/81440895/199651368-71dc6d07-3acd-4246-885d-4a93420d2bbf.PNG)


Battle 4 | After Battle 1 | After Battle 2 | After Battle 3 | Auto Battle
:-------: | :---------: | :----------: | :-------: | :-------:
![Battle - detah](https://user-images.githubusercontent.com/81440895/199651499-aa0733b2-3191-4f1d-be92-0d0c6a63fdce.PNG) | ![After Battle](https://user-images.githubusercontent.com/81440895/199651510-6c68b6d6-9713-4506-9202-809cb27f1fc1.PNG) | ![Item drop](https://user-images.githubusercontent.com/81440895/199651519-61e24577-5d26-4d32-9b20-690236502274.PNG) | ![ghost lost](https://user-images.githubusercontent.com/81440895/199651521-460f4f98-0ef8-4ac3-ab1a-129c6a501c59.PNG) | ![autobattle](https://user-images.githubusercontent.com/81440895/199651524-258e5e63-07b9-4563-9ffc-038d3a49476e.PNG)


