# Recipe Book
A tool for managing a collection of recipes

# Requirements
* .NET Core 3.2 or higher

# Projects
## Application
|Project|Type|Purpose|
|---|---|---|
|recipebook.functions|Azure Function|API layer for the application|
|recipebook.blazor|Blazor WebAssembly|UI for the application
|recipebook.core|.NET Standard Class Library|Core library for non technology specific logic|

## Tests
|Project|Type|Purpose|
|---|---|---|
|recipebook.functions.test|XUnit Tests|Unit Tests for the recipebook.functions project|
|recipebook.blazor.test|XUnit Tests|Unit tests for hte recipebokk.blazor project|

# Actions
This project uses GitHub Actions for builds and releases

|Name|Path|Purpose|
|---|---|---|
|CI Build|.github\workflows\ci-build.yml|Continuous Integration build for the repository.  Builds, runs tests, and packages artifacts.  Runs on all branches|
|Deploy recipebook.functions|.github\workflows\fuctions-deploy.yml|Deploys the recipebook.functions to an Azure Functions resource|
|Deploy recipebook.blazor|.github\workflows\blazor-deploy.yml|Deploys  the recipebook.blazor app to a GitHub pages branch used for hosting the UI|

# Dependencies
To run this project successfully, there are the following dependencies:
|Resource|Purpose|
|---|---|
|[Cosmos DB](https://docs.microsoft.com/en-us/azure/cosmos-db/introduction)| Underlying data store for persisting data|
|[Azure Function](https://docs.microsoft.com/en-us/azure/azure-functions/)| Azure functions used to host the recipebook.functions app|
|[Azure Active Directory](https://azure.microsoft.com/en-us/services/active-directory/)| Azure AD tenant used to authenticate users and the underlying services|
|[GitHub Pages](https://guides.github.com/features/pages/)|Used to host the Blazor WebAssembly application|