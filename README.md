<table style="border: 0px none;">
  <tr>
    <td>
      <img src="./.template.config/icon.png" width="50">
    </td>
    <td style="vertical-align: middle;">
      <h1 style="border: 0px none; margin: 0; padding: 0;">Inspirato Basic API Template</h1>
    </td>
  </tr>
</table>

This is a very basic API template only with one health check endpoint. This solution can be run directly or used to install the template. In order to make this repo more maintainable, no complex template syntaxes are used. All template related configurations are in the `.template.config` folder.

There are two main objectives of this initiative:
* Quick creation and deployment of API
* Keep the repo easy to understand (without complex templating code) so that it's maintainable

---
## How can I use this repo to install the basic template?
* Pull this repo
* Run the following command:
  * `dotnet new install .` 

## How can I create a solution using the new basic API template?
* Run one of the following commands:
  * `dotnet new insp-basic-api` // from within the desired destinition folder
  * `dotnet new insp-basic-api -o <SOLUTION_NAME>`

## How can I view all the installed templates?
* Run the following command: 
  * `dotnet new list`

## How can I search templates?
* This can be done in different ways. Here are some examples:
  * `dotnet new list --tag inspirato`
  * `dotnet new list --author inspirato`
  * `dotnet new list insp`
  * `dotnet new list <SEARCH_KEY_WORD>`

## How can I uninstall this _Inspirato Basic API Template_ template?
* Run one of the following commands:
  * `dotnet new uninstall <PATH_TO_TEMPLATE_CONFIG_FOLDER>` // if you need to explicitly specify the path of the ".template.config" folder
  * `dotnet new uninstall .` // if you are at the solution file level

---
## Explanation of the `template.json` file:

```json
{
    "$schema": "http://json.schemastore.org/template",
    "author": "Inspirato Devs",
    // Used as search keyword -> $dotnet new list insp
    "classifications": [
        "insp", "webapi", "basic", "simple"  
    ],
    // A unique name for this template.
    "identity": "BasicTemplate.Api",
    "name": "Inspirato's Basic API Template",
    // shortName will be used with dotnet new command to create 
    "shortName": "insp-basic-api", 
    // sourceName will be replaced everywhere in the solution
    "sourceName": "BasicTemplate.Api",
    "preferNameDirectory": true,
    "tags": {
        "language": "C#",
        "type": "solution"
    }
}
```
---
## Useful Links:
* [dotnet new list](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new-list)
* [Custom templates for dotnet new](https://learn.microsoft.com/en-us/dotnet/core/tools/custom-templates)
* [Properties of template.json](https://learn.microsoft.com/en-us/dotnet/core/tools/custom-templates#templatejson)
* [File matching patterns reference](https://learn.microsoft.com/en-us/azure/devops/pipelines/tasks/file-matching-patterns?view=azure-devops#match-characters)