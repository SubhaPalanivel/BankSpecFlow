Feature: Withdraw

Scenario: Verify User Can Withdraw Money from Existing Account
  Given the user sends a PUT request to the "transaction/new" endpoint to withdraw "1500" Amount from Account "ACCuser001" 
  Then the response status code should be 201
  And the user sends GET request to the "account/" endpoint to get "ACCuser001" account details
  And the response should contain balance Amount as "2000"

 Scenario: Verify User Can Not Withdraw Money from Non Existing Account
  Given the user sends a PUT request to the "transaction/new" endpoint to withdraw "1500" Amount from Account "ACCuserna" 
  Then the response status code should be 400

Scenario: Verify User Can Not Withdraw When After Transaction Balance Is Lessthan 100
  Given the user "Subha" sends a POST request to the "account/new" endpoint  
  And the user sends a PUT request to the "transaction/new" endpoint to deposit "5000" Amount to Account "ACCSubha001" 
  And the user sends a PUT request to the "transaction/new" endpoint to withdraw "5980" Amount from Account "ACCuser001" 
  Then the response status code should be 403

Scenario: Verify USer Can Not Withdraw More Than 90%
  Given the user "Subha" sends a POST request to the "account/new" endpoint  
  And the user sends a PUT request to the "transaction/new" endpoint to deposit "5000" Amount to Account "ACCSubha001" 
  And the user sends a PUT request to the "transaction/new" endpoint to withdraw "5450" Amount from Account "ACCuser001" 
  Then the response status code should be 403
