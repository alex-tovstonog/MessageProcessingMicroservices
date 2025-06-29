# Message Processing API

This is a simple .NET API that receives and stores messages using RabbitMQ and PostgreSQL.

# Build and Run

To build and run the project:

docker compose up --build

This will start:
the API locally on port 8888. It's without UI so please use swagger (link below) to see/test endpoint(s)

the RabbitMQ UI at: http://localhost:15672
(login: guest / guest)

# API Documentation
Once the application is running, you can access the Swagger UI at:

http://localhost:8888/swagger

Use this interface to send and test messages.

# Project Structure
src/ – application source code
Dockerfile – builds the API image
docker-compose.yml – runs the API with RabbitMQ and PostgreSQL