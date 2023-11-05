Feature: Deposit

Scenario: Verify User Can Deposit Money Into Existing Account
  Given the user sends a PUT request to the "transaction/new" endpoint to deposit "10000" Amount to Account "ACCuser001" 
  Then the response status code should be 201
  And the user sends GET request to the "account/" endpoint to get "ACCuser001" account details
  And the response should contain Total Amount as "11000" 
  
Scenario: Verify User Can Not Deposit Money Into Non-Existing Account
  Given the user sends a PUT request to the "transaction/new" endpoint to deposit "10000" Amount to Account "ACCuserna" 
  Then the response status code should be 400

Scenario Outline: Verify Deposit Limits
  Given the user "<user>" sends a POST request to the "account/new" endpoint  
  And the user sends a PUT request to the "transaction/new" endpoint to deposit "<depositAmount>" Amount to Account "<AccountName>" 
  Then the response status code should be <responseCode>
  Examples: 
  | user        | AccountName |depositAmount  |responseCode  |  
  | user1       | ACCuser1001 | 10			  | 200          |
  | user2       | ACCuser2001 | 10000		  | 200          |
  | user3       | ACCuser3001 | 10001		  | 400          |
  | user4       | ACCuser4001 | 122221        | 400          |

