build:
	cd schemas && $(MAKE) build && cd -;
	cd nodejs && $(MAKE) build && cd -;
	cd csharp && $(MAKE) build && cd -;
