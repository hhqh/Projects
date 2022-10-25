/* Author: Helen Huang
 * Date: 11/6/2021
 *
 * Class Invariants:
 * - allergyCustomer inherits methods of customer class.
 * - Overrides parent class buyOne & buy to allows restriction of allergen.
 * - allergyCustomer is either severely allergic (including trace amounts of the
 *   ingredient) or sensitive only to large quantities of the allergen.
 *
 * Interface Invariants:
 * - buyOne() allows them to buy one item from a vendor and can not be purchased
 *   if the customer is severely allergic to an ingredient of the item and
 *   also needs to be available and not spoiled.
 * - buy() allows only customer who are sensitive to large quantities to
 *   purchase 2 or 3 meals.
 */

#include "allergyCustomer.h"

allergyCustomer::allergyCustomer(double bal, int accNum, bool severe,
                                 string allergicIngre) :
                                 customer(bal, accNum)
{
  severelyAllergic = severe;
  allergy = allergicIngre;
}

allergyCustomer allergyCustomer::operator+(allergyCustomer c)
{
  allergyCustomer local(this->balance + c.getBalance(), this->accountNum,
                        this->severelyAllergic, this->allergy);

  return local;
}

allergyCustomer& allergyCustomer::operator+=(allergyCustomer c)
{
  this->balance += c.getBalance();

  return *this;
}

allergyCustomer allergyCustomer::operator-(allergyCustomer c)
{
  allergyCustomer local(this->balance - c.getBalance(), this->accountNum,
                        this->severelyAllergic, this->allergy);

  return local;
}

allergyCustomer& allergyCustomer::operator-=(allergyCustomer c)
{
  this->balance -= c.getBalance();

  return *this;
}


// pre: item is not null
// post: balance may change
bool allergyCustomer::buyOne(entree *item, int accNum)
{
  bool contains;
  if (item != nullptr){
    contains = item->contains(allergy);
    if (!item->isSpoiled() && item->getQuantity() > 0 &&
        !(contains && severelyAllergic) && item->getPrice() < balance){
      balance -= item->getPrice();
      return true;
    }
  }
  return false;
}

// pre: item is not null
// post: balance may change
bool allergyCustomer::buy(entree* item, unsigned int quantity, int accNum)
{
  if (item != nullptr)
  {
    if (quantity >= MEALLIMITDOWN && quantity <= MEALLIMITUP && !severelyAllergic)
    {
      for (unsigned int i = 0; i < quantity; i++)
      {
        buyOne(item, accNum);
      }
      return true;
    }
  }
  return false;
}

/* Implementation Invariants:
 * - allergyCustomer can buy items according to the level of sensitivity and if item
 *   contains the allergen.
 * - Inherits customer's addition and subtraction operators.
 * - Has its own bool operators to compare its balance to other customer and
 *   double.
 */
