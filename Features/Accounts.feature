Feature: Account

Scenario: Create User Account with Default Amount successfully
  Given the user "user" sends a POST request to the "account/new" endpoint  
  Then the response status code should be 201
  And the response body should contain the AccountName as "ACCuser001" and TotalAmount as "1000"

Scenario: Create multiple Accounts for single User
  Given the user "user" sends a POST request to the "account/new" endpoint to create two account  
  Then the response status code should be 201
  And the response body should list two account details

Scenario: Delete valid Account
  Given the user sends a DELETE request to the "account" endpoint to delete Account "ACCuser001"  
  Then the response status code should be 200
  And the response body should be Empty

 Scenario: Delete invalid Account
  Given the user sends a DELETE request to the "account" endpoint to delete Account "ACCuser005"  
  Then the response status code should be 204
  And the response body should contain Error Message as "Account Not Found"

  
