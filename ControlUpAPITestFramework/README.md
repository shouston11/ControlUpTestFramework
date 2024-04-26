Framework Approach
I created this framework using XUnit as the main test runner & HttpClient to send HTTP Requests.

Framework Structure
appsettings.json - this contains global settings that are passed into the framework such as URL & AccessKey.
ApiTestConfiguration.cs is utilised to read in the appsettings.json file and correspond to the correct model <ApiTestConfigurationModel>

HttpHelpers: 
ApiTestBase.cs - this class deals with setting up the HTTP Requests that we will want to call as part of out test cases. The methods in here
will control setting up request and passing through the required params required to make calls.
There are 3 methods in here:
MakeGetRequestAsyncList - Sets up async request and returns List response as a C# object using deserialise.
MakeGetRequestAsync - Sets up async request and returns response as a C# object using deserialise.
MakeApiCallAsync  - Called InitiateRequest to make the HTTP Call.

InitiateRequest.cs - This class sets up HTTP Client and passes in the required token etc to authorise a request. It controls passing in any required headers and makes
the actual async HTTP request. It will also do a StatusCode assertion check.

JSONHelpers:
JsonExtension.cs - contains 2 methods: FromJson & FromJsonList - these are used to deserialise any JSON response. This is primarily used to make asserting the 
response easier from a testcase. 

JsonSchemaValidate.cs - Added as an option to verify against a JSON Schema. Not Used at this time.

JsonSchema:
Contains GetAllTickerPriceChangeStatsResponse.json - Added as an example schema that could be passed in and asserted as part of API tests.

Models:
Contains all the API field models for the endpoints: ticker/24hr & /avgPrice. These Models will be utilised in testcases to verify responses etc.

Tests
All testcases live in here - the required endpoints, global variables and authentication will all be passed in here to allow successful requests. 

To Run Tests: Select View -> Test Explorer -> All tests can be executed from here.

CI: 
I Created a CI Pipeline in Azure Devops - the following .yml file can be found in the solution: AzureDevopsAPIIntegrationTests.yml.
The pipeline will complete the following steps:

1 - Checkout the codebase repo from github.
2 - Restore the repo
3 - Build the repo
4 - Run Tests
5 - Obtain Results





