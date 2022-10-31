#### Plant Based Milk Inventory

This project's purpose is to record and compare different vegan, plant-based milk products in Seattle grocery stores. The information needed are nutrition facts of vegan milk such as calories, carbohydrates, proteins and fats, the type of milk recorded (soy, coconut, macadamia, rice, almond, etc.), the brands of milk (Blue Diamonds, Whole Foods Market, Pacific Foods…). In addition to that, the location and date of purchase, ratings, and the price of the product are also recorded.

#### Install mySQL and server
Create schema milkdb to connect.

#### Key Users
| Role |	Action |
| ---- | ------ |
| Vegan Milk Information Provider | Provide nutrition, market price and ratings information of different vegan milk to help clients choose the most suitable products |
| Client | Looks up for purchase information of the vegan milk product |

#### Key Features
| No. | Area |  Description |
| --- | ---- | ------------ |
| 1 | Milk Entry | Add milk’s type, brand, location, price, ratings, nutrition facts. |
| 2 | Client Entry | Records Client’s info and keep track of client purchases. |
| 3 | Milk Report | Report all milk products in the system along with the basic information of price, milk type, milk brand and nutrition facts. |
| 4 | Client Report | Report all client’s purchased milk products along with client name and rating and purchase date. |
| 5 | Almond and Oat Milk Report | Report all the almond and oat milk products |
| 6	| Milk Price $3 and Up Report | Report all milk products with prices >= $3 |
| 7 | Client Not Shopped at QFC Report | Report all clients who have not shopped at QFC |
| 8 | Client Purchase Report | Report the client’s purchase summary (num of products brought, total spending…) |
| 9 | Milk Type Report | Report total num of milk products in each type of milk |
| 10 | Milk With Calories 70 Up Report | Report the milk products with calories >= 70 |
| 11 | Milk Brand with >=4.5 Rating Report | Report the milk brands with 4.5 rating and above |
| 12 | Client Purchase Report(02/03-02/09) | Report client spending from 02-03-2020 to 02-09-2020
| 13 | Client Visiting Report | Report store locations visited by each client |
| 14 | Milk Rating Report | Report all milk rating numbers even if it’s null |
| 15 | Client With No Rating Report | Report all clients who have not rated yet |


#### ERD
<img width="1061" alt="Screen Shot 2022-10-30 at 10 52 30 PM" src="https://user-images.githubusercontent.com/81440895/198940604-fa0050c0-c480-4047-aef9-2fe70c01c800.png">