Feature: Simulate a Credit
	As a client
	I want to simulate a credit
	
Scenario: Simulate a credit
	When I simulate a credit with amount 1000, duration 12 months and interest rate 5%
	Then the monthly payment should be 85.61
	And the number of installments is 12

