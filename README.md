# DemoCDCWithRabbitMQAndNetCore

## 💻 About The Project
The project was developed to demonstrate the effective implementation of Change Data Capture (CDC) processes using the Debezium tool. It efficiently monitors and records changes in data, facilitating the synchronization of information across various destinations, including databases, private data stores, disks, and more.

Change Data Capture (CDC) serves as a database technique designed to identify and capture changes (insertions, updates, deletions) in data. Instead of continuously scanning the entire database, CDC specifically tracks and captures only the data that has undergone changes since the last inspection. This approach streamlines the management and replication of updates, reducing the database workload and enhancing overall performance.

In essence, CDC, particularly when integrated with tools like Debezium, facilitates a streamlined process for monitoring and capturing data changes. It enables the synchronization of these changes across different systems without overwhelming the database with unnecessary queries or scans.

The demo implementation is a basic data streaming application incorporating Change Data Capture (CDC) principles using [Debezium](https://debezium.io/). It utilizes RabbitMQ as message broker to stream changes from a Microsoft SQL Server database. To consume these messages, a simple .NET Core consumer app was developed using [MassTransit](https://masstransit.io/).


## Demo approach
<center>
  <img src="https://github.com/GabrielBueno200/DemoCDCWithRabbitMQAndNetCore/assets/56837996/f77c3c86-466b-498f-9fea-286af8de7f29" />
</center>

## Requirements
- You must have docker and docker-compose installed 
- .NET 8 SDK

## 🚀 How To Run

```bash
# Clone the repository
$ git clone https://github.com/GabrielBueno200/DemoCDCWithRabbitMQAndNetCore.git

# Access the project folder in your terminal / cmd
$ cd DemoCDCWithRabbitMQAndNetCore

# Setup docker containers
$ docker compose up -d

# Access dotnet source project directory
cd ./DataStreaming.Consumer/src

# Run dotnet app
dotnet run build
```


