/* Author: Helen Huang
 * Date: 10/22/2021
 *
 * Overview: This header file lists all the declarations of variables
 * and function prototypes of the entree class.
 */


#ifndef ENTREE_H
#define ENTREE_H
#include <string>
#include <math.h>
using namespace std;

class entree {

  string entreeName;
  string* ingredientArr = nullptr;
  unsigned int* nutritionArr = nullptr;
  string expireDate;   //default = 01/01/2000
  bool needRefrigerate;
  bool refrigerated;
  bool powerOutage;
  unsigned int quantity;
  double price;

  bool isPastDate(string date);
  bool isNumber(const string &s);
  string setValidDate(string date);
  double setValidPrice(double price);
  bool compare(double x, double y, double epsilon =  0.0000001f);

public:
  explicit entree(string entreeName, string *ingredientArr, unsigned
  int *nutritionArr, string expireDate, bool needRefrigerate, unsigned int
  quantity, double price);
  entree();

  //add operator
  entree operator+(entree e); //entree + entree
  entree operator+(double num); //entree + double
  friend entree operator+(double num, entree e); // double + entree
  entree& operator+=(entree e);   // entree += entree
  entree& operator+=(double num);   // entree += double
  entree operator++();  // pre ++
  entree operator++(int num);  //post ++

  //subtract operator
  entree operator-(entree e); //entree - entree
  entree operator-(double num); //entree - double
  friend entree operator-(double num, entree e); // double - entree
  entree& operator-=(entree e);   // entree -= entree
  entree& operator-=(double num);   // entree -= double
  entree operator--();  // pre --
  entree operator--(int num);  //post --

  //comparison operator /w entree
  bool operator==(entree e);
  bool operator!=(entree e);
  bool operator<(entree e);
  bool operator>(entree e);
  bool operator<=(entree e);
  bool operator>=(entree e);

  //comparison operator /w double
  bool operator==(double num);
  bool operator!=(double num);
  bool operator<(double num);
  bool operator>(double num);
  bool operator<=(double num);
  bool operator>=(double num);

  //comparison operator double /w entree
  friend bool operator==(double num, entree e);
  friend bool operator!=(double num, entree e);
  friend bool operator<(double num, entree e);
  friend bool operator>(double num, entree e);
  friend bool operator<=(double num, entree e);
  friend bool operator>=(double num, entree e);

  string getName();
  string getExpireDate();
  unsigned int getQuantity();
  double getPrice();
  void setQuantity(unsigned int num);
  void setPowerOutage();
  bool isExpired();
  bool isSpoiled();
  unsigned int findNutritionFacts(int num);
  bool contains(string ingredients);

};


#endif
