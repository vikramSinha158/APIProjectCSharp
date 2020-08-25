Feature: PAS BVT Functionality

@CommonMicroservice
Scenario Outline: Verify that common microservice is working properly
	Given Initialize Get Request For common microservice with URL "<GetURL>"
	When Add Headers for common microservice
	And Execute Get Request for common microservice Get All
	Then Status code should be for common microservice Get All "<GetSucessCode>"
	And Verify Server for common microservice Get All
	
	Examples:
	| GetURL		| GetSucessCode |
	| CommonGetAll  | 200           |

@GatewayMicroservice
Scenario Outline: Verify that Gateway microservice is working properly
	Given Initialize Get Request For gateway microservice with URL "<GetURL>"
	When Add Headers for gateway microservice
	And Execute Get Request for gateway microservice Get All
	Then Status code should be for gateway microservice Get All "<GetSucessCode>"
	And Verify Server for gateway microservice Get All
	
	Examples:
	| GetURL		| GetSucessCode |
	| GatewayGetAll | 200           |

@LOCMicroservice
Scenario Outline: Verify that LOC microservice is working properly
	Given User connects to facility database and runs the "UserID_SQL1" query to fetch userid from users table
	And User connects to facility database and runs the "Password_SQL1" query to fetch password from aspnet_Users table
	When Initialize Post Request For LOC Token with URL "<PostURL>"
	And Add Headers for LOC Token
	And Add Body for LOC Token
	Then Execute Post Request for LOC Token
	And Verify Status Code should be "<StatusCode>" for LOC Token
	And Save Token
	And Initialize Get Request For LOC microservice with URL "<GetURL>"
	And Add Headers for LOC microservice
	And Execute Get Request for LOC microservice Get All
	Then Verify Status Code should be "<StatusCodeLOC>" for LOC Get All
	And Verify Server for LOC microservice Get All
	Examples:
	| PostURL      | StatusCode | GetURL  | StatusCodeLOC |
	| LOCPostToken | 200        |LOCGetAll| 200           |