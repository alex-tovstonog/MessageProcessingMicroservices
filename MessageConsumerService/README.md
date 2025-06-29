# Message Consumer Service

This is a simple .NET service which consumes messages from RabbitMQ and stores them in PostgreSQL. And also provides an API to retrieve stored messages.

# API Documentation
Once the application is running, you can access the Swagger UI at:

http://localhost:9999/swagger

Use this interface to send and test messages.

# Project Structure
src/ – application source code
Dockerfile – builds the API image
