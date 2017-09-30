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

## To run

Currently open two terminals

 1) run `./docker.sh` (or have eventstore installed and running locally)
 2) run `make test`

## requirements

 * Event Store 4.0.3.0
 * .Net core 1.1