/* Author: Helen Huang
 * Date: 11/6/2021
 *
 * Class Invariants:
 * - carbCustomer inherits methods of customer class.
 * - Overrides parent class buyOne to allows restriction of carb amount.
 * - carbCustomer has limited daily total carb.
 *
 * Interface Invariants:
 * - buyOne() allows them to buy one item from a vendor and can not be purchased
 *   if it is exceeds their daily total of carbohydrates and also needs to be
 *   available and not spoiled.
 * - getDailyIntake() allows customer to get total carb accessed for the day.
 */

#include "carbCustomer.h"

carbCustomer::carbCustomer(double bal, int accNum, unsigned int limit) :
              customer(bal, accNum)
{
  carbLimit = limit;
  storedCarbLimit = limit;
}

carbCustomer carbCustomer::operator+(carbCustomer c)
{
  unsigned int limit = this->carbLimit > c.carbLimit ? this->carbLimit : c
    .carbLimit;

  carbCustomer local(this->balance + c.getBalance(), this->accountNum,
                        limit);

  return local;
}

carbCustomer& carbCustomer::operator+=(carbCustomer c)
{
  unsigned int limit = this->carbLimit > c.carbLimit ? this->carbLimit : c
    .carbLimit;
  this->balance += c.getBalance();
  this->carbLimit = limit;

  return *this;
}

carbCustomer carbCustomer::operator-(carbCustomer c)
{
  unsigned int limit = this->carbLimit > c.carbLimit ? this->carbLimit : c
    .carbLimit;

  carbCustomer local(this->balance - c.getBalance(), this->accountNum,
                     limit);

  return local;
}

carbCustomer& carbCustomer::operator-=(carbCustomer c)
{
  unsigned int limit = this->carbLimit > c.carbLimit ? this->carbLimit : c
    .carbLimit;
  this->balance -= c.getBalance();
  this->carbLimit = limit;

  return *this;
}

// pre: item is not null
// post: balance may change
bool carbCustomer::buyOne(entree *item, int accNum)
{
  unsigned int carb;

  if (item != nullptr){
    carb = item->findNutritionFacts(CARBINDEX);

    if (!item->isSpoiled() && item->getQuantity() > 0 && carb < carbLimit && item->getPrice() < balance){
      carbLimit -= carb;
      balance -= item->getPrice();
      return true;
    }
  }
  return false;
}

unsigned int carbCustomer::getDailyIntake()
{
  return storedCarbLimit - carbLimit;
}

/* Implementation Invariants:
 * - carbCustomer can buy items that doesn't exceed given carb limit.
 * - Inherits customer's addition and subtraction operators.
 * - Has its own bool operators to compare its balance to other customer and
 *   double.
 */
