


## Adding Reverse Proxy

Add the `Yarp.ReverseProxy` to the API.:w


```csharp
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
```

Before other routes:

```csharp
app.MapReverseProxy();
```

### The Config

```json

 "ReverseProxy": {
    "Routes": {
      "route1": {
        "ClusterId": "hr",
        "Match": {
          "Path": "/hr/{**catch-all}"
        },
        "Transforms": [
         
          {"PathRemovePrefix": "/hr"}
        ]
      }
    },
    "Clusters": {
      "hr":{
        "Destinations": {
          "d1": {
            "Address": "http://localhost:XXXX"
          }
        }
      }
    }
  }
```