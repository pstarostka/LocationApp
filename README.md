# LocationApp

This is a small .NET 6 Web API using minimal API approach with [Fast-Endpoints](https://fast-endpoints.com/).

It's a small CRUD App for geolocation using IP addresses.

## Running the application
To run the application, you need to have a valid ApiKey from [ipstack](https://ipstack.com/)

To add user secrets for development purposes, use below commands in API project location:


``` 
dotnet user-secrets init
dotnet user-secrets set IpStackApiKey <your_api_key>
```

## Sample Data
There are a couple of data in DB. You can use these IPs to try them out:
  
  - 185.31.24.68
  - 212.77.98.9