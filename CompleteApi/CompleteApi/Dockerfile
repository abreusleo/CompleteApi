FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CompleteApi/CompleteApi.csproj", "CompleteApi/"]
RUN dotnet restore "CompleteApi/CompleteApi.csproj"
COPY . .
WORKDIR "/src/CompleteApi"
RUN dotnet build "CompleteApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CompleteApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompleteApi.dll"]
