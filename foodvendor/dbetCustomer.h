/* Author: Helen Huang
 * Date: 11/15/2021
 *
 * Overview: This header file lists all the declarations of variables
 * and function prototypes of the dbetustomer class.
 */

#pragma GCC diagnostic push
#pragma GCC diagnostic ignored "-Wc++11-extensions"

#ifndef DBETCUSTOMER_H
#define DBETCUSTOMER_H

#include "customer.h"

class dbetCustomer: public customer{

  const int SUGARINDEX = 9;
  const unsigned int SUGARLIMIT = 50;
  const unsigned int SUGARONE = 10;
  const unsigned int SUGARTOTAL = 25;
  unsigned int sugarLimit;

public:
  dbetCustomer(double bal, int accNum);

  //add operator
  dbetCustomer operator+(dbetCustomer c); //dbetCustomer + dbetCustomer
  dbetCustomer& operator+=(dbetCustomer c);   // dbetCustomer += dbetCustomer

  //subtract operator
  dbetCustomer operator-(dbetCustomer c); //dbetCustomer - dbetCustomer
  dbetCustomer& operator-=(dbetCustomer c);   // dbetCustomer -= dbetCustomer

  //bool operators inherited from customer

  bool buyOne(entree* item, int accNum);
  bool buy(entree* item, unsigned int quantity, int accNum);
};


#endif
