/* Author: Helen Huang
 * Date: 10/22/2021
 *
 * Class Invariants:
 * - An entree object will always have states of whether it is expired or spoiled.
 * - Some data members of a entree object can only be accessible through
 *   accessor.
 * - The input of nutritionArr value can only be of size 11.
 *
 * Interface Invariants:
 * - The expire state of an entree can only be changed when isExpired() is
 *   called.
 * - The spoiled state of an entree can only be changed when isSpoiled() is
 *   called.
 * - The contains(string) function returns true if the ingredient is in the
 *   entree, false if not.
 * - The findNutritionFacts(int) function returns the value of the corresponding
 *   nutrition located in a given index.
 */

#include "entree.h"

entree::entree()
{
  entreeName = " ";
  ingredientArr = nullptr;
  nutritionArr= nullptr;
  expireDate = "01/01/2000";
  needRefrigerate = false;
  powerOutage = false;
  price = 0.0;
  quantity = 0;
  refrigerated = true;
}

entree::entree(string entreeName, string ingredientArr[], unsigned int
nutritionArr[], string expireDate, bool needRefrigerate,
               unsigned int quantity, double price){
  this->entreeName = entreeName;
  this->ingredientArr = ingredientArr;
  this->nutritionArr = nutritionArr;
  this->expireDate = setValidDate(expireDate);
  this->needRefrigerate = needRefrigerate;
  this->quantity = quantity;
  this->price = setValidPrice(price);
  this->powerOutage = false;
  this->refrigerated = true;
}

double entree::setValidPrice(double price)
{
  if (price < 0.0){
    return 0.0;
  }
  return price;
}

string entree::setValidDate(string date)
{
  string defaultDate = "01/01/2000";

  if (date.empty()){
    return defaultDate;
  }

  string inMonth = date.substr(0,2);
  string inDay = date.substr(3,2);
  string inYear = date.substr(6,4);

  if(isNumber(inDay) && isNumber(inMonth) && isNumber(inYear)){
    if (stoi(inMonth) <= 12 && stoi(inMonth) >= 1) {
      if (stoi(inDay) <= 30 && stoi(inDay) >= 1) {
        if (stoi(inYear) <= 9999 && stoi(inDay) >= 1) {
          return date;
        }
      }
    }
  }
  return defaultDate;
}


bool entree::isPastDate(string date)
{
  time_t current = time(0);
  tm *ltm = localtime(&current);

  int curMonth = 1 + ltm->tm_mon;
  int curDay = ltm->tm_mday;
  int curYear = 1900 + ltm->tm_year;

  int inMonth = stoi(date.substr(0,2));
  int inDay = stoi(date.substr(3,2));
  int inYear = stoi(date.substr(6, 4));

  if (inYear < curYear){
    return true;
  }else if (inYear == curYear){
    if (inMonth < curMonth){
      return true;
    }else if(inMonth == curMonth){
      if (inDay <= curDay){
        return true;
      }
    }
  }
  return false;
}


entree entree::operator+(entree e)
{
  int nutriSize = 11;
  string newName = this->getName() + "__" + e.getName();
  unsigned int newQuantity = this->getQuantity() + e.getQuantity();
  double newPrice = this->getPrice() + e.getPrice();

  unsigned int *newNutriArr = new unsigned int[nutriSize];

  for (int i = 0; i < nutriSize; i++){
    unsigned int res = this->findNutritionFacts(i) + e.findNutritionFacts(i);
    newNutriArr[i] = res;
  }

  entree local (newName, this->ingredientArr, newNutriArr, this->getExpireDate(),
                this->needRefrigerate,
                newQuantity, newPrice);
  return local;
}

// Increases entree price
entree entree::operator+(double num)
{
  double newPrice;

  if (num > 0) {
    newPrice = this->getPrice() + num;
  }else{
    newPrice = this->getPrice() + abs(num);
  }
  entree local (this->entreeName, this->ingredientArr, this->nutritionArr,
                this->expireDate, this->needRefrigerate,
                this->quantity, newPrice);
  return local;

}

entree operator+(double num, entree e)
{
  return e + num;
}

entree& entree::operator+=(entree e)
{
  int nutriSize = 11;
  this->entreeName = this->getName() + "__" + e.getName();
  this->quantity += e.getQuantity();
  this->price += e.getPrice();

  for (int i = 0; i < nutriSize; i++){
    this->nutritionArr[i] += e.findNutritionFacts(i);
  }
  return *this;

}

entree& entree::operator+=(double num)
{
  if(num > 0) {
    this->price += num;
  }else{
    this->price += abs(num);
  }

  return *this;

}

entree entree::operator++()
{
  this->price = this->getPrice() + 1.0;
  return *this;
}

entree entree::operator++(int num)
{
  double newPrice = this->getPrice() + 1.0;

  entree local(this->entreeName, this->ingredientArr, this->nutritionArr,
               this->expireDate, this->needRefrigerate,
               this->quantity, newPrice);
  return local;
}

entree entree::operator-(entree e)
{
  int nutriSize = 11;
  unsigned int newQuantity;
  double newPrice;

  if (e.getQuantity() <= this->getQuantity()) {
    newQuantity = this->getQuantity() - e.getQuantity();
  }else{
    newQuantity = 0.0;
  }

  if (e.getPrice() <= this->getPrice()) {
    newPrice = this->getPrice() - e.getPrice();
  }else{
    newPrice = 0.0;
  }

  unsigned int *newNutriArr = new unsigned int[nutriSize];

  for (int i = 0; i < nutriSize; i++){
    unsigned int res;
    if (e.findNutritionFacts(i) < this->findNutritionFacts(i)){
      res = this->findNutritionFacts(i) - e.findNutritionFacts(i);
    }else{
      res = 0;
    }
    newNutriArr[i] = res;
  }

  entree local (this->entreeName, this->ingredientArr, newNutriArr, this->getExpireDate(),
                this->needRefrigerate,
                newQuantity, newPrice);
  return local;
}

// Decreases entree price
entree entree::operator-(double num)
{
  double newPrice;

  if (num <= this->getPrice()) {
    newPrice = this->getPrice() - num;
  }else{
    newPrice = 0.0;
  }

  entree local (this->entreeName, this->ingredientArr, this->nutritionArr,
                this->expireDate, this->needRefrigerate,
                this->quantity, newPrice);
  return local;

}

entree operator-(double num, entree e)
{
  return e - num;
}

entree& entree::operator-=(entree e)
{
  int nutriSize = 11;

  if (e.getQuantity() <= this->getQuantity()) {
    this->quantity -= e.getQuantity();
  }else{
    this->quantity = 0.0;
  }

  if (e.getPrice() <= this->getPrice()) {
    this->price -= e.getPrice();
  }else{
    this->price = 0.0;
  }

  for (int i = 0; i < nutriSize; i++){
    if (e.findNutritionFacts(i) < this->findNutritionFacts(i)){
      this->nutritionArr[i] -= e.findNutritionFacts(i);
    }else{
      this->nutritionArr[i] = 0;
    }
  }

  return *this;
}

entree& entree::operator-=(double num)
{
  if(num <= this->price) {
    this->price -= num;
  }else{
    this->price = 0.0;
  }

  return *this;
}

entree entree::operator--()
{
  if (this->getPrice() >= 1.0) {
    this->price = this->getPrice() - 1;
  }else{
    this->price = 0.0;
  }
  return *this;
}

entree entree::operator--(int num)
{
  double newPrice = this->getPrice() - 1;

  if (newPrice <= 0.0){
    newPrice = 0.0;
  }

  entree local(this->entreeName, this->ingredientArr, this->nutritionArr,
               this->expireDate, this->needRefrigerate,
               this->quantity, newPrice);
  return local;
}

bool entree::operator==(entree e)
{
  double res1 = this->getPrice() * this->getQuantity();
  double res2 = e.getPrice() * e.getQuantity();

  return compare(res1, res2);
}

bool entree::operator!=(entree e)
{
  double res1 = this->getPrice() * this->getQuantity();
  double res2 = e.getPrice() * e.getQuantity();

  return !compare(res1, res2);
}

bool entree::operator<(entree e)
{
  double res1 = this->getPrice() * this->getQuantity();
  double res2 = e.getPrice() * e.getQuantity();

  return res1 < res2;
}

bool entree::operator>(entree e)
{
  double res1 = this->getPrice() * this->getQuantity();
  double res2 = e.getPrice() * e.getQuantity();

  return res1 > res2;
}

bool entree::operator<=(entree e)
{
  double res1 = this->getPrice() * this->getQuantity();
  double res2 = e.getPrice() * e.getQuantity();

  return res1 < res2 || compare(res1, res2);
}

bool entree::operator>=(entree e)
{
  double res1 = this->getPrice() * this->getQuantity();
  double res2 = e.getPrice() * e.getQuantity();

  return res1 > res2 || compare(res1, res2);
}

bool entree::operator==(double num)
{
  double res1 = this->getPrice() * this->getQuantity();

  return compare(res1, num);
}

bool entree::operator!=(double num)
{
  double res1 = this->getPrice() * this->getQuantity();

  return !compare(res1, num);
}

bool entree::operator<(double num)
{
  double res1 = this->getPrice() * this->getQuantity();

  return res1 < num;
}

bool entree::operator>(double num)
{
  double res1 = this->getPrice() * this->getQuantity();

  return res1 > num;
}

bool entree::operator<=(double num)
{
  double res1 = this->getPrice() * this->getQuantity();

  return res1 < num || compare(res1, num);
}

bool entree::operator>=(double num)
{
  double res1 = this->getPrice() * this->getQuantity();

  return res1 > num || compare(res1, num);
}

bool operator==(double num, entree e)
{
  return e == num;
}

bool operator!=(double num, entree e)
{
  return e != num;
}

bool operator<(double num, entree e)
{
  return e > num;
}

bool operator>(double num, entree e)
{
  return e < num;
}

bool operator<=(double num, entree e)
{
  return e >= num;
}

bool operator>=(double num, entree e)
{
  return e <= num;
}

string entree::getName(){
  return entreeName;
}

string entree::getExpireDate(){
  return expireDate;
}

unsigned int entree::getQuantity() {
  return quantity;
}

double entree::getPrice(){
  return price;
}

void entree::setPowerOutage()
{
  powerOutage = true;
}

void entree::setQuantity(unsigned int num){
  quantity = num;
}

// pre: entree must have an expired date
// post: expire status may change
bool entree::isExpired()
{
  if (isPastDate(expireDate)){
    return true;
  }
  return false;
}

// pre: entree must have an expired date, refrigerate and powerOutage status
// post: spoil status may change
bool entree::isSpoiled()
{
  if(isExpired()){
    return true;
  }
  else if (needRefrigerate) {
    if (!refrigerated) {
      return true;
    }
  }
  else if (powerOutage){
    return true;
  }
  return false;

}

bool entree::contains(std::string ingredients)
{
  for (int i = 0; i < (int)ingredientArr->length()-1; i++){
    if (ingredients == ingredientArr[i]){
      return true;
    }
  }
  return false;
}

unsigned int entree::findNutritionFacts(int num)
{
  if (num > 10){
    return 0;
  }
  return nutritionArr[num];
}

bool entree::isNumber(const string& s)
{
  for (char const &ch : s) {
    if (isdigit(ch) == 0)
      return false;
  }
  return true;
}

bool entree::compare(double x, double y, double epsilon)
{
  if (fabs(x - y) < epsilon)
  {
    return true;
  }
  return false;
}

/*
 * Implementation Invariants:
 * - If the expiration date is past the date of today, then the entree is expired.
 * - If the entree needs to be refrigerated, and not refrigerated, then the
 *   entree is spoiled.
 * - If the entree is expired, then it is spoiled.
 * - Once there is a power outage, the entree will consider to be spoiled.
 * - The nutrition facts is looked up by the index number, each index correspond
 *   to a nutrition value. (e.g. number of servings -> index = 0)
 * - Entree can add entree (this totals the nutrition facts of both entrees)
 *   and can add a double value to the price (if price increase is needed).
 * - Same logic as entree-entree and entree-double and double-entree.
 * - Entree bool operator can compare entree to entree (their price) and
 *   entree to double.
 */