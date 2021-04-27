deps=node_modules
dist=dist

$(dist):
	mkdir -p $(dist)

$(deps):
	npm install

build: $(deps) $(dist)
	npm run build
