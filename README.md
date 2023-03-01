# Mockaccino

A source generator that adds a mock controller.

## Getting Started

Let's suppose that your team need to provide a new endpoint. That endpoint will be consumed by front-end's team.
With Mockaccino they won't need to wait your team develop the new endpoint. 
You just need to define the payloads and the possible scenarios, put all togheter in a `json` file, and it's done. You and your team can now develop the real solution without pressure.

### Installing Mockaccino
You should install [Mockaccino with NuGet](https://www.nuget.org/packages/Mockaccino):

    Install-Package Mockaccino
    
Or via the .NET Core command line interface:

    dotnet add package Mockaccino

Either commands, from Package Manager Console or .NET Core CLI, will download and install Mockaccino and all required dependencies.

### Example
For this example let's suppose you need to develop two new endpoints:
```gherkin
Feature: Create a Resource
    Scenario Outline: Success
        Given I request to create a new Resource creation
        When It's a valid request
        Then I should get the created resource's id
        And I should get a response with 200 HttpStatusCode

    Scenario Outline: Bad Request
        Given I request a new Resource creation
        When It's Name is empty
        Then I should get an array with validations Messages
        And I should get a response with 400 HttpStatusCode
```
**AND**

```gherkin
Feature: Get a Resource By Id
    Scenario Outline: Success
        Given I request to get a Resource
        When I give an id which exists
        Then I should get the resource's data
        And I should get a response with 200 HttpStatusCode

    Scenario Outline: Not Found
        Given I request to get a Resource
        When I give an id which not exists
        Then I should get a response with 404 HttpStatusCode
```

Well, with those scenarios defined, now it's just add a json file, naming it as `mockaccino.settings.json`, and let ***Mockaccino*** do the job for you.
The file to attend the above scenarios must be like:
```json
[
  {
    "Name": "GetResourceById",
    "Method": "Get",
    "Route": "~/api/v1/resources/{id}",
    "Description": "It returns some response",
    "Responses": [
      {
        "Priority": 0,
        "StatusCode": 200,
        "Content": {
          "Name": "Joe Doe",
          "Age": 32
        },
        "Filter": {
          "From": "Route",
          "ApplyOn": "id",
          "WhenEqualsTo": "1"
        }
      },
      {
        "Priority": 1,
        "StatusCode": 404,
        "Content": [
          {
            "Message": "Resource not found"
          }
        ]
      }
    ]
  },
  {
    "Name": "CreateResource",
    "Method": "Post",
    "Route": "~/api/v1/resources",
    "Description": "",
    "Responses": [
      {
        "Priority": 0,
        "StatusCode": 400,
        "Content": [
          {
            "Message": "Name cannot be empty!"
          }
        ],
        "Filter": {
          "From": "Body",
          "ApplyOn": "Name",
          "WhenEqualsTo": ""
        }
      },
      {
        "Priority": 1,
        "StatusCode": 200,
        "Content": {
          "Id": "1"
        }
      }
    ]
  }
]

```
### Prerequisites

The project depends on [Newtonsoft's Json.NET library](https://www.newtonsoft.com/json) and must be compatible with `netstandard2.0`.


## References
https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md
https://github.com/amis92/csharp-source-generators
