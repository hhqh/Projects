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


