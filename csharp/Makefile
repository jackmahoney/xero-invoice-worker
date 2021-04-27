FEED_URL=http://localhost:3000/events

deps=obj
out=out

run: $(deps) $(out)
	dotnet run -- --feed-url=$(FEED_URL) --invoice-dir=$(out)
	
$(out):
	mkdir -p $(out)

$(deps):
	dotnet restore


test: $(deps) $(out)
	dotnet test

