# مرحلة البناء باستخدام Alpine (أصغر حجمًا)
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# نسخ ملفات المشاريع أولاً لتحسين استخدام layer caching
COPY ["myfood.Api/Api/Api.csproj", "myfood.Api/Api/"]
COPY ["myfood.Shared/Shared/Shared.csproj", "myfood.Shared/Shared/"]
COPY ["myfood.Shared/Shared.Contract/Shared.Contract.csproj", "myfood.Shared/Shared.Contract/"]
COPY ["myfood.Shared/myfood.Messages/myfood.Messages.csproj", "myfood.Shared/myfood.Messages/"]
COPY ["myfood.Modules/Identity/Identity.infrastructure/Identity.infrastructure.csproj", "myfood.Modules/Identity/Identity.infrastructure/"]
COPY ["myfood.Modules/Identity/Identity.Domain/Identity.Domain.csproj", "myfood.Modules/Identity/Identity.Domain/"]
COPY ["myfood.Modules/Identity/Identity.Application/Identity.Application.csproj", "myfood.Modules/Identity/Identity.Application/"]
COPY ["myfood.Modules/Common/Common/Common.csproj", "myfood.Modules/Common/Common/"]
COPY ["myfood.Modules/Notification/Notification/Notification.csproj", "myfood.Modules/Notification/Notification/"]

# استعادة الحزم
RUN dotnet restore "myfood.Api/Api/Api.csproj"

# نسخ باقي الملفات وبناء المشروع
COPY . .
WORKDIR "/src/myfood.Api/Api"
RUN dotnet build "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# مرحلة النشر
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# مرحلة التشغيل النهائية باستخدام Distroless (أكثر أمانًا)
FROM gcr.io/distroless/base-dotnet:nonroot AS final
WORKDIR /app

# نسخ الملفات المنشورة
COPY --from=publish /app/publish .

# تعيين المنافذ
EXPOSE 8080
EXPOSE 8081

# تشغيل التطبيق كمستخدم غير root (مضمن في صورة distroless nonroot)
ENTRYPOINT ["dotnet", "Api.dll"]