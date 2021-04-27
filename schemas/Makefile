deps=node_modules
dist=dist

SCHEMA=$(PWD)/dist/schema.json 
CLASSES=$(PWD)/dist/Schema.cs

build: $(deps) $(dist)
	OUTPUT=$(SCHEMA) npm run generate-json
	cd generate && INPUT=$(SCHEMA) OUTPUT=$(CLASSES) dotnet run

$(dist):
	mkdir -p $(dist)

$(deps):
	npm install
