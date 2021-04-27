deps=node_modules
dist=dist

$(dist):
	mkdir -p $(dist)

$(deps):
	npm install

run: $(deps) $(dist)
	npm start
