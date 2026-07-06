## Como testar o projeto

### Pré-requisitos
- [Docker](https://www.docker.com/) instalado e em execução
- [.NET SDK](https://dotnet.microsoft.com/) instalado

### 1. Subir o Kafka com Docker

Na raiz do repositório, execute:

```bash
docker compose pull
docker compose up -d
```

> O `-d` roda os containers em segundo plano (opcional, mas recomendado).

### 2. Rodar o Producer

Em um terminal, na raiz do projeto:

```bash
cd ./Producer
dotnet run --urls=https://localhost:5001
```

O serviço ficará disponível em `https://localhost:5001`.

### 3. Rodar o Consumer

Em **outro terminal**, também a partir da raiz do projeto:

```bash
cd ./Consumer
dotnet run
```

O Consumer começará a consumir as mensagens publicadas nos tópicos do Kafka.

## Como publicar uma mensagem

### 2 endpoints 
- Enviar uma mensagem como query params `https://localhost:5001/?message=...`
<img width="853" height="333" alt="image" src="https://github.com/user-attachments/assets/aad678ff-a680-42b2-b90f-7268a0d5e14a" />

- Enviar uma messagem com body json `https://localhost:5001/person`
<img width="848" height="334" alt="image" src="https://github.com/user-attachments/assets/c3e0b693-46b2-480c-8775-175b070fba71" />


## Kafdrop

O Kafdrop é uma interface web (Web UI) gratuita e de código aberto desenvolvida pela Obsidian Dynamics para monitorar
e gerenciar clusters do Apache Kafka

pode ser acessada na url `http://localhost:19000/`
<img width="1267" height="941" alt="image" src="https://github.com/user-attachments/assets/bb1eead7-2b60-43ce-b5d4-a40b50b459aa" />

### Package

- Confluent.Kafka version: 2.15.0 (para configurar o kafka)





