# Architecture
The decisions that went into designing and building the application.

## Systems

- DotNET Core
- Sqlite
- NodeJS 

I chose to write the application in DotNET Core as it is a robust framework that suits a stable, long-living project. Sqlite was chosen as a persistence layer to store processed record IDs to avoid duplication. Sqlite is fast to prototype but in production I would select something like Reddis or Postgres for this application. I wrote a test server for prototyping and NodeJS and included it to demonstrate Javascript. For more complex Node apps I would use Typescript. 

### Structure
I find teams differ on code style and layout conventions so I just adopt those of my workplace. For this project I arranged the application around `Makefiles` and sub projects. The DotNET application uses a separate test project. It also uses a folder layout reminiscent of Java.

## How it works?
- The console application uses a command line argument parser to extract and validate parameters. 
- It then starts an async long running hosted service called InvoiceWorker.
- InvoiceWorker calls a feed endpoint via HTTP, deserializes a json response and process an EventResponse
- An EventRecord DbSet is used to check for already processed events in the sqlite database
- If received event IDs are not present in the database they are passed to the InvoiceService
- The InvoiceService writes a PDF for each event to the output directory given
- I use PdfSharp to convert HTML into 

## What was missing?
If I were to develop this application for production I would make some changes:
- Add health checks endpoints, proper logging, prometheus etc
- Dockerfile deployment strategy
- Kubernetes deployment and service etc (or whichever method used)
- CI integration (.circleci/config.yml) for building, testing, deploying
- Typesafe templating of the PDF (use Razor or similar)
- Better exception handling 
- Better long polling using backoff and retry with thread safety

### Testing
I would add much more test coverage for a real application. I would also add integration testing, continuous integration testing and deployment. Blue green deployments / smoketests etc.


> I would also try to work with the team that owned the event feed url and find out more about the constraints. 
