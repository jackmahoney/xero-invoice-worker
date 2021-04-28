deps=node_modules

run: $(deps)
	MODE="increment" TYPE="INVOICE_CREATED" node index.js

start: $(deps)
	MODE="increment" TYPE="INVOICE_CREATED" node index.js > /dev/null 2>&1 & echo "MOCK SERVER running on PID: $$! localhost:3000" 

$(dist):
	mkdir -p $(dist)

$(deps):
	npm install
