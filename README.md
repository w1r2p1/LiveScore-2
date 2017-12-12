# LiveScore
Simple Web API for sport results overview that allows submition and update of game scores, and standings calculation.

## Authorization
Authorization and user management is provided via Auth0 service. Client application needs to obtain bearer token by sending POST request to https://gotrunx.auth0.com/oauth/token with application/json body with contents similar to this:
```
{
  "client_id": YOUR_CLIENT_ID,
  "client_secret": YOUR_CLIENT_SECRET,
  "audience": "https://localhost",
  "grant_type": "client_credentials"
}
```
**NOTE: Replace YOUR_CLIENT_ID and YOUR_CLIENT_SECRET with provided values for your client app.**

Response from Auth0 service will look something like this:
```
{
  "access_token": RANDOM_ACCESS_TOKEN,
  "scope": "create:scores read:scores",
  "expires_in": 86400,
  "token_type": "Bearer"
}
```
**NOTE: RANDOM_ACCESS_TOKEN will actualy be your new bearer token value that needs to be passed in header of API requests as 'Authorization' parameter.**

There are two scopes defined for this API:
- `create:scores`: requested by *SubmitScores* and *UpdateScores* endpoints.
- `read:scores`: requested by *GetScores* and *GetStandings* endpoints.

## API Endpoints (api/scores)
This is a short walktrough of available endpoints and their parameters.

### SubmitScores (api/scores/submit) PUT
This endpoint allows submission of game scores. You can submit one game, entire group, multiple groups, even all league games.
Parameter (passed through request body) for this endpoint is an array of GameScore models which look like this:
```
{
  "leagueTitle": "Champions League 2017/18",
  "matchday": 1,
  "group": "A",
  "homeTeam": "Manchester United",
  "awayTeam": "Basel",
  "kickoffAt": "2017-09-01T20:00:00",
  "score": "3:0"
}
```
**NOTE: Date is in ISO 8601 format.**

### UpdateScores (api/scores/update) POST
This endpoint allows update of existing game scores. Similar usage as SubmitScores. You can update one game, entire group, multiple groups, even all league games.
Parameter (passed through request body) for this endpoint is an array of GameScore models which look like this:
```
{
  "leagueTitle": "Champions League 2017/18",
  "matchday": 1,
  "group": "A",
  "homeTeam": "Manchester United",
  "awayTeam": "Basel",
  "kickoffAt": "2017-09-01T20:00:00",
  "score": "3:2"
}
```
**NOTE: Date is in ISO 8601 format.**

### GetScores (api/scores/fetch) POST
This endpoint lists existing game scores. They can be filtered using parameter (passed through request body) that is array of Filter models which look like this:
```
[
  {
    "name": 0,
    "value": "2017-09-01T20:00:00"
  },
  {
    "name": 1,
    "value": "2017-09-08T20:00:00",
    "relation": 1
  }
]
```
Name parameter can have one of the following values:
- Start date (0): requires ISO 8601 formated date time
- End data (1): requires ISO 8601 formated date time
- Team (2): requires team Id
- Group (3): requires group Id

Relation parameter can have one of the following values:
- OR (0)
- AND (1)

**NOTE: Relation parameter is applied alongs with previous filter, hence first filter needs it not.**

### GetStandings (api/scores/standings) GET
This endpoint calculate standings based on existing scores. They can be filtered using parameter (passed through request URL) that is array of group Ids (int) which looks like this:
```
api/scores/standings?groupIds=1,2,4
```

**NOTE: URL parameter is optional and called 'groupIds'. It can contain single integer vaule or multiple (coma-separated) integer values.**

## Response Messages
Response messages will contain one of the following HTTP status codes:
- `200 OK`: if request is successfully proccessed and response is returned.
- `400 Bad Request`: if one of the parameters in the request is invalid.
- `401 Authorization Error`: probably if baerer token is expired or required scope is not provided by client.
- `500 Internal Server Error`: if error occurs during request processing caused by server state.

Error message body will look something like this:
```
{
  "error_code": 3
}
```
**NOTE: Error code contains additional info about the error that occured. (e.g. InvalidGroupId (4), or InvalidScores (6))**

## Tests
*In progress*

## Developer Guidelines
*In progress*
