# Invoice worker technical exercise - Jack Mahoney

## Goal
- Consume event feed and produce PDF invoices
- Poll HTTP endpoint
- Store state and resume
- Exit 0 on ctrl-c SIGINT
- Console applications with argument flags

### Input
- URL to JSON event feed 
- Configuration flags `--feed-url|--invoice-dir`

### Output
- Create, update, or delete a PDF for each invoice found in the event feed
- Action depends on the event type `INVOICE_CREATED|INVOICE_UPDATED|INVOICE_DELETE`

### Testing
Should continue on errors, new event types, malformed events but log 
