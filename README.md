# Description
This service provide get a list of countries via the third-party service and save it in MS SQL Server.
This project created for HR crmguru.ru.
# Run
## Run tests
```bash
git clone https://github.com/SGmuwa/CountryHRCrmguruRu
cd CountryHRCrmguruRu
dotnet test
```
## Run console program (Ubuntu)
### Clone
```bash
git clone https://github.com/SGmuwa/CountryHRCrmguruRu
cd CountryHRCrmguruRu
```
### Launch SQL Server
Start Microsoft SQL server. Replace PORT to port, PASSWORD to your password:
```bash
sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=PASSWORD' -p PORT:1433 mcr.microsoft.com/mssql/server:2019-CU4-ubuntu-16.04
```
For example:
```bash
sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=mypassword123!@#' -p 1433:1433 mcr.microsoft.com/mssql/server:2019-CU4-ubuntu-16.04
```
### Configure connection settings
For client's side settings you must to edit `ConsoleCountry/appsettings.json`. For creator model you must also edit `DBCountriesSource/appsettings.json`. For example:
```json
{
    "connectionStrings": {
        "MyDBContext": "Server=127.0.0.1,1433;Database=Master;User Id=SA;Password=mypassword123!@#;"
    }
}
```
Where `127.0.0.1` — IP, `1433` — port, `mypassword123!@#` — password.
### First load model to SQL Server
For create first models run:
```
dotnet ef database update -p DBCountriesSource/
```
You can also get first model from [releases](https://github.com/SGmuwa/CountryHRCrmguruRu/releases).
### Run app
Change directory:
```bash
cd ./ConsoleCountry/
```
Run app:
```bash
dotnet run
```
You can use environments:
```bash
env ConnectionStrings:MyDBContext='Server=127.0.0.1,1433;Database=Master;User Id=SA;Password=mypassword123!@#;' dotnet run
```
You can download binary from [releases](https://github.com/SGmuwa/CountryHRCrmguruRu/releases).
# Customer requirements
Requirements in a original language:

1. При запуске приложения должно быть 2 функциональности на выбор пользователя: Ввод названия страны; вывод всех стран с БД. После ввода страны должно выдать информацию о стране (Название, Код страны, Столица, Площадь, Население, Регион), полученную с АПИ https://restcountries.eu/rest/v2/name/Ukraine, либо если страна не найдена выдать об этом сообщение.
2. В случае если страна найдена, выдать предложение пользователю сохранить информацию в базу (MS SQL). Если пользователь отказывается - не сохранять.
    1. В БД должно быть 3 таблицы: Регионы (Id - идентификатор, Название - строка), Города (Id - идентификатор, Название - строка), Страны – (Id - идентификатор, Название – строка, Код страны – строка, Столица – идентификатор с таблицы Города, площадь – дробное число, Население – целое число, Регион – идентификатор с таблицы Регионы)
    2. Алгоритм добавления следующий:
        1. проверяем наличие Столицы в таблице Города, если найдена, то берем её идентификатор, если нет, то добавляем;
        2. проверяем наличие Региона в таблице Регионов, если найден, то берем его идентификатор, если нет, то добавляем;
        3. Проверяем наличие Страны в таблице стран по коду страны, если страна не найдена – добавляем с идентификаторами, полученными выше, если найдена обновляем значения.
3. При выборе вывода всех стран БД должен вывестись список всех стран в БД со следующими полями: Название, Код страны, Столица, Площадь, Население, Регион. Прошу обратить внимание, что Столицу и Регион тут нужно выводить название.
