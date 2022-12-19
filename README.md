# BradyFramework

Task
Automate finding currency exchange rates in https://www.tradingview.com 

In a framework of your choice, implement a test that should:
1) Navigate to TradingView exchange rates at https://www.tradingview.com/markets/currencies/rates-all/
2) Sign-in to the application (You can create a trial user for the test)
3) Select “Asia” tab
4) Find the last exchange rate for currency pair GBPJPY 
5) Find the last three exchange rates for GBPJPY for a period of 1 minute (Extra points if provided)
6) Sign out

Please provide source code and tools used for above task

Additional question:
Are there any other test types you would suggest for above screen? Please provide brief description and example of such tests.

---
# Solution

Framework Details
| Automation Framework| |
| ------------- | ------------- |
| Tool  | Playwright |
|Proramming Language |c#|
| BDD  | Specflow  |
|IDE | Visual Studio|
| Test Framework | MSTest|
| Reporting | Allure |
| API framework| Playwright, RestSharp|

- Tests are written generic to extract rates for any currency. Currency can be provided through specflow examples
- code will capture current rate and any changes in exchange rate for configured time. Time can be changed from Tests.

# Additional question:
- Are there any other test types you would suggest for above screen? Please provide brief description and example of such tests.
# JK: We can do mutiple types of testing on this screen.
     - Functional Test
       - Tests related to calculations of Price, change% etc
       - Tests related to calcuate performances (3m,6m,12m)
       - Tests related to API which provides these data to current screen
     - Non Functional Test
       - compatibility test - cross browser
       - Performance tests - as rates are constanly changing and multiple API calls are made
 

## Execution:
- To run tests, use command "dotnet test 'path of solution or csproj'" from powershell/CMD/ VS Developer tools or open this project in VS and run through Test explorer
- Results are generated in .csv file under results folder. Each test will generate unique test csv file with rates.
- Each execution generate recorded video under videos folder. Video is recorded in headless mode too.


## Fetures:
- Super Fast Execution
- Can write tests in Gherkin format
- Execute test in headless mode
- Support API testing
- Generate Detailed report
- Parallel Execution
- Can be integrated with any Test managememt tool
- Easy Integration with Hangfire to schedule test remotely 
- Record each test and attach screenshots for failed tests in report automatically

## Execution Report:
After execution all execution results are generate in JSON files, these JSON files can be used to generate reports.

To generate reports, you need allure-commandline and java, add bin folder of allure and JAVA HOME in system path. 

Use following cmd commands to generate allure reports :

 - allure serve 'path of json files'
 
 - allure generate 'path of json file' -o 'path where reports needs to be generated'
 
 
 To Open existing execution report :
 
- allure open 'path of the report'

![image](https://user-images.githubusercontent.com/37189965/208331148-7b444e3c-467a-43fb-a831-7c69a8068eb2.png)
![image](https://user-images.githubusercontent.com/37189965/208331240-628fd985-7f4a-4c69-ae1c-07d265c819aa.png)
