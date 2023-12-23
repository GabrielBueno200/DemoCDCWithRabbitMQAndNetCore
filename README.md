# DemoCDCWithRabbitMQAndNetCore

## 💻 About The Project
The project was developed to demonstrate the effective implementation of [Change Data Capture (CDC)](https://learn.microsoft.com/en-us/sql/relational-databases/track-changes/about-change-data-capture-sql-server?view=sql-server-ver16) processes using the [Debezium](https://debezium.io/) tool. It efficiently monitors and records changes in data, facilitating the synchronization of information across various destinations, including databases, private data stores, disks, and more.

Change Data Capture (CDC) serves as a database technique designed to identify and capture changes (insertions, updates, deletions) in data. Instead of continuously scanning the entire database, CDC specifically tracks and captures only the data that has undergone changes since the last inspection. This approach streamlines the management and replication of updates, reducing the database workload and enhancing overall performance.

In essence, CDC, particularly when integrated with tools like Debezium, facilitates a streamlined process for monitoring and capturing data changes. It enables the synchronization of these changes across different systems without overwhelming the database with unnecessary queries or scans. Of course, we use trigger in this state. But, CDC is more performatic than trigger. The reason is it serves all these operations by reading the transaction log file of the database.

The demo implementation is a basic data streaming application incorporating Change Data Capture (CDC) principles using [Debezium](https://debezium.io/). It utilizes RabbitMQ as message broker to stream changes from a Microsoft SQL Server database. To consume these messages, a simple .NET Core consumer app was developed using [MassTransit](https://masstransit.io/).


## Demo approach
<center>
  <img src="https://github.com/GabrielBueno200/DemoCDCWithRabbitMQAndNetCore/assets/56837996/f77c3c86-466b-498f-9fea-286af8de7f29" />
</center>

### Source Database:
The demo uses a database called `TestDatabase` featuring a table named `Person` with enabled CDC (see [enable_cdc.sql](https://github.com/GabrielBueno200/DemoCDCWithRabbitMQAndNetCore/blob/main/enable_cdc.sql)).

### RabbitMQ
- The demo uses the RabbitMQ Message Broker configured through `rabbitmq` docker-compose service. This service is started with a pre-configured exchange `test.TestDatabase.dbo.Person`, binded with a queue named `person-cdc-queue`. Every change captured by debezium using CDC is published in this exchange and routed to this queue as a message (see [rabbitmq-defs.json](https://github.com/GabrielBueno200/DemoCDCWithRabbitMQAndNetCore/blob/main/docker_volumes/rabbitmq/rabbitmq-defs.json)).
- The exchange name was configured according debezium documentation: `topic_prefix.DatabaseName.dbo.TableName`. Prefix can be configured in the [application.properties](https://github.com/GabrielBueno200/DemoCDCWithRabbitMQAndNetCore/blob/main/docker_volumes/debezium/conf/application.properties) file.

### Target System: .NET Core Consumer
The demo implements a .NET Core console application using a MassTransit consumer named `PersonConsumer`, which listens and consumes all `person-cdc-queue` messages .

## Requirements
- You must have docker and docker-compose installed 
- .NET 8 SDK

## 🚀 How To Run

```bash
# Clone the repository
$ git clone https://github.com/GabrielBueno200/DemoCDCWithRabbitMQAndNetCore.git

# Access the project folder in your terminal / cmd
$ cd ./DemoCDCWithRabbitMQAndNetCore

# Setup docker containers
$ docker compose up -d

# For the next steps, make sure all docker containers have already been completely initialized

# Access dotnet source project directory
$ cd ./DataStreaming.Consumer/src

# Run dotnet app
$ dotnet run build
```
After execute the steps above, you can monitor RabbitMQ queue accessing `http://localhost:15672/#/queues` using some web browser (both login and password are "guest", as specified in the configuration files). Then, make some change (INSERT, UPDATE or DELETE) on `Person` table and check messages being published to the queue. After that you can visualize message being consumed by the running .NET Core Consumer logs.
