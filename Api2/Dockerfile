FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Api2.csproj", ""]
RUN dotnet restore "Api2.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "Api2.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Api2.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Api2.dll"]