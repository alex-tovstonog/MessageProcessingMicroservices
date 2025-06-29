# Message Processing Microservices

This is a simple .NET Microservices API that receives and stores messages using RabbitMQ and PostgreSQL.

# Build and Run

To build and run the project:

docker compose up --build

This will start:
the MessageApiService which recieves messages via HTTP and publishes them to RabbitMQ. API locally on port 8888
the MessageConsumerService which consumes messages from RabbitMQ and stores them in PostgreSQL. API locally on port 9999

Both APIs are without UI so please use swagger (link below) to see/test endpoint(s)

the RabbitMQ UI at: http://localhost:15672
(login: guest / guest)

# API Documentation
Once the application is running, you can access the Swagger UI at:

#MessageApiService 
http://localhost:8888/swagger

#MessageConsumerService
http://localhost:9999/swagger

# Project Structure
MessageApiService/ – MessageApiService source code
MessageConsumerService/ – MessageConsumerService source code
docker-compose.yml – runs both services with RabbitMQ and PostgreSQL