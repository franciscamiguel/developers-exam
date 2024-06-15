# INSPAND Developers Exam #
### Estrutura do Projeto
Domain
Responsabilidade: A camada de Domain é responsável pela lógica de negócios e as regras de domínio da aplicação. Contém entidades, agregados, repositórios, serviços de domínio e validações.

#Infrastructure
Responsabilidade: A camada de Infrastructure é responsável pela implementação de detalhes técnicos, como a persistência de dados e a comunicação com serviços externos. Nesta camada, implementamos os repositórios, os contextos de banco de dados e qualquer serviço de infraestrutura.

### WebApp
Responsabilidade: A camada de WebApp é responsável por expor a aplicação para os usuários finais. Contém controladores, modelos de visão e serviços de aplicação que interagem com as outras camadas para processar e retornar as solicitações HTTP.

### Comunicação Entre as Camadas
Domain: Define as regras de negócios e as entidades que serão usadas nas outras camadas.
Infrastructure: Implementa interfaces definidas na camada de Domain, como repositórios e serviços de infraestrutura.
WebApp: Faz uso dos serviços e repositórios definidos nas outras camadas para atender às requisições dos usuários.
A comunicação ocorre da seguinte maneira:

### WebApp interage com Infrastructure para acessar os dados persistidos.
Infrastructure usa os contratos definidos em Domain para implementar a persistência de dados.
Domain é independente e não depende de nenhuma outra camada.
Implementação dos Requisitos
Listar Usuários:

### Criação de um controlador em WebApp para listar usuários.
Uso de um serviço de aplicação que acessa o repositório de usuários em Infrastructure.
Criação, Edição e Exclusão de Usuários:

### Controladores em WebApp para criar, editar e excluir usuários.
Validações em Domain para garantir que todos os campos obrigatórios sejam preenchidos e os critérios de aceitação sejam atendidos.
Serviços de domínio para encapsular a lógica de criação, edição e exclusão.
Envio de evento para disparo de e-mail ao cadastrar um novo usuário (pode ser implementado um simples log para simular o envio).
Validações:

### Uso de FluentValidation para validar as regras de domínio (e.g., campos obrigatórios, tamanho máximo, unicidade de email e login, etc).
Implementação de verificações adicionais no repositório para garantir unicidade de email e login e evitar duplicação.
Feedback Sobre a Estrutura do Projeto
Camada de Domain
Papel: Gerenciar a lógica de negócios, garantindo que todas as regras de domínio sejam aplicadas corretamente.
Força: Independência de outras camadas, facilitando a testabilidade e a manutenção das regras de negócio.
### Camada de Infrastructure
Papel: Implementar detalhes técnicos como a persistência de dados e interações com serviços externos.
Força: Abstração de detalhes técnicos da lógica de negócios, permitindo mudanças na infraestrutura sem impactar a camada de domínio.
Camada de WebApp
Papel: Expor a aplicação ao usuário, processando requisições HTTP e retornando respostas adequadas.
Força: Centralização da lógica de interface com o usuário, separando preocupações de interface das regras de negócio.
Comunicação e Hierarquia
### Hierarquia: Domain -> Infrastructure -> WebApp.
Comunicação: WebApp chama serviços de Infrastructure, que por sua vez implementam contratos definidos em Domain.
Item Negativo
Um ponto negativo que pode ser observado é a falta de documentação e exemplos práticos sobre como configurar e iniciar o projeto. Um README mais detalhado ajudaria a orientar o desenvolvedor sobre as dependências necessárias, como configurar o ambiente de desenvolvimento, e exemplos básicos de uso.