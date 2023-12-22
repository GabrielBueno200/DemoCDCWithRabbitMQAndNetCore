# DemoCDCWithRabbitMQAndNetCore

## 💻 About The Project

A simple data streaming app with CDC (Change Data Capture) concepts applying Debezium, RabbitMQ, MassTransit, .NET Core and Microsoft SQL Server

The project was developed to demonstrate the effective implementation of Change Data Capture (CDC) processes using the Debezium tool. It efficiently monitors and records changes in data, facilitating the synchronization of information across various destinations, including databases, indexers, private data stores, disks, and more.

Change Data Capture (CDC) serves as a database technique designed to identify and capture changes (insertions, updates, deletions) in data. Instead of continuously scanning the entire database, CDC specifically tracks and captures only the data that has undergone changes since the last inspection. This approach streamlines the management and replication of updates, reducing the database workload and enhancing overall performance.

In essence, CDC, particularly when integrated with tools like Debezium, facilitates a streamlined process for monitoring and capturing data changes. It enables the synchronization of these changes across different systems without overwhelming the database with unnecessary queries or scans.

## Demo approach
<center>
  <img src="https://github.com/GabrielBueno200/DemoCDCWithRabbitMQAndNetCore/assets/56837996/f77c3c86-466b-498f-9fea-286af8de7f29" />
</center>

## Requirements
- You must have docker and docker-compose installed 

## 🚀 How To Run

```bash
# Clone the repository
$ git clone https://github.com/GabrielBueno200/DemoCDCWithRabbitMQAndNetCore.git

# Access the project folder in your terminal / cmd
$ cd DemoCDCWithRabbitMQAndNetCore

# Setup docker containers
$ docker compose up -d
```


