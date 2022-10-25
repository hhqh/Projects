/* Author: Helen Huang
 * Date: 10/22/2021
 *
 * Overview: This header file lists all the declarations of variables
 * and function prototypes of the customer class.
 */

#pragma GCC diagnostic push
#pragma GCC diagnostic ignored "-Wc++11-extensions"

#ifndef CUSTOMER_H
#define CUSTOMER_H

#include "entree.h"
#include <math.h>

class customer {

protected:
  const unsigned int MEALLIMITUP = 3;
  const unsigned int MEALLIMITDOWN = 1;
  double balance;
  int accountNum;

  bool compare(double x, double y, double epsilon);

public:
  customer(double bal, int accNum);

  //add operator
  customer operator+(customer c); //customer + customer
  customer& operator+=(customer c);   // customer += customer

  //subtract operator
  customer operator-(customer c); //customer - customer
  customer& operator-=(customer c);   // customer -= customer

  //comparison operator /w customer
  bool operator==(customer c);
  bool operator!=(customer c);
  bool operator<(customer c);
  bool operator>(customer c);
  bool operator<=(customer c);
  bool operator>=(customer c);

  //comparison operator /w int
  bool operator==(double num);
  bool operator!=(double num);
  bool operator<(double num);
  bool operator>(double num);
  bool operator<=(double num);
  bool operator>=(double num);

  friend bool operator==(double num, customer c);
  friend bool operator!=(double num, customer c);
  friend bool operator<(double num, customer c);
  friend bool operator>(double num, customer c);
  friend bool operator<=(double num, customer c);
  friend bool operator>=(double num, customer c);

  double getBalance();
  void addMoney(int accNum, double money);
  virtual bool buy(entree* item, unsigned quantity, int accNum);
  virtual bool buyOne(entree* item, int accNum);

};


#endif