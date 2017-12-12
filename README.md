# LiveScore
Simple Web API for sport results overview that allows submition and update of game scores, and standings calculation.

## Authorization
*In progress*

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

## Response Messages
*In progress*

## Tests
*In progress*

## Developer Guidelines
*In progress*
