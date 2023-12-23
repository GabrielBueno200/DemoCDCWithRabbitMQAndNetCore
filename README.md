# DemoCDCWithRabbitMQAndNetCore

## 💻 About The Project
The project was developed to demonstrate the effective implementation of [Change Data Capture (CDC)](https://learn.microsoft.com/en-us/sql/relational-databases/track-changes/about-change-data-capture-sql-server?view=sql-server-ver16) processes using the [Debezium](https://debezium.io/) tool. It efficiently monitors and records changes in data, facilitating the synchronization of information across various destinations, including databases, private data stores, disks, and more.

Change Data Capture (CDC) serves as a database technique designed to identify and capture changes (insertions, updates, deletions) in data. Instead of continuously scanning the entire database, CDC specifically tracks and captures only the data that has undergone changes since the last inspection. This approach streamlines the management and replication of updates, reducing the database workload and enhancing overall performance.

In essence, CDC, particularly when integrated with tools like Debezium, facilitates a streamlined process for monitoring and capturing data changes. It enables the synchronization of these changes across different systems without overwhelming the database with unnecessary queries or scans.

The demo implementation is a basic data streaming application incorporating Change Data Capture (CDC) principles using [Debezium](https://debezium.io/). It utilizes RabbitMQ as message broker to stream changes from a Microsoft SQL Server database. To consume these messages, a simple .NET Core consumer app was developed using [MassTransit](https://masstransit.io/).

## Why use CDC with Debezium instead trigger-based CDC?

The reason to use Debezium instead trigger is because its CDC approach is log-based. Of course, we can use trigger to monitore state events, but log-based CDC is better.

Trigger-based CDC poses various challenges, including complexity in maintaining and updating triggers as applications evolve; database performance degradation due to increased logic execution; resource overhead from accumulated triggers impacting scalability; dependencies on application changes leading to potential conflicts; troubleshooting difficulties; risks associated with cascading triggers; and resource consumption affecting system performance in high-concurrency environments.

In contrast, log-based CDC offers several advantages: It operates without impacting the production database's performance, as it doesn't require scanning operational tables or making application-level changes. It efficiently captures new transactions, including inserts, updates, and deletes, by reading the transaction log from the source database. This method is less intrusive compared to trigger-based CDC, ensuring minimal interference with the database operations. Additionally, log-based CDC remains consistent across different relational databases, despite variations in the transaction log's name, making it a reliable and scalable solution for tracking changes.

Furthermore, when paired with Debezium, log-based CDC not only efficiently captures changes from transaction logs but also enables intercepting these transactions across diverse messaging systems, like Kafka, RabbitMQ, Amazon SQS and others. This integration facilitates the consumption of these changes in various technologies and programming languages, enhancing the flexibility and adaptability of the CDC process.

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

## References

[Types Of Change Data Capture (CDC) For SQL: Choose Wisely](https://estuary.dev/sql-change-data-capture/)
