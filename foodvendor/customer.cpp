/* Author: Helen Huang
 * Date: 10/22/2021
 *
 * Class Invariants:
 * - A customer needs to have an account number and initial balance passed by
 *   through constructor.
 * - Customers can add money and buy items.
 *
 * Interface Invariants:
 * - addMoney allows a customer to add money with a given account number and
 *   amount of money to add.
 * - buyItems allows a customer to buy items while deducting from the
 *   customer's balance.
 */

#include "customer.h"

customer::customer(double bal, int accNum)
{
  if (accNum > 0 && bal > 0)
  {
    accountNum = accNum;
    balance = bal;
  }
  else
  {
    accountNum = 0;
    balance = 0;
  }
}

bool customer::compare(double x, double y, double epsilon = 0.0000001f)
{
  if (fabs(x - y) < epsilon)
  {
    return true;
  }
  return false;
}

customer customer::operator+(customer c)
{
  customer local(this->balance + c.getBalance(), this->accountNum);

  return local;
}

customer& customer::operator+=(customer c)
{
  this->balance += c.getBalance();

  return *this;
}

customer customer::operator-(customer c)
{
  customer local(this->balance - c.getBalance(), this->accountNum);

  return local;
}

customer& customer::operator-=(customer c)
{
  this->balance -= c.getBalance();

  return *this;
}

bool customer::operator==(customer c)
{
  return compare(this->getBalance(), c.getBalance());
}

bool customer::operator!=(customer c)
{
  return !compare(this->getBalance(), c.getBalance());
}

bool customer::operator<(customer c)
{
  bool res = this->getBalance() < c.getBalance();
  return res;
}

bool customer::operator>(customer c)
{
  bool res = this->getBalance() > c.getBalance();
  return res;
}

bool customer::operator<=(customer c)
{
  bool res = this->getBalance() < c.getBalance();
  return res || compare(this->getBalance(), c.getBalance());
}

bool customer::operator>=(customer c)
{
  bool res = this->getBalance() > c.getBalance();
  return res || compare(this->getBalance(), c.getBalance());
}

bool customer::operator==(double num)
{
  return compare(this->getBalance(), num);
}

bool customer::operator!=(double num)
{
  return !compare(this->getBalance(), num);
}

bool customer::operator<(double num)
{
  return (this->getBalance() < num);
}

bool customer::operator>(double num)
{
  return (this->getBalance() > num);
}

bool customer::operator<=(double num)
{
  return (this->getBalance() < num) || compare(this->getBalance(), num);
}

bool customer::operator>=(double num)
{
  return (this->getBalance() >= num) || compare(this->getBalance(), num);
}

bool operator==(double num, customer c)
{
  return c == num;
}

bool operator!=(double num, customer c)
{
  return c != num;
}

bool operator<(double num, customer c)
{
  return c > num;
}

bool operator>(double num, customer c)
{
  return c < num;
}

bool operator<=(double num, customer c)
{
  return c >= num;
}

bool operator>=(double num, customer c)
{
  return c <= num;
}

// pre: customer must have account number
// post: customer balance may change
void customer::addMoney(int accNum, double money)
{
  if (accNum == accountNum) {
    balance += money;
  }
}

// pre: none
// post: customer balance may change
bool customer::buy(entree* item, unsigned quantity, int accNum)
{
  if (item != nullptr && accNum == accountNum)
  {
    if (quantity >= MEALLIMITDOWN && quantity <= MEALLIMITUP)
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

bool customer::buyOne(entree* item, int accNum)
{
  if (item != nullptr && accNum == accountNum)
  {
    if (!item->isSpoiled() && item->getQuantity() > 0 && item->getPrice() < balance)
    {
      balance -= item->getPrice();
      return true;
    }
  }
  return false;
}

double customer::getBalance()  // balance may be negative or positive
{
  return balance;

}

/* Implementation Invariants:
 * - Customer can only add money when account number is given,
 * - Customer can only buy items if their balance is enough to buy that item.
 * - Customer can only add or subtract another customer, it would break
 *   encapsulation if customer can add a number, the function addMoney
 *   already does that & ensure customer can add or deduct money (pass in
 *   negative number) with account number.
 * - Bool operators compare customers' balance and a given double balance.
 */