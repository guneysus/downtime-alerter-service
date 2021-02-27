# redesigned-waffle

## Running

```
dotnet user-secrets set SendGridKey <key>
```

or `Manage User Secrets`

## Using

Default user/password:

```
username: serefguneysu@gmail.com
password: WX&U7gj5B
```


## Migrations

```
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate --context DowntimeAlerterDataContext
dotnet ef database update --context DowntimeAlerterDataContext

``