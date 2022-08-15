# Ali ZEROUALI
# Tui.Musement.Api

Web Api Application which allows you to retrieve the weather forecast for a specific city provided by TUI Musement and save the informations retrieved.

# Technologies

- [Microsoft ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0)
- [Dapper](https://github.com/DapperLib/Dapper)
- [Microsoft Open API](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-5.0)
- [Serilog](https://serilog.net/)
- [Unit Testing Using XUnit](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test)

# How To Install

- You need to install [Visual Studio 2022](https://visualstudio.microsoft.com/fr/vs/#:~:text=Visual%20Studio%202022%20est%20le,plus%20fluide%20et%20plus%20r%C3%A9actif.)
- Install [Dot Net 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Check if Dot Net 6 is well installed, for that you have to open you command line and tap the following command :
```
    dotnet --list-sdks
```
- Install [Git](https://git-scm.com/downloads) 
- Go to GitHub and get the solution from GitHub to your Local using `git clone`
- Once you have to application locally, go to `Weather.Forecast.Cli.sln` and double click
- Once you application is opened in Visual Studio, make sure that the Layer `Weather.Forecast.Cli` is set to `Set as Statup project`.
- Run you application

# Code organization : Clean Architecture 

The project is based on the architectural model ``Clean Architecture`` whose documentation is available on the Microsoft website : [Architect Modern Web Applications with ASP.NET Core and Azure](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/)

The organization of the code is based on the model of [Jason Taylor Clean Architecture](https://github.com/jasontaylordev/CleanArchitecture).

## Layer : Tui.Musement.Api

Layer which is a web api application based on ASP.NET Core 6. This layer depends on both Application and Infrastructure layers, however, dependency on Infrastructure is only to support dependency injection. Therefore, only Startup.cs should reference Infrastructure.

## Tui.Musement.Domain

Layer that contains all entities, enumerations, exceptions, interfaces, types, and logic specific to the domain layer.

## Tui.Musement.Application

Layer that contains all application logic. It depends on the domain layer, but has no dependency on any other layer or project. This layer defines interfaces that are implemented by external layers. For example, if the application needs to access a notification service, a new interface would be added to the application and an implementation would be created within the framework.

## Tui.Musement.Infrastructure

Layer that contains classes for accessing external resources such as file systems, web services, smtp, etc. These classes must be based on interfaces defined within the application layer.

# Folder Structure

```
├── azure
│   ├── azure-pipelines.yaml
│   └── azuredeploy.parameters.json
├── azure-pipelines
│   ├── build.yaml
│   └── GitVersion.yml
├── Tui.Musement.Api.sln
├── README.md
├── docs
├── src
│   ├── Tui.Musement.Api
│   ├── Tui.Musement.Application
│   ├── Tui.Musement.Domain
│   └── Tui.Musement.Infrastructure
└── test
    ├── Tui.Musement.Api.UnitTests
    ├── Tui.Musement.Application.UnitTests
    ├── Tui.Musement.Domain.UnitTests
    ├── Tui.Musement.Infrastructure.UnitTests
    └── Tui.Musement.Api.IntegrationTests
```

- **azure** : Azure infrastructure deployment files
- **azure-pipelines** : Azure pipeline (YAML) to build the application
- **docs** : documentation elements (markdown, help, etc.)
- **src** : product source code
- **test** : test projects

# Dapper (DB Access)

The project uses [Dapper](https://github.com/DapperLib/Dapper) as a data access technology (ORM - Object Relational Mapping).

## Health Checks

The API exposes two types of health checks :

| Health Check | Endpoint | Description |
|:--|:--|:--|
| Liveness | `/diagnostics/health/live` | Exposes the availability of the API, without checking whether the API is functional or not. |
| Readiness | `/diagnostics/health/ready` | Expose whether the API is ready to receive requests. It checks the operation of the API as well as the operation of its dependencies. |

> ⚠ The healt check `Liveness` is used by **Azure Application Gateway** to query API availability.

Microsoft Documentation: 
[Health checks in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-5.0)

---

# Endpoints To Set and Get The Forecast For A Specific City 

Here are the steps to follow for each layer in this API in order to create two endpoints that allows you to set or get the weather forecast informations for a specific city :

### Step 0 : Database

Let's choose Oracle as a Database.
Let's suppose that the table CITY already exist, so you only have to create a table FORECAST_DAY     as following :
- `FORECAST_DAY` with 3 columns at minimum for now (**CityId** : Number(6) NOT NULL, **Date** : DATE, **Condition** : Varchar2(20 BYTE))

Then you have to add a **CONSTRAINT** to the **ForecastDay** in order to create a foreign key for the column **CityId** that references the table **City** and it's column **Id**.

Then you have to create an **UNIQUE INDEX** on the table **ForecastDay** for the column **CityId** in order to make the search more faster.

### Step 1 : Tui.Musement.Domain

In this layer you have to create the POCO entities as following :

- Create a Folder `Entities`
- Create a Sub Folder `City` and a Sub Folder `WeatherForecast`
- Create a public class `City.cs` in the Folder `City` with the following parameters :

| Class Name| Properties | Types | Description |
|:--|:--|:--|:--|
| City | **Id** | Integer | The city identifier |
| City | **Name** | Nullable String | The city name |
| City | **ForecastDays** | Nullable List of ForecastDay Type | all the forecasts by days for the city |
> Note: `ForecastDay` Type should be created in the folder "WeatherForecast".

- Create the following public classes in the Folder `WeatherForecast`

| Class Name | Properties | Types |Description|
|:--|:--|:--|:--|
| Condition | **Text** | Nullable String |The weather condition|
| Day | **Condition** | Nullable Condition Type | The weather by day|
| ForecastDay | **Date** | Nullable String | The date in a string format|
| ForecastDay | **Day** | Nullable Day Type | The weather by day|
| Forecast | **Forecastday** | Nullable List of ForecastDay Type | The forecast object|
| Location | **Name** | Nullable String | The city name|
| Location | **LocalTime** | Nullable String | The local time of the city|
| WeatherForecast | **Location** | Nullable Location Type | The localion object|
| WeatherForecast | **Forecast** | Nullable Forecast Type | The forecast object|

### Step 2 : Tui.Musement.Infrastructure

In this layer you have to do the following steps :

- Create a class `OracleConnectionFactory.cs`, in this class you are going to use **Dapper** for the mapping.
- Inside **OracleConnectionFactory** class create a function as following :
```sh
private static void SetDapperMappings(){}
```
- Inside this function you need to add mapping between the entity and the table here is an exemple :
```sh
SqlMapper.SetTypeMap(typeof([YourEntity]),[Your Function for mapping]);
```
- **[Your Function for mapping]** : for this part, am gonna show you how to create it later in the Application layer.
- You need to create another function inside **OracleConnectionFactory** that will create the connection and get the KeyVault **ConnectionStrig** from environment variables 
```sh
public OracleConnection CreateConnection(){}
```
- Don't forget to add a default Constructor for the class and pass the **SetDapperMappings()** inside of it.

Now we are done with the **OracleConnectionFactory.cs**, let's create the DataAccess Objects.

- First create a folder `DataAccessLayer` and a Sub-Folder `DataAccess` then create the following classes : `CityDataAccess.cs`, `WeatherForecastDataAccess.cs`
- For the **WeatherForecastDataAccess** create a function to get the weather forecast informations for a specific city from weather Url:
```sh
public async Task<WeatherForecast> GetForecastInfosAsync(City city){}
```
- Use **IConfiguration** using **Microsoft.Extensions.Configuration** package to retrieve the weather url from appsettings.json as following :
```sh
string url = _conf.GetValue<string>("WeatherUrl") + $"&q={city.Name}&days=2";
```
- Get **HttpClient.GetAsync(url)** to get the reponse
- Use **Newtonsoft.Json** package to deserialize the json reponse into WeatherForecast
- For the **CityDataAccess** create a the following functions :
```sh
public async Task<IEnumerable<ForecastDay>> GetWeatherForecastByCityIdAsync(GetWeatherForecastByCityQuery query){}
```
```sh
public async Task UpdateOrCreateWeatherForecastForCityAsync(UpdateOrCreateWeatherForecastForCityCmd command){}
```

> Note: `UpdateOrCreateWeatherForecastForCityCmd` and `GetWeatherForecastByCityQuery` are going to be defined later in this design.

- For both theses functions create a SQL query for getting or setting the weather forecast for a city, here is an exemple :
```sh
var getWeatherForecastList = $@"SELECT  Day,
                                        Condition,
                                        CityId
                                        FROM FORECAST_DAY WHERE CityId = {query.id}";
```
- For the **UpdateOrCreateWeatherForecastForCityAsync** you need to verify first if the city id already exists in the table CITY, if not, you'll need to insert a new city in this table, otherwise, you'll juste need to update the **FORECAST_DAY** table with the list of ForecastDay sent in object **City**. You can do all this in one single **Stored Procedure** in Oracle and then use it in you C# code.

### Step 3 : Tui.Musement.Application

In this layer you need to do the following steps, but before you need to keep in mind that you are not allowed to add references to other layers except for Domain Layer:

- Create the following folders : 

```
├── Common
│   ├── Exceptions
│   ├── Mapping
│   │   ├── AutoMapper
│   │   └── Dapper
│   │       ├── City
│   │       └── ForecastDay
│   ├── QueriesAndCommands
│   │   ├── Queries
│   │   └── Commands
│   ├── Dto
│   │   └── City
│   └── Interfaces
│       └── DataAccess
```
- In the **Exceptions** folder you can create your own custom Exceptions based on your needs like a `NotFoundException` that inherit from `Exception`
- In the **Mapping** ==> **Dapper** ===> **City** ==> Create a Class `CityMapping.cs` and then create the following function (use Dapper for CustomPropetyTypeMap):
```
public static CustomPropertyTypeMap GetCityMap(){}
```
- Inside this Function create a Dictionary<string, string> where you gonna put in **Keys** the Column name and in **Values** the corresponding property of the entity you want to map.
- And then you return the following BuildMap :
```
 return DapperCustomBuilder.BuildMap([You Dictionnary Here], [You Entity Type Here]);
```
- You need to do the same thnig for `ForecastDayMapping.cs`
- So in the **Mapping** ==> **Dapper** ===> **ForecastDay** ==> Create a Class `ForecastDayMapping.cs` and then create the following function :
```
public static CustomPropertyTypeMap GetForecastDayMap(){}
```
- In the folder **Dto** ==> **City** ==> Create a Class `CityDto.cs` similar to `City.cs` because in Api Layer we are not allowed to use reference to Domain Layer except for Startup.cs

- In the folder **Mapping** ==> **AutoMapper** ==> Create a Class `CityProfile.cs` in order to do an automapping between the **CityDto** and **City** Entity
- So for the `CityProfile.cs` you'll need to add the package `AutoMapper` and the you call should inherit from `Profile`, and in you **default Constructor** you can simply add the following line : 
```
CreateMap<City, CityDto>();
```

- In the folder **Interfaces** ==> **DataAccess** create the following interfaces with there functions :

| Interface | Not Implemented Functions |
|:--|:--|
| IWeatherDataAccess | Task<WeatherForecast> GetForecastInfosAsync(City city) |
| ICityDataAccess | Task<IEnumerable<ForecastDay>> GetWeatherForecastByCityIdAsync(int id) | 
| ICityDataAccess | Task UpdateOrCreateWeatherForecastForCityAsync(City city) | 

- Then return to the `CityDataAccess.cs` and `WeatherForecastDataAccess.cs` and do the implementation of these two interfaces like this :

```
    public class CityDataAccess : ICityDataAccess
```

```
    public class WeatherForecastDataAccess : IWeatherForecastDataAccess
```

- Now let's move to folder **QueriesAndCommands** ==> **Queries** ==> Create a Class **GetWeatherForecastByCityQuery.cs** with the following properties : 

| Properties | Types |
|:--|:--|
| **Id** | String |

- You can add validations to this properties, so you can add the following code for exemple :

```
    public void Validate()
        {
            var errors = new StringBuilder();

            AppendErrorIfArgumentNull(errors);

            if (!string.IsNullOrEmpty(errors.ToString()))
            {
                throw new ValidationException(errors.ToString());
            }
        }

        private void AppendErrorIfArgumentNull(StringBuilder errors)
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                errors.AppendLine($@"{nameof(Id)} must be provided.");
            }
        }
```

- Now let's move to the folder **QueriesAndCommands** ==> **Commands** ==> Create a class **UpdateOrCreateWeatherForecastForCityCmd.cs** with the following properties :

| Properties | Types |
|:--|:--|
| **Id** | String |
| **Name** | String |
| **ForecastDays** | List of ForecastDay Type |

- Also you can add some validation at this level.

### Step 4 : Tui.Musement.Api

In this layer you need to do the following steps :

-   First You need to Add in your appsettings.json the **"key" : "value"** of the **WeatherUrl** and also the **DB ConnectionString**.
-   In Class `Statup.cs` you need to add the dependency injection configuration for the instanciation of the classes : 
So In the **ConfureService** Method you need to add the following lines :
```
services.AddScoped<ICityDataAccess, CityDataAccess>();
services.AddScoped<IWeatherForecastDataAccess, WeatherForecastDataAccess>();
services.AddSingleton<OracleConnectionFactory>();
```

- And Also you can add the swagger configuration : [Swagger](https://code-maze.com/swagger-ui-asp-net-core-web-api/) 
- Now Create a folder `Controllers` and then create a class `CityController.cs`
- In this class you should inherit from `ControllerBase` so for this one you'll need to install the package `Microsoft.AspNetCore.Mvc`
- Then you need to add at least these two attributes to your class :
```
[ApiController]
[Route("[controller]")]
```
- And for sure if you want to add more autorisation level you can add the attribute **[Authorize]**
- Now let's create the first function which going to set the weather forecast informations to a city, so you need to create the function as follow :

```
[HttpPut("{cityId}")]
public async Task<IActionResult> UpdateOrCreateWeatherForecastForCityAsync(string cityId, CityDto city){}
```
- Inside of you function you can first call the class **WeatherDataAccess** located in the layer Infrastructure and use the method **GetForecastInfosAsync** to get the weather informations related to the city sent in parameters :

```
await _weatherDataAccess.GetForecastInfosAsync(city.Id);
```

- Then you'll have to create a new instance and fill **UpdateOrCreateWeatherForecastForCityCmd** with the object city sent in parameters.
- Then you can add some validations if necessary
- After that you can just call the class **CityDataAccess** located in the layer Infrastructure and use the method **UpdateOrCreateWeatherForecastForCityAsync** in order to set the forecast informations for the city.
- And at the end **return NoContent();**

- Now let's create the second function which is going to get the weather forecast informations to a city, so you need to create the function as follow :

```
[HttpGet("getByCityId")]
public async Task<CityDto> GetWeatherForecastByCityIdAsync([FromQuery] GetWeatherForecastByCityQuery query){}
```
- Inside of this function you'll juste have to call the following method :

```
var cityResult = await _cityDataAccess.GetWeatherForecastByCityIdAsync(query);
```
- But before making this call, make sure to do some validations before, specially for the city identifier if it's not null
- Once you got the result from **_cityDataAccess.GetWeatherForecastByCityIdAsync(query)**, call mapper to map between City and CityDto as follow :

```
var result = _mapper.Map<CityDto>(cityResult);
```

- You can also do a quick check on the **result** value if the mapping passed with success
- And then just **return result;**
