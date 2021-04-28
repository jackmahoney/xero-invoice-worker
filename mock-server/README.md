# Test event server
A simple express server that responds with event payload when polled. 
Used for local testing and development of csharp invoice worker.

## Run
`make run`

### Start in background
You can start the mock server as a background process

`make start`

## Configure

Use environment variables to set response behavior. 

Mode controls how event IDs are generated.

```
process.env.MODE = 'increment|random';
```

Type controls the event type response.
```
TYPE = 'INVOICE_CREATED|INVOICE_DELETED|INVOICE_UPDATED|MIXED';
```
