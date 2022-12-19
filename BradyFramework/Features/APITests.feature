Feature: APITests

A short summary of the feature

@APIPlaywright
Scenario: API Test Sample using Playwright
	Given I call POST employee API 'api/v1/create'

@APIRestSharp
Scenario: API Test Sample using RestSharp
	Given I call GET employee API 'api/v1/employees'