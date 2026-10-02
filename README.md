# QuickBite

Food-delivery API used to demo Vertical Slice Architecture in .NET 6.

Each git branch is one slide. Check out the branch, then run the API.

## Prerequisites

- .NET 6 SDK
- SQL Server LocalDB

Connection string in `src/QuickBite.Api/appsettings.json`:

```
Server=(localdb)\mssqllocaldb;Database=QuickBite;Trusted_Connection=True;MultipleActiveResultSets=true
```

## Run

```
dotnet run --project src/QuickBite.Api
```

The API listens on `http://localhost:5080`.

Each startup drops and recreates the `QuickBite` database so the tables match the branch you checked out.

Sample call: `requests.http`.

## Branches

| Branch | Demo |
| --- | --- |
| `01-traditional-layered` | Controller, service, repository |
| `02-growing-order-service` | Place Order grows inside `OrderService` |
| `03-place-order-slice` | The feature moves into one folder |
| `04-request-handler-endpoint` | Request, handler, and endpoint |
| `05-validation` | Validator next to the feature |
| `06-complex-feature` | Restaurant, menu, pricing, payment, notification in one handler |
| `07-refactor-complex-feature` | Those pieces split inside the slice |
| `08-shared-logic` | Shared `PaymentCalculator` |
| `09-domain-logic` | `order.Cancel()` owns the rule |
| `10-final-architecture` | Features, domain, shared, infrastructure |
| `11-tests` | Validator and domain tests |

`main` matches `11-tests`.

From branch `06` onward, place an order with restaurant `11111111-1111-1111-1111-111111111111` and menu item `22222222-2222-2222-2222-222222222222`.
