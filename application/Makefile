FEED_URL=http://localhost:3000/events

deps=obj
out=out

run: $(deps) $(out)
	dotnet run -- --feed-url=$(FEED_URL) --invoice-dir=$(out)
	
clean:
	rm -rf $(out)
	rm -rf .events.db
	
$(out):
	mkdir -p $(out)

$(deps):
	dotnet tool install --global dotnet-ef
	dotnet restore

test: $(deps) $(out)
	dotnet test

