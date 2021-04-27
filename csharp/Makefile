deps=obj
out=out

run: $(deps) $(out)
	dotnet run -- --feed-url=http://localhost:8080/events --invoice-dir=$(out)
	
$(out):
	mkdir -p $(out)

$(deps):
	dotnet restore


test: $(deps) $(out)
	dotnet test

