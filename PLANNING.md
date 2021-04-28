# Planning
Taking the task and breaking it down.

## Goal
Application should:
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
Should continue on errors, new event types, malformed events but log exceptions

## Assumptions
I made these assumptions about the project requirements. In a real application I would consult with owner of the feed url provider to confirm assumptions.

- I assume the event feed endpoint delivers events by ID in ascending order and that one can paginte through them.
- Event IDs never go down (say after deleting events) so an event can be matched uniquely to its ID 
- I assume events of the same ID can appear again in rare cases but they are the exact same event as that received before with the same ID so can be ignored

Working with the endpoint owner to refine the pagination might benefit the project. 
Using a `beforeDate` and `afterDate` method based around timestamps of events instead of `lastId` for windowing results could be easier.
