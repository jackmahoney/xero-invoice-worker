deps=obj
generated=generated
out=out

$(out):
	mkdir -p $(out)

$(deps):
	dotnet restore

$(generated):
	mkdir -p $(generated)
	cp ../schemas/dist/*.cs $(generated)/

test: $(deps) $(generated) $(out)
	dotnet test

run: $(deps) $(generated) $(out)
	dotnet run -- --feed-url=http://localhost:8080/events --invoice-dir=$(out)
	
