# XPMan EventSourcing blurb

EventSourcing is the best thing ever? 

In this session we'll discuss what EventSourcing is, its pros and cons, and some use-cases.

To make it real we'll be running through a demo EventSourced Checkout kata. 

Before the event:
 
 * You don't need to do anything :)

-----------

# Checkout Kata

Implement the code for a supermarket checkout that calculates the total price of a number of items.

The code will be used by an electronic till that 
 * can only scan one item at a time. 
 * sends a total to display as the items are scanned
 * can mark a basket as sold (any new items scanned are in a new basket)

Goods are priced individually, however there are weekly special offers for when multiple items are bought. For example "A is 50 each" or "3 for 130".

Weekly offers change frequently. The initial prices and offers are as follows:

| SKU         |   Item Price        |   Special Offers |
| ----------- | ------------------- | ---------------- |
| A           |   50                |   3 for 120      |
| B           |   30                |   2 for 45       |
| C           |   60                |                  |
| D           |   99                | 



<a rel="license" href="http://creativecommons.org/licenses/by-sa/4.0/"><img alt="Creative Commons Licence" style="border-width:0" src="https://i.creativecommons.org/l/by-sa/4.0/88x31.png" /></a><br />This work is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-sa/4.0/">Creative Commons Attribution-ShareAlike 4.0 International License</a>.