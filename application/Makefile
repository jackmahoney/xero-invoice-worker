FEED_URL=http://localhost:3000/events

deps=obj
out=out

$(deps):
	dotnet tool install --global dotnet-ef
	dotnet tool install -g dotnet-format
	dotnet restore

$(out):
	mkdir -p $(out)

fmt:
	dotnet-format .


run: $(deps) $(out)
	dotnet run -- --feed-url=$(FEED_URL) --invoice-dir=$(out) --follow
	
clean:
	rm -rf $(out)
	rm -rf .events.db
	
test: $(deps) $(out)
	dotnet test
	
build: $(deps) $(out)
	dotnet publish -c Release -o $(out)

