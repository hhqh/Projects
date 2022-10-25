/* Author: Helen Huang
 * Date: 11/15/2021
 *
 * Overview: This header file lists all the declarations of variables
 * and function prototypes of the allergyCustomer class.
 */

#ifndef ALLERGYCUSTOMER_H
#define ALLERGYCUSTOMER_H


#include "customer.h"

class allergyCustomer : public customer{
  bool severelyAllergic; // false = sensitive to large quantities
  string allergy;

public:
  allergyCustomer(double bal, int accNum, bool severe, string allergicIngre);

  //add operator
  allergyCustomer operator+(allergyCustomer c); //allergyCustomer +allergyCustomer
  allergyCustomer& operator+=(allergyCustomer c);  // allergyCustomer+=allergyCustomer

  //subtract operator
  allergyCustomer operator-(allergyCustomer c);//allergyCustomer-allergyCustomer
  allergyCustomer& operator-=(allergyCustomer c);  //allergyCustomer-=allergyCustomer

  //bool operators inherited from customer

  bool buyOne(entree *item, int accNum);
  bool buy(entree *item, unsigned int quantity, int accNum);
};


#endif
