# :tophat: Microservices Communication with Dapr and RabbitMQ

This repository showcases the communication between microservices using Dapr and RabbitMQ. It comprises two microservices: the "Manager" service and the "Accessor" service. The application simulates a simple contacts book system, where contacts are stored and retrieved using MongoDB.

This project serves as a playground for practicing backend technologies.

## :hammer_and_wrench: Technologies Used

- .NET Core
- Dapr
- Docker + docker compose
- RabbitMQ
- MongoDB

## :building_construction: Project architecture

The project architecture consists of the following components:

- **Manager Service**: Accepts requests (Get, Post, Delete), validates phone numbers to be correct israeli numbers, processes them to the **RabbitMQ** queue or directly to **Accessor** via **Dapr**.
- **Accessor Service**: Obtains messages from the **RabbitMQ** queue, or recieves requests directly from **Manager**, processes them, and performs actions on **MongoDB**.
- **RabbitMQ**: Messaging system facilitating the exchange of requests between the **Manager** and **Accessor** services.
- **Dapr**: Manages communication between the **Manager** and **Accessor** services, as well as the **RabbitMQ** queue.
- **MongoDB**: Database used for storing and retrieving contacts.

<img src="https://github.com/ek-popkova/contacts/assets/111788752/6bc64177-ae9f-4180-a4a2-278db81e1fa1" alt="Project architecture" width="500">


## :rocket: Usage

The Manager service exposes the following endpoints:

- `GET /contacts`: returns the list of all contacts.
- `GET /contact/{phone}`: returns the list of all contacts with the given phone number.
- `DELETE /contact/{phone}`: deletes all contacts with the given phone number.
- `POST /contact`: adds a new contact. 
- `POST /contact-rabbitMQ`: adds a new contact via RabbitMQ query.

For POST requests, the contact should be given in the request body in JSON format:

```json
{
  "name": "John Doe",
  "phone": "+972123456789"
}
```
