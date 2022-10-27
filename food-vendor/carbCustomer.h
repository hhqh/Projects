/* Author: Helen Huang
 * Date: 11/15/2021
 *
 * Overview: This header file lists all the declarations of variables
 * and function prototypes of the carbCustomer class.
 */

#ifndef CARBCUSTOMER_H
#define CARBCUSTOMER_H

#include "customer.h"

class carbCustomer: public customer {
  const int CARBINDEX = 7;
  unsigned int carbLimit;
  unsigned int storedCarbLimit;

public:

  carbCustomer(double bal, int accNum, unsigned int limit = 30);

  //add operator
  carbCustomer operator+(carbCustomer c); //carbCustomer + carbCustomer
  carbCustomer& operator+=(carbCustomer c);  //carbCustomer+=carbCustomer

  //subtract operator
  carbCustomer operator-(carbCustomer c);//carbCustomer-carbCustomer
  carbCustomer& operator-=(carbCustomer c);//carbCustomer-=carbCustomer

  //bool operators inherited from customer

  bool buyOne(entree* item, int accNum);
  unsigned int getDailyIntake();
};


#endif
