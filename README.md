# DemoDotNet
Sistema de demonstração ASP.NET Core, Docker, Swagger.  
Este sistema é composto por dois projetos de APIs que expõem suas definições Swagger.  
Para testar as chamadas dos endpoints, existe um terceiro projeto com a Swagger UI, que importa as definições dos dois projetos.  
Os três serviços rodam atrás de um proxy reverso Traefik. Desta forma podemos acessar todos os serviços pelo mesmo host e porta.  

## Rodando o sistema com docker
Acesse a pasta raiz do projeto via __bash__ e inicie o os serviços utlizando docker-compose:

```bash
docker-compose up -d
```
O docker-compose irá fazer o build das imagens e iniciar o sistema em http://localhost:8080. Também será exposta a interface de administração do Traefik em http://localhost:8081.  
Caso queira utilizar outras portas, basta alterar o *docker-compose.yml*.


## Endpoints disponíveis

| Url                     | Sistema       | Descrição                           |
| ----------------------- |:-------------:| -----------------------------------:|
| /                       | Swagger       | Interface de testes das APIS        |
| /taxas/taxajuros        | API Taxas     | Consulta da taxa de juros           |
| /calculos/showmethecode | API Cálculos  | Consulta do endereço do repositório |
| /calculos/calculajuros  | API Cálculos  | Cálculo de juros compostos          |

## Rodando testes unitários
Acesse a pasta raiz do projeto via  e execute o seguinte comando:

```bash
dotnet test
```
*Também irá aparecer no Test Explorer do Visual Studio*
