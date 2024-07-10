# One Way

```http
POST /catalog
Content-Type: application/json

{
    "vendor": "Microsoft",
    "application": "VSCode",
    "version": "1.91"
}
```


# Another way

```http
POST /catalog/nintendo/super-mario-brothers
Content-Type: application/json

{
    "version": "2"
}

```

Response:

```http
201 Created
Content-Type: application/json
Location: "/catalog/nintendo/super-mario-brothers/2"

{
    "version": "1.91",
    "vendor": "Microsoft",
    "application": "VSCode"
}