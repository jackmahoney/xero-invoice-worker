deps=node_modules

run: $(deps)
	MODE="increment" TYPE="INVOICE_CREATED" node index.js

$(dist):
	mkdir -p $(dist)

$(deps):
	npm install
