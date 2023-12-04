# Projektets Startguide

Detta projekt är byggt med .NET och Docker och använder Command och Query mönster för att hantera tjänster. Här är några steg för att komma igång:

## Steg 1: Förberedelse

1. Se till att du har installerat Docker på din maskin. Om du inte har det, kan du ladda ner det här: [Docker Downloads](https://www.docker.com/products/docker-desktop).

2. Se till att du har .NET SDK installerat. Om du inte har det, kan du ladda ner det här: [Download .NET SDK](https://dotnet.microsoft.com/download/dotnet).

## Steg 2: Klona projektet

Klona detta projekt till din lokala maskin:

# bash
git clone <repository-url>


# Steg 3: Bygg och kör Docker-containern

docker build -t myapp .
docker run -d -p 8080:80 myapp



# Steg 4: Använda kommandon och frågor

docker exec -it <container-id> dotnet run command "<kommando-parametrar>"

docker exec -it <container-id> dotnet run query "<fråge-parametrar>"

# Steg 5: Testa tjänsterna
## Använd en webbläsare eller ett verktyg som curl för att testa de exponerade API-tjänsterna på 
## http:// localhost:8080.


# Steg 6: Stoppa och ta bort containern

docker stop <container-id>
docker rm <container-id>
