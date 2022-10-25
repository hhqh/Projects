/*
 * Author: Helen Huang
 * Date: 11/15/2021
 *
 * Overview: This program allows a vector of vendors to be created along with
 * its randomly made entree and customer (sub)objects (heterogenous
 * collection). The vector of vendors allows functions to be called and
 * tested with randomization. Below are the functions a vendor object can be
 * tested on:
 *
 * 1.load(entree item) - allows user to load entree item(s) to the vendor.
 * 2.sell(entree item, customer person) - allows the entree item(s) to be sold
 *   to a customer.
 * 3.powerOutage() - if the vendor is refrigerated then there is a power outage
 *   where food needed refrigeration is spoiled
 * 4.cleanStock() - throw out any expired or spoiled items in the vendor
 * It also tests the copy constructor, assignment operator, move semantics
 * using unique and shared pointers.
 *
 * Additional Tests:
 * - entree, vendor and customer (and its children) operators
 * (Addition, Subtraction, Comaprison)
 * - Total Nutrition after meal is purchased
 *
 * Assumptions: Assuming that valid inputs are entered through the object
 * constructors, if not, does check inputs.
 * Some functions can be tested by calling other functions that are calling
 * the function, like isStock() in vendor, vendor methods also calls out
 * customer and entree class methods.
 *
 */

#include <iostream>
#include <vector>
#include <cmath>
#include <memory>
#include "entree.h"
#include "vendor.h"
#include "carbCustomer.h"
#include "dbetCustomer.h"
#include "allergyCustomer.h"

using namespace std;

const int MAX = 30;
const int MAX_DOUBLE = 3000;
const int MAX_DOUBLE_BALANCE = 300000;
const int MAX_YEAR = 4;
const int MAX_DAY_MONTH = 2;
const int ONE = 1;
const int ACC_NUM = 893939;
const unsigned int SIZE = 8;
const unsigned int SIZE_NUTRI = 11;
const string VALID_DATE = "09/04/2024";
static const char ALPHANUM[] =
  "!@#$%^&*"
  "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
  "abcdefghijklmnopqrstuvwxyz";
static const char DATE[] =
  "0123456789";
const bool TRUE = true;
const bool FALSE = false;

void welcomeMessage();
void balanceMessage(customer *list[], int size);
void deallocateMessage();
void endMessage();
char randomChar();
string randomString();
string randomDateString();
double randomDouble();
double randomDoubleBalance();
int randomNumber();
customer* getCustomer(int num);
void populateStringArray(string arr1[], string arr2[], string arr3[]);
void populateIntArray(unsigned int arr1[], unsigned int arr2[],
                      unsigned int arr3[]);
void populateEntreeList(vector<entree> &list, entree& e1, entree& e2,
                       entree& e3, entree& e4, entree& e5);
void populateCustomerList(customer *list[], int size);
void populateVendorLists(vector<shared_ptr<vendor> > &shared,
                         vector<unique_ptr<vendor> > &unique,
                        shared_ptr<vendor> &v1, shared_ptr<vendor> &v2,
                        shared_ptr<vendor> &v3, unique_ptr<vendor> &v4,
                        unique_ptr<vendor> &v5, unique_ptr<vendor> &v6);
void addCustomerMoney(customer *list[], int size, int accNum, double money);
void loadVendorItems(vector<shared_ptr<vendor> > s,vector<unique_ptr<vendor> >& u,
                     vector<entree> items);
void sellVendorItems(vector<shared_ptr<vendor> > s,vector<unique_ptr<vendor> >& u,
                     customer *customerList[], vector<entree> &entreeList,
                     unsigned int quantity);
void cleanVendor(vector<shared_ptr<vendor> > s, vector<unique_ptr<vendor> > &u);
void removeVendorItem(vector<shared_ptr<vendor> > &s,
                      vector<unique_ptr<vendor> > &u);
void deepCopyCallByValue(entree &item);
void moveOperator();
void entreeOperatorTest(vector<entree> &list);
void vendorOperatorTest();
void customerOperatorTest(customer *list[]);
void printTotalNutrition(vector<entree> &entreeList);

int main() {

  srand(time(0));
  string ingreArr1[SIZE];
  string ingreArr2[SIZE];
  string ingreArr3[SIZE];
  unsigned int nutriArr1[SIZE_NUTRI];
  unsigned int nutriArr2[SIZE_NUTRI];
  unsigned int nutriArr3[SIZE_NUTRI];

  welcomeMessage();

  //Populating arrays
  populateStringArray(ingreArr1, ingreArr2, ingreArr3);
  populateIntArray(nutriArr1, nutriArr2, nutriArr3);

  //Making random entree objects and one entree that is not
  // spoiled/expired for testing
  entree e1 (randomString(), ingreArr1, nutriArr1, randomDateString(), FALSE,
             randomNumber(), randomDouble());
  entree e2 (randomString(), ingreArr2, nutriArr2, randomDateString(), TRUE,
             randomNumber(), randomDouble());
  entree e3 (randomString(), ingreArr3, nutriArr3, randomDateString(), TRUE,
             randomNumber(), randomDouble());
  entree e4 (randomString(), ingreArr2, nutriArr1, randomDateString(), FALSE,
             randomNumber(), randomDouble());
  entree e5 (randomString(), ingreArr2, nutriArr3, VALID_DATE, FALSE,
             randomNumber(), randomDouble());

  // Making vector of vendor shared pointers & vector of vendor unique pointers
  shared_ptr<vendor> v1(new vendor(randomNumber(), TRUE));
  shared_ptr<vendor> v2(new vendor(randomNumber(), FALSE));
  shared_ptr<vendor> v3(new vendor(randomNumber(), FALSE));
  unique_ptr<vendor> v4(new vendor(randomNumber(), TRUE));
  unique_ptr<vendor> v5(new vendor(randomNumber(), FALSE));
  unique_ptr<vendor> v6(new vendor(randomNumber(), FALSE));

  vector<shared_ptr<vendor> > sharedVendorList;
  vector<unique_ptr<vendor> > uniqueVendorList;
  vector<entree> entreeList;

  populateEntreeList(entreeList, e1, e2, e3, e4, e5);

  // Making random customer (sub)objects with one known balance for testing
  customer* customerList[SIZE_NUTRI];
  populateCustomerList(customerList, SIZE_NUTRI);

  // Testing entree operators
  entreeOperatorTest(entreeList);

  // Testing vendor operators
  vendorOperatorTest();

  // Testing customer operators
  customerOperatorTest(customerList);

  //add money to customers
  addCustomerMoney(customerList, SIZE_NUTRI, ACC_NUM, randomDoubleBalance());

  // Move constructor tested by pushing in vector
  populateVendorLists(sharedVendorList, uniqueVendorList, v1, v2, v3, v4, v5,
                      v6);

  // Loading entrees to v1, v2, v3;
  loadVendorItems(sharedVendorList, uniqueVendorList, entreeList);

  // Selling entree items to customer
  sellVendorItems(sharedVendorList, uniqueVendorList, customerList,
                  entreeList, ONE);
  balanceMessage(customerList, SIZE_NUTRI);

  // Printing total nutrition for meals, if purchase successfully will be
  // added, it not stays the same
  printTotalNutrition(entreeList);

  // Power outage & cleaning stock
  cleanVendor(sharedVendorList,uniqueVendorList);

  // Removing a vendor from list
  removeVendorItem(sharedVendorList,uniqueVendorList);

  // Testing move operator
  moveOperator();

  // Testing deep copy
  deepCopyCallByValue(e5);

  endMessage();

  return 0;
}

void welcomeMessage()
{
  cout << "Welcome to P4!\n";
  cout << "\nCreating & Populating data...\n" << endl;
}

void balanceMessage(customer* list[], int size)
{
  for (int i = 0; i < size; i++){
    cout << "Customer " << (i + 1) << "'s balance: "
         << list[i]->getBalance() << endl;
  }
  cout << endl;
}

void deallocateMessage()
{
  cout << "v4 has been deallocated! " << endl;
}

void endMessage()
{
  cout << "\nEnd of P4\n";
}

char randomChar()
{
  return ALPHANUM[rand() % MAX];
}

double randomDouble()
{
  double value = rand() % MAX_DOUBLE;
  return value /= 100;;
}

double randomDoubleBalance()
{
  double value = rand() % MAX_DOUBLE_BALANCE;
  return value /= 100;;
}

string randomString()
{
  string str;
  for (int i = 0; i < MAX; i++){
    str += randomChar();
  }
  return str;
}

string randomDateString()
{
  string year;
  string day;
  string month;

  for (int i = 0; i < MAX_YEAR; i++){
    year += DATE[rand() % MAX_YEAR];
  }

  for (int i = 0; i < MAX_DAY_MONTH; i++){
    day += DATE[rand() % MAX_DAY_MONTH];
  }

  for (int i = 0; i < MAX_DAY_MONTH; i++){
    month += DATE[rand() % MAX_DAY_MONTH];
  }
  return month +"/" + day + "/" + year;
}

int randomNumber()
{
  return rand() % MAX;
}

void populateStringArray(string arr1[], string arr2[], string arr3[])
{
  for (unsigned int i = 0; i < SIZE; i++){
    arr1[i] = randomString();
  }

  for (unsigned int i = 0; i < SIZE; i++){
    arr2[i] = randomString();
  }

  for (unsigned int i = 0; i < SIZE; i++){
    arr3[i] = randomString();
  }
}

void populateIntArray(unsigned int arr1[], unsigned int arr2[],
                      unsigned int arr3[])
{
  for (unsigned int i = 0; i < SIZE_NUTRI; i++){
    arr1[i] = randomNumber();
  }

  for (unsigned int i = 0; i < SIZE_NUTRI; i++){
    arr2[i] = randomNumber();
  }

  for (unsigned int i = 0; i < SIZE_NUTRI; i++){
    arr2[i] = randomNumber();
  }
}

void addCustomerMoney(customer *list[], int size, int accNum, double money)
{
  cout << "\nTesting the customer addMoney function..." << endl;

  balanceMessage(list, SIZE_NUTRI);

  for (int i = 0; i < size; i++){
    list[i]->addMoney(accNum, money);
  }

  balanceMessage(list, SIZE_NUTRI);
}

void populateEntreeList(vector<entree> &list, entree& e1, entree& e2,
                        entree& e3, entree& e4, entree& e5){
  list.push_back(e1);
  list.push_back(e2);
  list.push_back(e3);
  list.push_back(e4);
  list.push_back(e5);
}

void populateVendorLists(vector<shared_ptr<vendor> > &shared,
                         vector<unique_ptr<vendor> > &unique,
                         shared_ptr<vendor> &v1, shared_ptr<vendor> &v2,
                         shared_ptr<vendor> &v3, unique_ptr<vendor> &v4,
                         unique_ptr<vendor> &v5, unique_ptr<vendor> &v6){
  cout << "\nTesting the move constructor by pushing vendors to vector..."
       << endl;
  shared.push_back(v1);
  shared.push_back(v2);
  shared.push_back(v3);

  unique.push_back(move(v4));
  unique.push_back(move(v5));
  unique.push_back(move(v6));
}

void printTotalNutrition(vector<entree> &entreeList)
{
  cout << "\nPrinting total nutrition for meals..." << endl;
  cout << "\nTotal Nutrition for Meal 1" << endl;

  for (unsigned int i = 0; i < SIZE_NUTRI; i++){
    cout << entreeList[0].findNutritionFacts(i) << " " << endl;
  }

  cout << "\nTotal Nutrition for Meal 2" << endl;

  for (unsigned int i = 0; i < SIZE_NUTRI; i++){
    cout << entreeList[1].findNutritionFacts(i) << " " << endl;
  }

}

void loadVendorItems(vector<shared_ptr<vendor> > s,
                     vector<unique_ptr<vendor> > &u, vector<entree> items)
{
  cout << "\nLoading the entree items in vendors..." << endl;
  s[0]->Load(items[0]);
  s[0]->Load(items[1]);
  s[0]->Load(items[2]);
  s[0]->Load(items[3]);
  s[0]->Load(items[4]);

  s[1]->Load(items[0]);
  s[1]->Load(items[1]);
  s[1]->Load(items[3]);
  s[1]->Load(items[4]);

  s[2]->Load(items[0]);
  s[2]->Load(items[2]);
  s[2]->Load(items[4]);

  u[0]->Load(items[0]);
  u[0]->Load(items[1]);
  u[0]->Load(items[2]);
  u[0]->Load(items[3]);
  u[0]->Load(items[4]);

  u[1]->Load(items[0]);
  u[1]->Load(items[1]);
  u[1]->Load(items[3]);
  u[1]->Load(items[4]);

  u[2]->Load(items[0]);
  u[2]->Load(items[2]);
  u[2]->Load(items[4]);
}

void sellVendorItems(vector<shared_ptr<vendor> > s,vector<unique_ptr<vendor> >& u,
                     customer *customerList[], vector<entree> &entreeList,
                     unsigned int quantity){
  cout << "\nSelling the entree items in vendors to customers..." << endl;

  for (unsigned int j = 0; j < SIZE_NUTRI; j++)
  {
    s[0]->Sell(&entreeList[0], customerList[j], ACC_NUM, quantity);
  }

  for (unsigned int j = 0; j < SIZE_NUTRI; j++)
  {
    s[1]->Sell(&entreeList[1], customerList[j], ACC_NUM, quantity);
  }

  for (unsigned int j = 0; j < SIZE_NUTRI; j++)
  {
    u[0]->Sell(&entreeList[2], customerList[j], ACC_NUM, quantity);
  }

  for (unsigned int j = 0; j < SIZE_NUTRI; j++)
  {
    u[1]->Sell(&entreeList[4], customerList[j], ACC_NUM, quantity);
  }

  for (unsigned int j = 0; j < SIZE_NUTRI; j++)
  {
    u[2]->Sell(&entreeList[4], customerList[j], ACC_NUM, quantity);
  }
}

void cleanVendor(vector<shared_ptr<vendor> > s, vector<unique_ptr<vendor> > &u)
{
  cout << "\nTesting power outage and clean stock function..." << endl;

  for (int i = 0; i < (int)s.size(); i++){
    cout << "(Shared) Vendor " << i+1 << "'s number of in stock items: "
         << s[i]->getStockItemSize() << endl;
  }

  for (int i = 0; i < (int)u.size(); i++){
    cout << "(Unique) Vendor " << i+1 << "'s number of in stock items: "
         << u[i]->getStockItemSize() << endl;
  }

  for (int i = 0; i < (int)s.size(); i++) {
    s[i]->PowerOutage();
  }

  for (int i = 0; i < (int)u.size(); i++){
    u[i]->PowerOutage();
  }

  for (int i = 0; i < (int)s.size(); i++){
    s[i]->CleanStock();
  }

  for (int i = 0; i < (int)u.size(); i++){
    u[i]->CleanStock();
  }

  cout << endl;

  for (int i = 0; i < (int)s.size(); i++){
    cout << "(Shared) Vendor " << i+1 << "'s number of in stock items: "
         << s[i]->getStockItemSize() << endl;
  }

  for (int i = 0; i < (int)u.size(); i++){
    cout << "(Unique) Vendor " << i+1 << "'s number of in stock items: "
         << u[i]->getStockItemSize() << endl;
  }
}

void removeVendorItem(vector<shared_ptr<vendor> > &s,
                      vector<unique_ptr<vendor> > &u) {
  cout << "\nRemoving vendor item from list" << endl;
  s.pop_back();
  u.pop_back();

  cout << "(Shared) Vendor List Size: " << s.size() << endl;
  cout << "(Unique) Vendor List Size: " << u.size() << endl;
}

void getVendorSize(shared_ptr<vendor> v)
{
  cout << "Vendor Size: " << v->getStockItemSize() << endl;
}

void deepCopyCallByValue(entree& item)
{
  cout << "\nTesting copy constructor by copying the first vendor to the second"
          "..." << endl;
  vendor v1(SIZE, FALSE);
  v1.Load(item);
  cout << "Vendor Size of First Vendor: " << v1.getStockItemSize() << endl;
  vendor v2(v1);
  cout << "Vendor Size of Second Vendor: " << v2.getStockItemSize() << endl;

  cout << "Testing assignment operator by assigning third vendor to the first"
          " vendor.." << endl;
  vendor v3;
  v3 = v1;
  cout << "Vendor Size of Third Vendor: " << v2.getStockItemSize() << endl;
}

void moveOperator()
{
  cout << "\nTesting the move assignment operator by moving v4 to v5..."
       << endl;
  unique_ptr<vendor> v4(new vendor(randomNumber(), FALSE));
  unique_ptr<vendor> v5;
  v5 = move(v4);

  if (!v4){
    deallocateMessage();
  }
}

customer* getCustomer(int num)
{
  bool random = (num < randomNumber());

  if (num % 4 == 0){
    return new customer(randomDoubleBalance(), ACC_NUM);
  }else if (num % 4 == 1){
    return new carbCustomer(randomDoubleBalance(), ACC_NUM, randomNumber());
  }else if (num % 4 == 2){
    return new dbetCustomer(randomDoubleBalance(), ACC_NUM);
  }else{
    return new allergyCustomer(randomDoubleBalance(), ACC_NUM, random,
                               randomString());
  }
}

void populateCustomerList(customer *list[], int size)
{
  for (int i = 0; i < size; i++){
    list[i] = getCustomer(randomNumber());
  }
}

void entreeOperatorTest(vector<entree> &list)
{
  cout << "\nTesting the entree operators..." << endl;
  cout << "\n-------------------------Addition------------------------" << endl;
  cout << "operator+(entree): " << endl;

  cout << "\nNutrition Facts for Entree 1: " << endl;
  for (unsigned int i = 0; i < SIZE_NUTRI; i++){
    cout << list[0].findNutritionFacts(i) << " ";
  }
  cout << "\nPrice for Entree 1: " << list[0].getPrice() << endl;

  cout << "\nNutrition Facts for Entree 2: " << endl;
  for (unsigned int i = 0; i < SIZE_NUTRI; i++){
    cout << list[1].findNutritionFacts(i) << " ";
  }
  cout << "\nPrice for Entree 2: " << list[1].getPrice() << endl;

  entree res1 = list[0] + list[1];

  cout << "\nNutrition Facts for Entree 1 + Entree 2: " << endl;
  for (unsigned int i = 0; i < SIZE_NUTRI; i++){
    cout << res1.findNutritionFacts(i) << " ";
  }
  cout << "\nPrice for Entree 1 + Entree 2: " << res1.getPrice() << endl;

  cout << "\n\noperator+(double): " << endl;
  entree res2 = list[0] + randomDouble();
  cout << "\nPrice for Entree 1 + random double: "<< res2.getPrice() << endl;

  cout << "\n\noperator+(double, entree): " << endl;
  entree res3 = randomDouble() + list[1];
  cout << "\nPrice for random double + Entree 2: "<< res3.getPrice() << endl;

  cout << "\n\noperator+=(entree): " << endl;
  list[0] += list[1];
  cout << "\nNutrition Facts for Entree 1: " << endl;
  for (unsigned int i = 0; i < SIZE_NUTRI; i++){
    cout << list[0].findNutritionFacts(i) << " ";
  }
  cout << "\nPrice for Entree 1 += Entree 2: " << list[0].getPrice() << endl;

  cout << "\n\noperator+=(double): " << endl;
  list[0] += randomDouble();
  cout << "\nPrice for Entree 1 += random double: " << list[0].getPrice() <<
  endl;

  cout << "\n\noperator++(): " << endl;
  ++list[0];
  cout << "\nPrice for ++Entree 1: " << list[0].getPrice() <<
       endl;

  cout << "\n\noperator++(int): " << endl;
  entree res4 = list[0]++;
  cout << "\nPrice for Entree 1++: " << res4.getPrice() << endl;

  // Subtraction---
  cout << "\n----------------------Subtraction------------------------" << endl;
  cout << "operator-(entree): " << endl;

  cout << "\nNutrition Facts for Entree 1: " << endl;
  for (unsigned int i = 0; i < SIZE_NUTRI; i++){
    cout << list[0].findNutritionFacts(i) << " ";
  }
  cout << "\nPrice for Entree 1: " << list[0].getPrice() << endl;

  cout << "\nNutrition Facts for Entree 2: " << endl;
  for (unsigned int i = 0; i < SIZE_NUTRI; i++){
    cout << list[1].findNutritionFacts(i) << " ";
  }
  cout << "\nPrice for Entree 2: " << list[1].getPrice() << endl;

  entree res5 = list[0] - list[1];

  cout << "\nNutrition Facts for Entree 1 - Entree 2: " << endl;
  for (unsigned int i = 0; i < SIZE_NUTRI; i++){
    cout << res5.findNutritionFacts(i) << " ";
  }
  cout << "\nPrice for Entree 1 - Entree 2: " << res5.getPrice() << endl;

  cout << "\n\noperator-(double): " << endl;
  entree res6 = list[0] - randomDouble();
  cout << "\nPrice for Entree 1 - random double: "<< res6.getPrice() << endl;

  cout << "\n\noperator-(double, entree): " << endl;
  entree res7 = randomDouble() - list[1];
  cout << "\nPrice for random double - Entree 2: "<< res7.getPrice() << endl;

  cout << "\n\noperator-=(entree): " << endl;
  list[0] -= list[1];
  cout << "\nNutrition Facts for Entree 1: " << endl;
  for (unsigned int i = 0; i < SIZE_NUTRI; i++){
    cout << list[0].findNutritionFacts(i) << " ";
  }
  cout << "\nPrice for Entree 1 -= Entree 2: " << list[0].getPrice() << endl;

  cout << "\n\noperator-=(double): " << endl;
  list[0] -= randomDouble();
  cout << "\nPrice for Entree 1 -= random double: " << list[0].getPrice() <<
       endl;

  cout << "\n\noperator--(): " << endl;
  --list[0];
  cout << "\nPrice for --Entree 1: " << list[0].getPrice() <<
       endl;

  cout << "\n\noperator--(int): " << endl;
  entree res8 = list[0]--;
  cout << "\nPrice for Entree 1++: " << res8.getPrice() << endl;

  //Bool------------
  cout << "\n-----------------------Comparison-----------------------" << endl;
  cout << "\noperator==(entree): " << endl;
  cout << "Entree 1 == Entree 2\tResult: " << boolalpha << (list[0] == list[1]);

  cout << "\n\noperator!=(entree): " << endl;
  cout << "Entree 1 != Entree 2\tResult: " << boolalpha << (list[0] != list[1]);

  cout << "\n\noperator<=(entree): " << endl;
  cout << "Entree 1 <= Entree 2\tResult: " << boolalpha << (list[0] <= list[1]);

  cout << "\n\noperator>=(entree): " << endl;
  cout << "Entree 1 >= Entree 2\tResult: " << boolalpha << (list[0] >= list[1]);

  cout << "\n\noperator<(entree): " << endl;
  cout << "Entree 1 < Entree 2\t\tResult: " << boolalpha << (list[0] < list[1]);

  cout << "\n\noperator>(entree): " << endl;
  cout << "Entree 1 > Entree 2\t\tResult: " << boolalpha << (list[0] > list[1]);


  cout << "\n\noperator==(double): " << endl;
  cout << "Entree 1 == double\t\tResult: " << boolalpha
       << (list[0] == randomDouble());

  cout << "\n\noperator!=(double): " << endl;
  cout << "Entree 1 != double\t\tResult: " << boolalpha
       << (list[0] != randomDouble());

  cout << "\n\noperator<=(double): " << endl;
  cout << "Entree 1 <= double\t\tResult: " << boolalpha
       << (list[0] <= randomDouble());

  cout << "\n\noperator>=(double): " << endl;
  cout << "Entree 1 >= double\t\tResult: " << boolalpha
       << (list[0] >= randomDouble());

  cout << "\n\noperator<(double): " << endl;
  cout << "Entree 1 < double\t\tResult: " << boolalpha
       << (list[0] < randomDouble ());

  cout << "\n\noperator>(double): " << endl;
  cout << "Entree 1 > double\t\tResult: " << boolalpha
       << (list[0] > randomDouble());

  cout << "\n\noperator==(double, entree): " << endl;
  cout << "double == Entree 1\t\tResult: " << boolalpha
       << (randomDouble() == list[0]);

  cout << "\n\noperator!=(double, entree): " << endl;
  cout << "double != Entree 1\t\tResult: " << boolalpha
       << (randomDouble() != list[0]);

  cout << "\n\noperator<=(double, entree): " << endl;
  cout << "double <= Entree 1\t\tResult: " << boolalpha
       << (randomDouble() <= list[0]);

  cout << "\n\noperator>=(double, entree): " << endl;
  cout << "double >= Entree 1\t\tResult: " << boolalpha
       << (randomDouble() >= list[0]);

  cout << "\n\noperator<(double, entree): " << endl;
  cout << "double < Entree 1\t\tResult: " << boolalpha
       << (randomDouble() < list[0]);

  cout << "\n\noperator>(double, entree): " << endl;
  cout << "double > Entree 1\t\tResult: " << boolalpha
       << (randomDouble() > list[0]);

  cout << endl << endl;
}

void vendorOperatorTest(){

  entree e1(randomString(), nullptr, nullptr, randomDateString(), FALSE,
            randomNumber(), randomDouble());
  entree e2(randomString(), nullptr, nullptr, randomDateString(), FALSE,
            randomNumber(), randomDouble());

  vendor v1(randomNumber(), TRUE);
  vendor v2(randomNumber(), FALSE);

  v1.Load(e1);
  v1.Load(e2);
  v2.Load(e1);

  cout << "\nTesting the vendor operators..." << endl;
  cout << "\n-------------------------Addition------------------------" << endl;
  cout << "operator+(vendor): " << endl;

  cout << "\n# of Stock Items for Vendor 1: " << v1.getStockItemSize() <<
  endl;

  cout << "\n# of Stock Items for Vendor 2: " << v2.getStockItemSize() <<
       endl;

  vendor res1 = v1 + v2;

  cout << "\n# of Stock Items for Vendor 1 + Vendor 2: "
       << res1.getStockItemSize() << endl;

  cout << "\n\noperator+=(vendor): " << endl;
  v1 += v2;
  cout << "\n# of Stock Items for Vendor 1 += Vendor 2: "
       << v1.getStockItemSize() << endl;

  cout << "\n\noperator+(int): " << endl;
  vendor res2 = v1 + randomNumber();
  cout << "\nSize for Vendor 1: " << v1.getSize() << endl;
  cout << "\nSize for Vendor 1 + int: " << res2.getSize() << endl;

  cout << "\n\noperator+(int, vendor): " << endl;
  vendor res3 = randomNumber() + v1;
  cout << "\nSize for int + Vendor 1: "<< res3.getSize() << endl;

  cout << "\n\noperator+=(int): " << endl;
  v1 += randomNumber();
  cout << "\nSize for Vendor 1 += int: " << v1.getSize() << endl;

  cout << "\n\noperator++(): " << endl;
  ++v1;
  cout << "\nSize for ++Vendor 1: " << v1.getSize() << endl;

  cout << "\n\noperator++(int): " << endl;
  vendor res4 = v1++;
  cout << "\nSize for Vendor 1++: " << res4.getSize() << endl;

  // Subtraction---
  cout << "\n----------------------Subtraction------------------------" << endl;
  cout << "operator-(vendor): " << endl;

  cout << "\n# of Stock Items for Vendor 1: " << v1.getStockItemSize() <<
       endl;

  cout << "\n# of Stock Items for Vendor 2: " << v2.getStockItemSize() <<
       endl;

  vendor res5 = v1 - v2;

  cout << "\n# of Stock Items for Vendor 1 - Vendor 2: "
       << res5.getStockItemSize() << endl;

  cout << "\n\noperator-=(vendor): " << endl;
  v1 -= v2;
  cout << "\n# of Stock Items for Vendor 1 -= Vendor 2: "
       << v1.getStockItemSize() << endl;

  cout << "\n\noperator-(int): " << endl;
  vendor res6 = v1 - randomNumber();
  cout << "\nSize for Vendor 1: " << v1.getSize() << endl;
  cout << "\nSize for Vendor 1 - int: " << res6.getSize() << endl;

  cout << "\n\noperator-(int, vendor): " << endl;
  vendor res7 = randomNumber() - v1;
  cout << "\nSize for int - Vendor 1: "<< res7.getSize() << endl;

  cout << "\n\noperator-=(int): " << endl;
  v1 -= randomNumber();
  cout << "\nSize for Vendor 1 -= int: " << v1.getSize() << endl;

  cout << "\n\noperator--(): " << endl;
  --v1;
  cout << "\nSize for --Vendor 1: " << v1.getSize() << endl;

  cout << "\n\noperator--(int): " << endl;
  vendor res8 = v1--;
  cout << "\nSize for Vendor 1--: " << res8.getSize() << endl;

  //Bool------------
  cout << "\n-----------------------Comparison-----------------------" << endl;
  cout << "\noperator==(vendor): " << endl;
  cout << "Vendor 1 == Vendor 2\tResult: " << boolalpha << (v1 == v2);

  cout << "\n\noperator!=(vendor): " << endl;
  cout << "Vendor 1 != Vendor 2\tResult: " << boolalpha << (v1 != v2);

  cout << "\n\noperator<=(vendor): " << endl;
  cout << "Vendor 1 <= Vendor 2\tResult: " << boolalpha << (v1 <= v2);

  cout << "\n\noperator>=(vendor): " << endl;
  cout << "Vendor 1 >= Vendor 2\tResult: " << boolalpha << (v1 >= v2);

  cout << "\n\noperator<(vendor): " << endl;
  cout << "Vendor 1 < Vendor 2\t\tResult: " << boolalpha << (v1 < v2);

  cout << "\n\noperator>(vendor): " << endl;
  cout << "Vendor 1 > Vendor 2\t\tResult: " << boolalpha << (v1 > v2);


  cout << "\n\noperator==(int): " << endl;
  cout << "Vendor 1 == int\t\tResult: " << boolalpha << (v1 == randomNumber());

  cout << "\n\noperator!=(int): " << endl;
  cout << "Vendor 1 != int\t\tResult: " << boolalpha << (v1 != randomNumber());

  cout << "\n\noperator<=(int): " << endl;
  cout << "Vendor 1 <= int\t\tResult: " << boolalpha << (v1 <= randomNumber());

  cout << "\n\noperator>=(int): " << endl;
  cout << "Vendor 1 >= int\t\tResult: " << boolalpha << (v1 >= randomNumber());

  cout << "\n\noperator<(int): " << endl;
  cout << "Vendor 1 < int\t\tResult: " << boolalpha << (v1 < randomNumber());

  cout << "\n\noperator>(int): " << endl;
  cout << "Vendor 1 > int\t\tResult: " << boolalpha << (v1 > randomNumber());


  cout << "\n\noperator==(int, vendor): " << endl;
  cout << "int == Vendor 1\t\tResult: " << boolalpha << (randomNumber() == v1);

  cout << "\n\noperator!=(int, vendor): " << endl;
  cout << "int != Vendor 1\t\tResult: " << boolalpha << (randomNumber() != v1);

  cout << "\n\noperator<=(int, vendor): " << endl;
  cout << "int <= Vendor 1\t\tResult: " << boolalpha << (randomNumber() <= v1);

  cout << "\n\noperator>=(int, vendor): " << endl;
  cout << "int >= Vendor 1\t\tResult: " << boolalpha << (randomNumber() >= v1);

  cout << "\n\noperator<(int, vendor): " << endl;
  cout << "int < Vendor 1\t\tResult: " << boolalpha << (randomNumber() < v1);

  cout << "\n\noperator>(int, vendor): " << endl;
  cout << "int > Vendor 1\t\tResult: " << boolalpha << (randomNumber() > v1);

  cout << endl << endl;
}

void customerOperatorTest(customer *list[])
{
  cout << "\nTesting the customer & its subtypes operators..." << endl;
  cout << "\n-------------------------Addition------------------------" << endl;
  cout << "operator+(customer): " << endl;

  cout << "\nBalance for Customer 1: " << list[0]->getBalance() << endl;
  cout << "\nBalance for Customer 2: " << list[1]->getBalance() << endl;

  customer res1 = *list[0] + *list[1];

  cout << "\nBalance for Customer 1 + Customer 2: " << res1.getBalance() <<
  endl;

  cout << "\n\noperator+=(customer): " << endl;
  *list[0] += *list[1];
  cout << "\nBalance for Customer 1 += Customer 2: " << list[0]->getBalance()
  << endl;

  // Subtraction---
  cout << "\n----------------------Subtraction------------------------" << endl;
  cout << "operator-(customer): " << endl;

  customer res2 = *list[0] - *list[1];

  cout << "\nBalance for Customer 1 - Customer 2: " << res2.getBalance() <<
       endl;

  cout << "\n\noperator-=(customer): " << endl;
  *list[0] -= *list[1];
  cout << "\nBalance for Customer 1 -= Customer 2: " << list[0]->getBalance()
       << endl;

  //Bool------------
  cout << "\n-----------------------Comparison-----------------------" << endl;
  cout << "\noperator==(customer): " << endl;
  cout << "Customer 1 == Customer 2\tResult: " << boolalpha << (*list[0] ==
  *list[1]);

  cout << "\n\noperator!=(customer): " << endl;
  cout << "Customer 1 != Customer 2\tResult: " << boolalpha << (*list[0] !=
  *list[1]);

  cout << "\n\noperator<=(customer): " << endl;
  cout << "Customer 1 <= Customer 2\tResult: " << boolalpha << (*list[0] <=
  *list[1]);

  cout << "\n\noperator>=(customer): " << endl;
  cout << "Customer 1 >= Customer 2\tResult: " << boolalpha << (*list[0] >=
  *list[1]);

  cout << "\n\noperator<(customer): " << endl;
  cout << "Customer 1 < Customer 2\t\tResult: " << boolalpha << (*list[0] <
  *list[1]);

  cout << "\n\noperator>(customer): " << endl;
  cout << "Customer 1 > Customer 2\t\tResult: " << boolalpha << (*list[0] >
  *list[1]);


  cout << "\n\noperator==(double): " << endl;
  cout << "Customer 1 == double\t\tResult: " << boolalpha << (*list[0] ==
  randomDouble());

  cout << "\n\noperator!=(double): " << endl;
  cout << "Customer 1 != double\t\tResult: " << boolalpha << (*list[0] ==
  randomDouble());

  cout << "\n\noperator<=(double): " << endl;
  cout << "Customer 1 <= double\t\tResult: " << boolalpha << (*list[0] <=
  randomDouble());

  cout << "\n\noperator>=(double): " << endl;
  cout << "Customer 1 >= double\t\tResult: " << boolalpha << (*list[0] >=
  randomDouble());

  cout << "\n\noperator<(double): " << endl;
  cout << "Customer 1 < double\t\tResult: " << boolalpha << (*list[0] <
  randomDouble());

  cout << "\n\noperator>(double): " << endl;
  cout << "Customer 1 > double\t\tResult: " << boolalpha << (*list[0] >
  randomDouble());


  cout << "\n\noperator==(double, customer): " << endl;
  cout << "double == Customer 1\t\tResult: " << boolalpha << (randomDouble() ==
  *list[0]);

  cout << "\n\noperator!=(double, customer): " << endl;
  cout << "double != Customer 1\t\tResult: " << boolalpha << (randomDouble() !=
  *list[0]);

  cout << "\n\noperator<=(double, customer): " << endl;
  cout << "double <= Customer 1\t\tResult: " << boolalpha << (randomDouble() <=
  *list[0]);

  cout << "\n\noperator>=(double, customer): " << endl;
  cout << "double >= Customer 1\t\tResult: " << boolalpha << (randomDouble() >=
  *list[0]);

  cout << "\n\noperator<(double, customer): " << endl;
  cout << "double < Customer 1\t\tResult: " << boolalpha << (randomDouble() <
  *list[0]);

  cout << "\n\noperator>(double, customer): " << endl;
  cout << "double > Customer 1\t\tResult: " << boolalpha << (randomDouble() >
  *list[0]);

  cout << endl << endl;
}