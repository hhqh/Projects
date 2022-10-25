Dataset Information

Overall description: According to the github cited source, this data set contains a list of car information and attributes along with the necessary information about the pecification of an auto interm of various characteristics, the assigned insurace risk rating, its normalized looses in use as compared to other cars. 

The data set we choose is a specification of auto imports and the data is cited from three sources:
1) 1985 Model Import Car and Truck Specifications, 1985 Ward's
Automotive Yearbook.
2) Personal Auto Manuals, Insurance Services Office, 160 Water
Street, New York, NY 10038
3) Insurance Collision Report, Insurance Institute for Highway
Safety, Watergate 600, Washington, DC 20037

The data consists of cars with various attributes such as car brands, price, fuel type, appearance etc. The data set has 205 entries, large enough for preprocessing invalid data and applying machine learning models such as classification and regression. The size of the data provides more accuracy for the models. The data set is quite complex and challenging to process as there is some noisy and null data in the set. Therefore, we would expect to do some data preprocessing before using it to train the model. From the report of the data sources, the data set has been used in various machine learning projects with some decent results. Because of the decent results, we can rely on this data set while analyzing and evaluating a model.

Attribute description:

7. Attribute Information:     
     Attribute:                	Attribute Range:
     ------------------        	-----------------------------------------------
  1. symboling (int):			the risk factor associated with tis price ranging from being safe -3 to +3 for being risky
  2. normalized-losses(int):   	relative average loss payment per insured vehicle year
  3. make(string):                  the list of car brands in the data set
  4. fuel-type(string):             type of fuel the cars use in the data set
  5. aspiration(string):            the internal combustion engine where the intake of air depends on the atmospheric pressure  
  6. num-of-doors(int):             number of doors the car has 
  7. body-style(string):            the type of body of the car
  8. drive-wheels(string):          the type of drive-wheels
  9. engine-location(string):       location of the engine
 10. wheel-base(float):             continuous from 86.6 120.9.
 11. length(float):                 the car's length
 12. width(float):                  the car's width
 13. height(float):                 the car's height
 14. curb-weight(int):         	the weight of the vehicle including a full tank of fuel and all standard equipment.
 15. engine-type(string):           the type of engine in the car
 16. num-of-cylinders(int):         the number of cylinders in the car
 17. engine-size(int):              the size of the engine
 18. fuel-system(string):           the fuel system that the car has
 19. bore(float):                   the diameter of each cylinder
 20. stroke(float):                 A phase of the engine's cycle during which the piston travels from top to bottom or vice versa
 21. compression-ratio(int):        the ratio of the volume of the cylinder and its head space
 22. horsepower(int):               the power of the horsepower in the car.
 23. peak-rpm(int):                 the highest speed that the car can achieve
 24. city-mpg(int):                 the score a car will get on average in city conditions, with stopping and starting at lower speeds
 25. highway-mpg(int):              the average a car will get while driving on an open stretch of road without stopping or starting
 26. price(int):                    price range of the car

Size of dataset:

The total number of instances in the data set: 205
The total Number of attributes: 26
   -- 15 continuous
   -- 1 integer
   -- 10 nominal