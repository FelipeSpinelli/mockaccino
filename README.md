# Mr.Mime

A .NET framework for fill objects properties.

## Getting Started

These instructions will get you an overview to start filling your objects the way you need.

### Example

Let's suppose that you have a Customer class, like below, and needs to fill it's properties to test an response of an API. 
```
using System;

namespace MrMime.Core.Tests.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public short Age { get; set; }
        public DateTime? Birthday { get; set; }
        public bool IsActive { get; set; }
    }
}
```

After you refer Mr.Mime to your project, you will need to instantiate an object of class *** Mimenator *** and invoke the Load method to load the contracts. You can pass the path of the files, otherwise it will attempt to load the files from the Contracts folder, within the current directory, during the execution of the program.
```
var mimenator = new Mimenator();
mimenator.Load();
```
**Or**
```
var mimenator = new Mimenator();
mimenator.Load("<contracts-folder-path>");
```
Mr.Mime will look for files in the specified folder that end with .contract.json. (Ex .: ***Customer.contract.json***).
These files need to look like this:
```
{

    "contract_id": "D3087454-23A7-47B5-B7E4-5E1BA6BF15D0",
    "name": "Customer",
    "fields": [
        {
            "name": "CustomerId",
            "type": "Guid",
            "is_nullable": false,
            "fill_mode": "Random"
        },
        {
            "name": "Name",
            "type": "String",
            "is_nullable": false,
            "fill_mode": "Random"
        },
        {
            "name": "Age",
            "type": "Int16",
            "is_nullable": false,
            "fill_mode": "Random",
            "min_value": "18",
            "max_value": "70"
        },
        {
            "name": "Birthday",
            "type": "DateTime",
            "is_nullable": true,
            "fill_mode": "Random"
        },
        {
            "name": "IsActive",
            "type": "Boolean",
            "is_nullable": false,
            "fill_mode": "Fixed",
            "default_value": false
        }
    ]
}
```
After the contracts has been loaded, you just need to invoke the Imitate method, giving an object and the **contract *name* or *id***:
```
var customer = mimenator.Imitate(new Customer(), "Customer");
```
***Or***
```
var customer = mimenator.Imitate(new Customer(), Guid.Parse("D3087454-23A7-47B5-B7E4-5E1BA6BF15D0"));
```


## Prerequisites

The project depends on [Newtonsoft's Json.NET library](https://www.newtonsoft.com/json) and .NET Framework 4.5.1 version and above. 
