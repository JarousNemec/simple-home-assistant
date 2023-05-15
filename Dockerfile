#image for windows

#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
#WORKDIR /App
## Copy everything
#COPY . ./
### Restore as distinct layers
#RUN dotnet restore "./SimpleHomeAssistantServer/SimpleHomeAssistantServer.csproj"
### Build and publish a release
#RUN dotnet publish "./SimpleHomeAssistantServer/SimpleHomeAssistantServer.csproj" -c Release -o out
#
## Build runtime image
#FROM mcr.microsoft.com/dotnet/aspnet:6.0
#WORKDIR /App
#EXPOSE 10002:10002
#COPY --from=builder /App/out .
#ENTRYPOINT ["dotnet", "SimpleHomeAssistantServer.dll"]


#image for armv7

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
WORKDIR /App
# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore "./SimpleHomeAssistantServer/SimpleHomeAssistantServer.csproj"
# Build and publish a release
RUN dotnet publish "./SimpleHomeAssistantServer/SimpleHomeAssistantServer.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:6.0.0-focal-arm32v7
WORKDIR /App
EXPOSE 10002:10002
COPY --from=builder /App/out .
ENTRYPOINT ["dotnet", "SimpleHomeAssistantServer.dll"]

# build image
# docker image build -t jardathedev/shaserver:latest -f Dockerfile .

# push to docker registry
# docker push jardathedev/shaserver:latest

# docker login

# pull to docker registry
# docker pull jardathedev/shaserver:latest

# run
#docker run -p 10002:10002 jardathedev/shaserver:latest