
# checkout-basket-service

This API will allow our users to set up and manage an order of items.
The API will allow users to add and remove items and change the quantity of the items they want.
They should also be able to simply clear out all items from their order and start again.

# API

## Basket Authorization

GET /BasketAuthorization/connect/token

## Baskets
- GET /Baskets
- POST /Baskets
- GET /Baskets/{basketId}
- DELETE /Baskets/{basketId}

## Basket Items
- GET /BasketItems/{basketId}/items/{itemId}
- DELETE /BasketItems/{basketId}/items/{itemId}
- PATCH /BasketItems/{basketId}/items/{itemId}
- POST /BasketItems/{basketId}/items

# Usage

This API requires jwt authorization. To be able to call methods in this API one must first call `GET  /BasketAuthorization/connect/token` in order to get a authorization token. Then send it in the request headers:

```cs
var uri = "http://localhost:49435";
var httpConnection = new HttpConnection(uri);
var client = new BasketsAuthorizationClient(httpConn);
var token = await client.GetTokenAsync();
```

and then with this token create an http connection for the client using the token:

```cs
var header = new Tuple<string, string>("Authorization", token);
var httpConn = new HttpConnection(uri, headers);
var client = new BasketsClient(httpConn);
```

# License
ISC


# Contributing

This is our suggestion on how you should go about proposing changes to this project:

1. [Fork this project][fork] to your account.
2. [Create a branch][branch] for the changes you intend to make.
3. Make your changes and commit then.
4. [Send a pull request][pr] from your fork’s branch to our `master` branch.

Use the web-based interface when possible. It will help you by automatically forking the project, prompting to send a pull request, etc.

[fork]: https://help.github.com/articles/fork-a-repo/
[branch]: https://help.github.com/articles/creating-and-deleting-branches-within-your-repository
[pr]: https://help.github.com/articles/using-pull-requests/
