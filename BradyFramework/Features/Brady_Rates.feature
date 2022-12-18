Feature: Brady_Rates
#Task
#Automate finding currency exchange rates in https://www.tradingview.com 
#
#In a framework of your choice, implement a test that should:
#1) Navigate to TradingView exchange rates at https://www.tradingview.com/markets/currencies/rates-all/
#2) Sign-in to the application (You can create a trial user for the test)
#3) Select “Asia” tab
#4) Find the last exchange rate for currency pair GBPJPY 
#5) Find the last three exchange rates for GBPJPY for a period of 1 minute (Extra points if provided)
#6) Sign out
#
#Please provide source code and tools used for above task
#
#Additional question:
#Are there any other test types you would suggest for above screen? Please provide brief description and example of such tests.

@ExtractRates @ExchangeRates
Scenario Outline: Extract Rates for currencies
	Given I navigate to TradingView exchange rates
	When I sign in to the application
	And I choose '<region>' tab
	And I find last exchange rate for currency pair '<currency>'
	And I find last '3' exchange rates for '<currency>' for a period of '1' minute
	And I sign out
Examples:
	| region | currency |
	| Asia   | GBPJPY   |
	| All    | USDJPY   |
	| Minor  | EURGBP   |
