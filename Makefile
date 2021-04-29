#
# Application makefile - see makefiles in other folders too
#
.PHONY: test

setup:
	cd application && $(MAKE) build

test:
	cd test && $(MAKE) test

clean:
	cd application && $(MAKE) clean

mock:
	cd mock-server && $(MAKE) run
	
spawn_mock:
	cd mock-server && $(MAKE) start

run:
	cd application && $(MAKE) run 

build:
	docker build -t application .
