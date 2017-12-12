# LiveScore
Simple Web API for sport results overview that allows submition and update of game scores, and standigs calculation.

## Authorization
*In progress*

## API Endpoints (api/scores)
This is a short walktrough of available endpoints and their parameters.

### SubmitScores (api/scores/submit) PUT
This endpoint allows submission of game scores. You can submit one game, entire group, multiple groups, even all league games.
Parameter for this endpoint is an array of GameScore models which look like this:
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

## Tests
*In progress*

## Sample Date
*In progress*

## Developer Guidelines
*In progress*
