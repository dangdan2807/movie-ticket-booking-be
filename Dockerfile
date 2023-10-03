# Sử dụng một hình ảnh .NET SDK để xây dựng ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Sao chép mã nguồn ứng dụng vào hình ảnh
COPY . .

# Sử dụng dotnet để xây dựng ứng dụng
RUN ["dotnet", "restore"]
RUN dotnet build -c Release
RUN dotnet publish -c Release -o out

# Hình ảnh cuối cùng chạy ứng dụng ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "MovieTicketBookingBe.dll"]
