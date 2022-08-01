# Personal API

## Roadmap
- Simple API to start using docker
- Create another api so we can use Microservices architecture
- Create one docker-compose.yml to run all services
- Create decent pipeline on github
- Create communication between services using RabbitMq
- Create database to storage calculations
- Create cache to avoid useless communication
- Create communication between services using GRPCS
- Create unit tests
- Stress test the application
- Use kubernetes instead docker
- Deploy it on aws

## Docs
### 1. Microservices
- https://docs.microsoft.com/en-us/azure/architecture/guide/architecture-styles/microservices
### 2. Docker
- https://docs.docker.com/
### 3. APIs
- https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio
- https://docs.docker.com/samples/dotnetcore/
### 4. Github Actions
- https://docs.github.com/pt/actions
### 5. RabbitMq
- https://www.rabbitmq.com/features.html
### 6. Grpc
- https://grpc.io/about/
- https://grpc.io/docs/
### 7. Unit tests
- https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
### 8. Stress tests
- https://docs.locust.io/en/stable/
### 9. Kubernetes
- https://kubernetes.io/pt-br/docs/home/
### 10. AWS
- https://docs.aws.amazon.com/

## How to run your api
1. Go to Dockerfile directory
2. Run ```docker build . -t {image_name}```
3. Run ```docker run -d --rm -p {PORT_HTTP}:{PORT_HTTP} -p {PORT_HTTPS}:{PORT_HTTPS} -e ASPNETCORE_HTTP_PORT=https://+:{PORT_HTTPS} -e ASPNETCORE_URLS=http://+:{PORT_HTTP} {image_name}```
## hello world
