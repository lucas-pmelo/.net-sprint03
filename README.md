# Sprint 03 - .NET

- Augusto Barcelos Barros – RM: 98078
- Gabriel Souza de Queiroz – RM: 98570
- Lucas Pinheiro de Melo – RM: 97707
- Mel Maia Rodrigues – RM: 98266

## Definição da Arquitetura da API

Para a nossa solução, optamos por utilizar uma arquitetura monolítica. A escolha por essa abordagem se deve ao fato de que, embora o projeto tenha potencial para ser escalável e possua um domínio central com subdomínios distintos, ele se concentra em resolver uma dor específica do cliente de forma simples e direta. O foco principal é desenvolver uma solução eficiente, que atenda às necessidades atuais com o menor custo possível, tanto em termos de desenvolvimento quanto de manutenção.

A abordagem monolítica é ideal para este estágio do projeto, pois facilita a implementação inicial, reduz a complexidade e acelera o tempo de entrega. Todos os componentes do sistema, incluindo as funcionalidades de precificação de convênios e recomendação de hospitais e clínicas, serão desenvolvidos e implantados como uma única unidade. Isso simplifica a gestão do código, a implantação e a depuração, além de permitir uma comunicação direta entre os diferentes módulos do sistema, sem a necessidade de configurar e gerenciar a comunicação entre serviços separados.

## Implementação da API e Justificativa da Arquitetura Escolhida

A implementação da API seguirá os princípios da arquitetura monolítica, onde todo o código-fonte do projeto, incluindo os subdomínios "Cliente" e "Unidade", será mantido em um único repositório e será compilado e executado como um único aplicativo. Isso significa que todas as funcionalidades, desde a interface com o usuário até a lógica de negócios e o acesso a dados, estarão integradas em um único código-base.

Uma das principais diferenças entre uma arquitetura monolítica e uma arquitetura de microservices é a forma como os módulos são gerenciados e implantados. Em uma arquitetura de microservices, cada serviço é independente, com seu próprio ciclo de vida, podendo ser desenvolvido, implantado e escalado separadamente. Isso proporciona maior flexibilidade e resiliência, especialmente em sistemas complexos e de grande escala. No entanto, essa abordagem também traz desafios adicionais, como a necessidade de gerenciar a comunicação entre serviços, lidar com a consistência de dados distribuídos e orquestrar a implantação de múltiplos serviços.

No contexto do nosso projeto atual, onde o objetivo é criar uma solução eficiente e de fácil manutenção, a arquitetura monolítica se mostra mais adequada. Entretanto, à medida que o projeto evoluir, novas funcionalidades forem adicionadas e a complexidade do sistema aumentar, será natural considerar a migração para uma arquitetura de microservices. Isso permitirá uma melhor separação de domínios, facilitando a escalabilidade, a manutenção e a adição de novas funcionalidades de forma organizada e estruturada.

---

## Instruções para rodar o código:
Clone o repositório ou obtenha o código-fonte:

Certifique-se de ter o código .NET localmente.
Instale as dependências:

Execute dotnet restore na raiz do projeto para restaurar os pacotes NuGet necessários.
Configurar o ambiente:

Defina as variáveis de ambiente e conexões com banco de dados, se aplicável. Verifique os arquivos de configuração como appsettings.json.
Rodar o projeto:

Execute o comando dotnet run para iniciar a aplicação. Ela estará disponível na porta configurada (por padrão, http://localhost:5000 ou https://localhost:5001).
Acessar a API:

Utilize um cliente HTTP como o Postman ou curl para interagir com os endpoints expostos.
Endpoints disponíveis:
## Endpoints

### AgreementController

1. **GET /api/agreement/{id}**  
   Retorna o acordo com o ID especificado.
   - **Respostas**:
     - `200 OK`: Acordo encontrado.
     - `404 NotFound`: Acordo não encontrado.
     - `400 BadRequest`: ID inválido.

2. **GET /api/agreement**  
   Retorna uma lista de todos os acordos.
   - **Respostas**:
     - `200 OK`: Lista de acordos.
     - `500 InternalServerError`: Erro no servidor.

3. **POST /api/agreement**  
   Cria um novo acordo.
   - **Body**: Objeto JSON de *Agreement*.
   - **Respostas**:
     - `201 Created`: Acordo criado.
     - `400 BadRequest`: Dados inválidos.

4. **PUT /api/agreement/{id}**  
   Atualiza um acordo existente com o ID especificado.
   - **Body**: Objeto JSON de *Agreement*.
   - **Respostas**:
     - `200 OK`: Acordo atualizado.
     - `404 NotFound`: Acordo não encontrado.
     - `400 BadRequest`: ID ou dados inválidos.

5. **DELETE /api/agreement/{id}**  
   Exclui o acordo com o ID especificado.
   - **Respostas**:
     - `204 NoContent`: Acordo excluído.
     - `404 NotFound`: Acordo não encontrado.
     - `400 BadRequest`: ID inválido.

---

### CustomerController

1. **GET /api/customer/{id}**  
   Retorna o cliente com o ID especificado.
   - **Respostas**:
     - `200 OK`: Cliente encontrado.
     - `404 NotFound`: Cliente não encontrado.
     - `400 BadRequest`: ID inválido.

2. **POST /api/customer**  
   Cria um novo cliente.
   - **Body**: Objeto JSON de *Customer*.
   - **Respostas**:
     - `201 Created`: Cliente criado.
     - `400 BadRequest`: Dados inválidos.

3. **PUT /api/customer/{id}**  
   Atualiza um cliente existente com o ID especificado.
   - **Body**: Objeto JSON de *Customer*.
   - **Respostas**:
     - `200 OK`: Cliente atualizado.
     - `404 NotFound`: Cliente não encontrado.
     - `400 BadRequest`: ID ou dados inválidos.

4. **DELETE /api/customer/{id}**  
   Exclui o cliente com o ID especificado.
   - **Respostas**:
     - `204 NoContent`: Cliente excluído.
     - `404 NotFound`: Cliente não encontrado.
     - `400 BadRequest`: ID inválido.

---

### UnitController

1. **GET /api/unit/{id}**  
   Retorna a unidade com o ID especificado.
   - **Respostas**:
     - `200 OK`: Unidade encontrada.
     - `404 NotFound`: Unidade não encontrada.
     - `400 BadRequest`: ID inválido.

2. **GET /api/unit**  
   Retorna uma lista de todas as unidades.
   - **Respostas**:
     - `200 OK`: Lista de unidades.
     - `500 InternalServerError`: Erro no servidor.

3. **POST /api/unit**  
   Cria uma nova unidade.
   - **Body**: Objeto JSON de *Unit*.
   - **Respostas**:
     - `201 Created`: Unidade criada.
     - `400 BadRequest`: Dados inválidos.

4. **PUT /api/unit/{id}**  
   Atualiza uma unidade existente com o ID especificado.
   - **Body**: Objeto JSON de *Unit*.
   - **Respostas**:
     - `200 OK`: Unidade atualizada.
     - `404 NotFound`: Unidade não encontrada.
     - `400 BadRequest`: ID ou dados inválidos.

5. **DELETE /api/unit/{id}**  
   Exclui a unidade com o ID especificado.
   - **Respostas**:
     - `204 NoContent`: Unidade excluída.
     - `404 NotFound`: Unidade não encontrada.
     - `400 BadRequest`: ID inválido.
