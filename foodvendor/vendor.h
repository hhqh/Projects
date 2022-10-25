/* Author: Helen Huang
 * Date: 10/22/2021
 *
 * Overview: This header file lists all the declarations of variables
 * and function prototypes of the vendor class.
 */


#ifndef VENDOR_H
#define VENDOR_H
#include "entree.h"
#include "customer.h"


class vendor {
  const unsigned int SIZE = 10;
  entree * items = nullptr;
  unsigned int size;
  bool storeRefrigerated;
  unsigned int count = 0;

  void resize();
  void remove(unsigned int index, unsigned int quantity);
  int findIndex(entree* thing); //if found, return index of the entree thing,
  // if not return -1
  bool compare(entree* firstItem, entree* secondItem);

public:
  vendor(unsigned int entreeSize = 10, bool storeRefrigerate = false);
  vendor(const vendor& v);
  vendor& operator=(const vendor &rhs);
  vendor(vendor&& v);
  vendor& operator=(vendor&& rhs);
  ~vendor();

  //add operator
  vendor operator+(vendor &v); //vendor + vendor
  vendor operator+(int num); //vendor + num
  friend vendor operator+(int num, vendor &v); //num + vendor
  vendor& operator+=(vendor &v);   // vendor += vendor
  vendor& operator+=(int num);   // vendor += num
  vendor& operator++();  // pre ++
  vendor operator++(int num);  //post ++

  //subtract operator
  vendor operator-(vendor &v); //vendor - vendor
  vendor operator-(int num); //vendor - num
  friend vendor operator-(int num, vendor &v); //num - vendor
  vendor& operator-=(vendor &v);   // vendor -= vendor
  vendor& operator-=(int num);   // vendor -= num
  vendor& operator--();  // pre --
  vendor operator--(int num);  //post --

  //comparison operator /w vendor
  bool operator==(vendor v);
  bool operator!=(vendor v);
  bool operator<(vendor v);
  bool operator>(vendor v);
  bool operator<=(vendor v);
  bool operator>=(vendor v);

  //comparison operator /w int
  bool operator==(int num);
  bool operator!=(int num);
  bool operator<(int num);
  bool operator>(int num);
  bool operator<=(int num);
  bool operator>=(int num);

  //comparison operator int /w vendor
  friend bool operator==(int num, vendor &v);
  friend bool operator!=(int num, vendor &v);
  friend bool operator<(int num, vendor &v);
  friend bool operator>(int num, vendor &v);
  friend bool operator<=(int num, vendor &v);
  friend bool operator>=(int num, vendor &v);


  int getStockItemSize();
  int getSize();
  bool IsStocked(entree* thing);
  void Load(entree thing);
  void Sell(entree* thing, customer* person, int accNum, unsigned int
  quantity);
  void PowerOutage();
  void CleanStock();
};


#endif
