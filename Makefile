.PHONY: test

setup:
	@echo "Ready."

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
