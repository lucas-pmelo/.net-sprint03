- Augusto Barcelos Barros – RM: 98078
- Gabriel Souza de Queiroz – RM: 98570
- Lucas Pinheiro de Melo – RM: 97707
- Mel Maia Rodrigues – RM: 98266

Definição da Arquitetura da API

Para a nossa solução, optamos por utilizar uma arquitetura monolítica. A escolha por essa abordagem se deve ao fato de que, embora o projeto tenha potencial para ser escalável e possua um domínio central com subdomínios distintos, ele se concentra em resolver uma dor específica do cliente de forma simples e direta. O foco principal é desenvolver uma solução eficiente, que atenda às necessidades atuais com o menor custo possível, tanto em termos de desenvolvimento quanto de manutenção.

A abordagem monolítica é ideal para este estágio do projeto, pois facilita a implementação inicial, reduz a complexidade e acelera o tempo de entrega. Todos os componentes do sistema, incluindo as funcionalidades de precificação de convênios e recomendação de hospitais e clínicas, serão desenvolvidos e implantados como uma única unidade. Isso simplifica a gestão do código, a implantação e a depuração, além de permitir uma comunicação direta entre os diferentes módulos do sistema, sem a necessidade de configurar e gerenciar a comunicação entre serviços separados.

Implementação da API e Justificativa da Arquitetura Escolhida

A implementação da API seguirá os princípios da arquitetura monolítica, onde todo o código-fonte do projeto, incluindo os subdomínios "Cliente" e "Unidade", será mantido em um único repositório e será compilado e executado como um único aplicativo. Isso significa que todas as funcionalidades, desde a interface com o usuário até a lógica de negócios e o acesso a dados, estarão integradas em um único código-base.

Uma das principais diferenças entre uma arquitetura monolítica e uma arquitetura de microservices é a forma como os módulos são gerenciados e implantados. Em uma arquitetura de microservices, cada serviço é independente, com seu próprio ciclo de vida, podendo ser desenvolvido, implantado e escalado separadamente. Isso proporciona maior flexibilidade e resiliência, especialmente em sistemas complexos e de grande escala. No entanto, essa abordagem também traz desafios adicionais, como a necessidade de gerenciar a comunicação entre serviços, lidar com a consistência de dados distribuídos e orquestrar a implantação de múltiplos serviços.

No contexto do nosso projeto atual, onde o objetivo é criar uma solução eficiente e de fácil manutenção, a arquitetura monolítica se mostra mais adequada. Entretanto, à medida que o projeto evoluir, novas funcionalidades forem adicionadas e a complexidade do sistema aumentar, será natural considerar a migração para uma arquitetura de microservices. Isso permitirá uma melhor separação de domínios, facilitando a escalabilidade, a manutenção e a adição de novas funcionalidades de forma organizada e estruturada.
