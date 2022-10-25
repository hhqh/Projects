/* Author: Helen Huang
 * Date: 10/22/2021
 *
 * Class Invariants:
 * - The size of the vendor is passed by constructor, as unsigned int type to
 *   prevent negative numbers and also the state of refrigerated is passed by
 *   and can't be manipulated.
 * - The items stored in the vendor class vary.
 *
 * Interface Invariants:
 * - Load function pass by an entree item containing the name, ingredients,
 *   nutrition facts, expiration date, if need to be refrigerated, price and
 *   quantity.
 * - sell function must have a valid customer with balance, or it will not
 *   sell items to the assigned customer.
 * - powerOutage function allows the refrigerated vendor to power off, and
 *   causes items in the vendor to be spoiled.
 * - cleanStock function clears all the expired and spoiled items
 * - isStock checks if the item is in the vendor.
 */


#include <iostream>
#include "vendor.h"
using namespace std;

vendor::vendor(unsigned int entreeSize, bool storeRefrigerate)
{
  if (entreeSize > 0) {
    size = entreeSize;
  }else{
    size = SIZE;
  }
  items = new entree[size];
  storeRefrigerated = storeRefrigerate;
}

vendor::vendor(const vendor& v)
{
  size = v.size;
  count = v.count;
  items = new entree[size];
  for (unsigned int i = 0; i < size; i++){
    items[i] = v.items[i];
  }
}

vendor& vendor::operator=(const vendor &rhs)
{
  if (this != &rhs){
    delete [] items;
    size = rhs.size;
    count = rhs.count;
    items = new entree[size];
    for (unsigned int i = 0; i < size; i++){
      items[i] = rhs.items[i];
    }
  }
  return *this;
}

vendor::vendor(vendor&& v)
{
  size = v.size;
  items = v.items;

  v.size = 0;
  v.items = nullptr;
}

vendor& vendor::operator=(vendor&& rhs)
{
  if (this != &rhs){
    delete [] items;
    size = rhs.size;
    items = rhs.items;
    rhs.size = 0;
    rhs.items = nullptr;
  }
  return *this;
}

vendor::~vendor()
{
  delete [] items;
}

vendor vendor::operator+(vendor &v)
{
  unsigned int newSize = getStockItemSize() + v.getStockItemSize();

  vendor local(newSize, storeRefrigerated);

  for (int i = 0; i < getStockItemSize(); i++){
    local.Load(items[i]);
  }

  for (int i = 0; i < v.getStockItemSize(); i++){
    local.Load(v.items[i]);
  }

  return local;
}

vendor vendor::operator+(int num)
{
  unsigned int newSize;
  if (num > 0){
    newSize = getStockItemSize() + num;
  }else{
    newSize = getStockItemSize();
  }

  vendor local(newSize, storeRefrigerated);

  return local;
}

vendor operator+(int num, vendor &v)
{
  return v + num;
}

vendor& vendor::operator+=(vendor &v)
{
  size = getStockItemSize() + v.getStockItemSize();

  return *this;
}

vendor& vendor::operator+=(int num)
{
  if (num > 0){
    size = getStockItemSize() + num;
  }else{
    size = getStockItemSize();
  }

  return *this;
}

vendor& vendor::operator++()
{
  size++;
  return *this;
}

vendor vendor::operator++(int num)
{
  unsigned int newSize = size + 1;

  vendor local(newSize, storeRefrigerated);

  return local;
}


vendor vendor::operator-(vendor &v)
{
  unsigned int newSize;

  if (v.getStockItemSize() < getStockItemSize()){
    newSize = getStockItemSize() - v.getStockItemSize();
  }else{
    newSize = SIZE;
  }

  vendor local(newSize, storeRefrigerated);

  for (unsigned int i = 0; i < newSize; i++){
    local.Load(items[i]);
  }

  return local;
}

vendor vendor::operator-(int num)
{
  unsigned int newSize;
  if (num < getStockItemSize()){
    newSize = getStockItemSize() - num;
  }else{
    newSize = SIZE;
  }

  vendor local(newSize, storeRefrigerated);

  return local;
}

vendor operator-(int num, vendor &v)
{
  return v - num;
}

vendor& vendor::operator-=(vendor &v)
{
  if (v.getStockItemSize() < getStockItemSize()){
    size = getStockItemSize() - v.getStockItemSize();
    count = size;
  }else{
    size = SIZE;
    count = 0;
  }

  return *this;
}

vendor& vendor::operator-=(int num)
{
  if (num < getStockItemSize()){
    size = getStockItemSize() - num;
    count = size;
  }else{
    size = SIZE;
    count = 0;
  }

  return *this;
}

vendor& vendor::operator--()
{
  if (size > 0) {
    size--;
    count = size;
  }else{
    size = SIZE;
    count = 0;
  }

  return *this;
}

vendor vendor::operator--(int num)
{
  unsigned int newSize;

  if (size > 0) {
    newSize = size - 1;
  }else{
    newSize = SIZE;
  }

  vendor local(newSize, storeRefrigerated);

  return local;
}

bool vendor::operator==(vendor v)
{
  return (getStockItemSize() == v.getStockItemSize());
}

bool vendor::operator!=(vendor v)
{
  return (getStockItemSize() != v.getStockItemSize());
}

bool vendor::operator<(vendor v)
{
  return (getStockItemSize() < v.getStockItemSize());
}

bool vendor::operator>(vendor v)
{
  return (getStockItemSize() > v.getStockItemSize());
}

bool vendor::operator<=(vendor v)
{
  return (getStockItemSize() <= v.getStockItemSize());
}

bool vendor::operator>=(vendor v)
{
  return (getStockItemSize() >= v.getStockItemSize());
}

bool vendor::operator==(int num)
{
  return (getStockItemSize() == num);
}

bool vendor::operator!=(int num)
{
  return (getStockItemSize() != num);
}

bool vendor::operator<(int num)
{
  return (getStockItemSize() < num);
}

bool vendor::operator>(int num)
{
  return (getStockItemSize() > num);
}

bool vendor::operator<=(int num)
{
  return (getStockItemSize() <= num);
}

bool vendor::operator>=(int num)
{
  return (getStockItemSize() >= num);
}

bool operator==(int num, vendor &v)
{
  return v == num;
}

bool operator!=(int num, vendor &v)
{
  return v != num;
}

bool operator<(int num, vendor &v)
{
  return v > num;
}

bool operator>(int num, vendor &v)
{
  return v < num;
}

bool operator<=(int num, vendor &v)
{
  return v >= num;
}

bool operator>=(int num, vendor &v)
{
  return v <= num;
}


// pre: vendor size exceeds after loading items
// post: size will change by two times
void vendor::resize()
{
  entree * temp = nullptr;
  unsigned int originalSize = size;
  size *= 2;
  temp = new entree[size];
  for (unsigned int i = 0; i < originalSize; i++){
    temp[i] = items[i];
  }
  delete [] items;
  items = temp;
}

// pre: item must be expired or spoiled
// post: size will decrease as item is removed
void vendor::remove(unsigned int index, unsigned int quantity)
{
  entree *temp = nullptr;

  if((items[index].getQuantity() == 1 && quantity == 1) ||(items[index].getQuantity() == quantity)) {
    count--;
    temp = new entree[count];
    for (unsigned int i = 0; i < index; i++) {
      temp[i] = items[i];
    }
    for (unsigned int j = index; j < count; j++) {
      temp[j] = items[j + 1];
    }
    delete[] items;
    items = temp;
  }else{
    unsigned int newQuantity = items[index].getQuantity() - quantity;
    items[index].setQuantity(newQuantity);
  }
}

bool vendor::compare(entree* firstItem, entree* secondItem)
{
  if (firstItem->getName() == secondItem->getName() &&
      firstItem->getExpireDate() == secondItem->getExpireDate() &&
      firstItem->getPrice() == secondItem->getPrice() &&
      firstItem->getQuantity() == secondItem->getQuantity()){
    return true;
  }
  return false;
}

int vendor::findIndex(entree* thing)
{
  for (unsigned int i = 0; i < count; i++){
    if (compare(thing, &items[i])){
      return i;
    }
  }
  return 0;
}

// pre: none
// post: vendor size may change
void vendor::Load(entree things)
{
  if(count == size){
    resize();
  }else{
    items[count] = things;
    count++;
  }
}

// pre: entree item must be passed
// post: may be true or false
bool vendor::IsStocked(entree* thing)
{
  int result = findIndex(thing);

  if (result >= 0){
    if(items[result].isSpoiled() || items[result].isExpired()){
      return false;
    }
    return true;
  }
  return false;
}

// pre: none
// post: vendor size may change
void vendor::Sell(entree *thing, customer *person, int accNum,
                  unsigned int quantity)
{
  int j;
  if (IsStocked(thing)){
    if (thing->getQuantity() >= quantity){
     if (person->buy(thing, quantity, accNum)){
       j = findIndex(thing);
       remove(j, quantity);
       for (unsigned int i = 0; i < quantity; i++){
         *thing = *thing + *thing;
       }
     }
    }
  }
}

// post: item status of powerOutage may change
void vendor::PowerOutage()
{
  for (unsigned int i = 0; i < count; i++){
    if (storeRefrigerated) {
      items[i].setPowerOutage();
    }
  }
}

// post: vendor size may change
void vendor::CleanStock()
{
  for (unsigned int i = 0; i < count; i++) {
    if (items[i].isExpired()) {
      remove(i, items[i].getQuantity());
    }
    else if(items[i].isSpoiled()){
      remove(i, items[i].getQuantity());
    }
  }
}

int vendor::getStockItemSize()
{
  return count;
}

int vendor::getSize()
{
  return size;
}

/* Implementation Invariants:
 *- When loading an item to the vendor, if the item container is full, the
 *  size of the item container will expand by 2 times.
 * - isStock uses private functions findIndex and compare to search for the
 *item in the vendor. The item is compared by name, price and quantity.
 * - Add operators for vendors are implemented: a vendor can add another
 * vendor (adds all the items of the other vendor), a vendor can add an int
 * (updates the size of the vendor).
 * - Subtract operators are in the same format as the add operators.
 * - Bool operators compare vendor to vendor and vendor to int and int to
 * vendor.
 */