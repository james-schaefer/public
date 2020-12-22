import decimal
import gmpy2
from gmpy2 import mpz

chars = 25
cols  = 80
rows  = 40
pages = 410
book  = cols * rows * pages

half_pages = 205
half_book = cols * rows * half_pages

prob2 = mpz('1')
prob = decimal.Decimal (1)

for i in range (1, 11):
    prob = prob * chars 
    prob2 = prob2 * chars 
    print ("{:07}".format(i), ": ", "{:,}".format(prob)) 
    if i == 6:
        print("           296,000,000           odds of winning powerball ")
        print("           331,916,588           us population ")

    if i == 7:
        print("           7,800,000,000         global population  ")
        print("           110,000,000,000       number of humans to have ever lived.")

for i in range (11, rows*cols+1 ):
    prob = prob * chars 
    prob2 = prob2 * chars 

    if i == 20:
        print("           7.00E+27               atoms in a human body ")
        print ("{:07}".format(i), ": ", "{:.2E}".format(prob)) 
    if i == 57:
        print ("{:07}".format(i), ": ", "{:.2E}".format(prob)) 
        print("           1.00E+80               atoms in the universe ")

    if i == cols:
        print ("{:07}".format(i), ": ", "{:.2E}".format(prob), "             One line of text in the book") 

    if i == (cols*rows):
        print ("{:07}".format(i), ": ", "{:.2E}".format(prob), "            One page in the book") 


for i in range (rows*cols+1, half_book+1 ):
    prob = prob * chars 
    prob2 = prob2 * chars 
    if i == half_book:
        print ("{:07}".format(i), ": ", "{:.2E}".format(prob), "          Half of one book") 


for i in range (half_book+1, book+1 ):
    prob2 = prob2 * chars 

print ("{:06}".format(book), ":                         One book") 
print ("")
print (prob2)
