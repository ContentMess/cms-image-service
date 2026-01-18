FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build-env

ARG NUGET_USER
ARG NUGET_PASSWORD

RUN dotnet nuget add source "https://nuget.pkg.github.com/contentmess/index.json" --name Github --username $NUGET_USER --password $NUGET_PASSWORD --store-password-in-clear-text

WORKDIR /App

# Copy 
COPY ./src .

# Build Api
WORKDIR /App/Cms.ImageService.Api/src

# Build and publish a release
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine as runtime-env

WORKDIR /App

COPY --from=build-env /App/Cms.ImageService.Api/src/out .

RUN apk update
RUN apk add bash libwebp libwebp-tools
RUN export PATH="/usr/bin/cwebp:$PATH"

ENTRYPOINT ["dotnet", "Cms.ImageService.Api.dll"]
