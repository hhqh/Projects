/* Author: Helen Huang
 * Date: 11/6/2021
 *
 * Class Invariants:
 * - dbetCustomer inherits methods of customer class.
 * - Overrides parent class of buy and buyOne to allows restriction of sugar amount.
 * - dbetCustomer has limited daily total sugars of 50g.
 *
 * Interface Invariants:
 * - buyOne() allows them to buy one item from a vendor and the item can not be
 *   purchased if it is exceeds 10g of sugar and also needs to be available and
 *   not spoiled.
 * - buy() allows the customer to buy a meal of 2 or 3 items from a vendor and can
 *   not exceed the customer's meal totals for sugars of 25g.
 */

#include "dbetCustomer.h"

dbetCustomer::dbetCustomer(double bal, int accNum) : customer(bal,accNum)
{
  sugarLimit = SUGARLIMIT; //sugar limit set to 50g
}

dbetCustomer dbetCustomer::operator+(dbetCustomer c)
{
  dbetCustomer local(this->balance + c.getBalance(), this->accountNum);

  return local;
}

dbetCustomer& dbetCustomer::operator+=(dbetCustomer c)
{
  this->balance += c.getBalance();

  return *this;
}

dbetCustomer dbetCustomer::operator-(dbetCustomer c)
{
  dbetCustomer local(this->balance + c.getBalance(), this->accountNum);

  return local;
}

dbetCustomer& dbetCustomer::operator-=(dbetCustomer c)
{
  this->balance += c.getBalance();

  return *this;
}

// pre: item is not null
// post: balance may change
bool dbetCustomer::buyOne(entree* item, int accNum)
{
  unsigned sugar;

  if (item != nullptr){
    sugar = item->findNutritionFacts(SUGARINDEX);

    if (!item->isSpoiled() && item->getQuantity() > 0 && sugar <= SUGARONE && item->getPrice() < balance){
      sugarLimit -= sugar;
      balance -= item->getPrice();
      return true;
    }
  }
  return false;
}

// pre: item is not null
// post: balance may change
bool dbetCustomer::buy(entree* item, unsigned int quantity, int accNum)
{
  unsigned int sugar;
  unsigned int totalSugar;

  if (item != nullptr){
    sugar = item->findNutritionFacts(SUGARINDEX);
    totalSugar = sugar * quantity;

    if ((quantity >= MEALLIMITDOWN && quantity <= MEALLIMITUP)
         && totalSugar <= SUGARTOTAL){
      for (unsigned int i = 0; i < quantity; i++){
        buyOne(item, accNum);
      }
      return true;
    }
  }
  return false;
}

/* Implementation Invariants:
 * - dbetCustomer can buy one item that doesn't exceed 10g of sugar.
 * - When buying more than one, the item cannot exceed total sugar of 25g.
 * - Inherits customer's addition and subtraction operators.
 * - Has its own bool operators to compare its balance to other customer and
 *   double.
 */