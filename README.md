# scheduler

Scheduler Jobs

## ⚙️ Restore e Build do projeto

```properties
 dotnet restore && dotnet build
```

## ⚙️ Rodar Projeto Localmente (usando uma massa de dados como parametro)

```properties
 dotnet run --project scheduler.job "{\"JanelaInicio\":\"2019-11-10 09:00:00\",\"JanelaFim\":\"2019-11-11 12:00:00\",\"Jobs\": [{\"Id\": 1,\"Descricao\":\"Importa\u00E7\u00E3o de arquivos de fundos\",\"DataConclusao\":\"2019-11-10 12:00:00\",\"TempoEstimado\": 2},{\"Id\": 2,\"Descricao\": \"Importa\u00E7\u00E3o de dados da Base Legada\",\"DataConclusao\": \"2019-11-11 12:00:00\",\"TempoEstimado\": 4},{\"Id\": 3,\"Descricao\": \"Importa\u00E7\u00E3o de dados de integra\u00E7\u00E3o\",\"DataConclusao\": \"2019-11-11 08:00:00\",\"TempoEstimado\": 6}]}"
```

## ⚙️ Executar os testes

```properties
 dotnet test
```
