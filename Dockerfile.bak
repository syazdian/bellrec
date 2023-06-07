ARG ARG_APM_VENDOR=datadog

FROM enterprisenonpacr.azurecr.io/dotnet/aspnet:7.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim AS build

RUN apt-get update -y \
  && apt-get install -y python3

ARG NUGET_USERNAME
ENV SECRET_NUGET_USERNAME=$NUGET_USERNAME

ARG NUGET_PASSWORD
ENV SECRET_NUGET_PASSWORD=$NUGET_PASSWORD

WORKDIR /src
COPY . .
RUN dotnet workload restore
RUN dotnet restore "Server/Web.Server/Web.Server.csproj"
RUN dotnet build "Server/Web.Server/Web.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Server/Web.Server/Web.Server.csproj" -c Release -o /app/publish

FROM base AS apm-vendor-newrelic

RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
  && echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
  && wget https://download.newrelic.com/548C16BF.gpg \
  && apt-key add 548C16BF.gpg \
  && apt-get update \
  && apt-get install -y newrelic-netcore20-agent \
  && rm -rf /var/lib/apt/lists/*

ARG NewRelicHome=/usr/local/newrelic-netcore20-agent

ENV CORECLR_ENABLE_PROFILING=1 \
  CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
  CORECLR_NEWRELIC_HOME=$NewRelicHome \
  CORECLR_PROFILER_PATH=$NewRelicHome/libNewRelicProfiler.so \
  NEW_RELIC_DISTRIBUTED_TRACING_ENABLED=true
RUN mkdir -p ${NewRelicHome} && cd /usr/local && wget https://download.newrelic.com/dot_net_agent/previous_releases/8.30.0/newrelic-netcore20-agent_8.30.0.0_amd64.tar.gz && tar xvzf newrelic-netcore20-agent_8.30.0.0_amd64.tar.gz

FROM base AS apm-vendor-datadog

RUN apk --no-cache add curl
# Download and install the Tracer
RUN mkdir -p /opt/datadog \
  && mkdir -p /var/log/datadog \
  && TRACER_VERSION=$(curl -s https://api.github.com/repos/DataDog/dd-trace-dotnet/releases/latest | grep tag_name | cut -d '"' -f 4 | cut -c2-) \
  && curl -Lo /tmp/datadog-dotnet-apm.deb https://github.com/DataDog/dd-trace-dotnet/releases/download/v${TRACER_VERSION}/datadog-dotnet-apm_${TRACER_VERSION}_amd64.deb \
  && dpkg -i /tmp/datadog-dotnet-apm.deb \
  && rm /tmp/datadog-dotnet-apm.deb

# Enable the tracer
ENV CORECLR_ENABLE_PROFILING=1
ENV CORECLR_PROFILER={846F5F1C-F9AE-4B07-969E-05C26BC060D8}
ENV CORECLR_PROFILER_PATH=/opt/datadog/Datadog.Trace.ClrProfiler.Native.so
ENV DD_DOTNET_TRACER_HOME=/opt/datadog
ENV DD_INTEGRATIONS=/opt/datadog/integrations.json

FROM apm-vendor-${ARG_APM_VENDOR} as with-apm

FROM with-apm as final

ARG ARG_BUILD_VERSION
ENV BUILD_VERSION=$ARG_BUILD_VERSION

WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
EXPOSE 5000
ENTRYPOINT ["dotnet", "Bell.Reconciliation.Web.Server.dll", "--environment=Production"]
