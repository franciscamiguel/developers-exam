# INSPAND Developers Exam #

### O que é este projeto

Este projeto visa instigar o desenvolvedor a demostrar seus conhecimentos. Bem como avaliar o entendimento irá ter sobre projeto. Com isso existem situações que não foram implementadas no projeto, deixando assim a cargo do candidato avaliar.

### Resumo do projeto
Este projeto está construído em NetCore na **versão 8.0.205**, com esquema de arquitetura inspirada em **Clean Architecture**, ao qual este conta com 3 sub projetos:
- Domain
- Infrastructure
- Web

Ferramentas que podem ser usadas para desenvolver:
- VSCode.
- VS 2022.
- Sua criatividade.

Utilitários:
- Há um arquivo docker compose, caso queira utilizar o docker para subir o banco SqlServer. (Não iremos avaliar este conhecimento, apenas um meio mais rápido de subir uma base)

### Você pode
- Utilizar pacotes NuGet (https://www.nuget.org/) que julgar necessário.
- No acesso à banco de dados, você pode usar algum ORM: EF Core, NHibenate, Dapper ou outro, assim como fazer manualmente utilizando ADO. O projeto está com esquema para EF Core, porem pode ser alterado de acordo com sua escolha.
- Utilizar Dependency Injection.
- Utilizar FluentValidation.

### Observações
- O Banco de dados utilizado pelo projeto é o SqlServer, caso não tenha instalado em sua maquina, pode usar o docker compose para subir este banco.
- Caso queira pode executar em algum outro banco (SqlServer) de sua escolha e depois enviar atrelado ao projeto o arquivo .sql (Caso utilizar database first).

### Esperamos que você
- Implemente o desafio proposto.
- Receber um feedback sobre o projeto.
- Entretanto, o mais importante é conseguirmos analisar a maneira que você codifica, não tem problema se não for possível terminar tudo no tempo determinado.

### Desafio
Eu como administrador desejo que seja possível listar usuários cadastrados na plataforma, bem como realizar criação, edição e exclusão dele.
Para cadastro de usuário é necessário o input de alguns dados como: Nome, Sobrenome,  Login, Senha, E-mail e Idade. Todos os campos são de preenchimento obrigatório. 
Ao realizar o cadastro de um novo usuário, é desejável que o sistema envie um e-mail de boas-vindas ao usuário.

**Critérios de aceite:**
- Não pode existir dois ou mais usuários com mesmo e-mail e login. Caso ocorra exibir uma mensagem informando que o e-mail já existe cadastrado.
- Não pode existir dois ou mais usuários com mesmo nome e sobrenome. Caso ocorra exibir uma mensagem informando que o nome e sobrenome já existe cadastrado.
- Idade não pode ser menor que 10 e maior que 100. Caso ocorra informar que a idade é inválida.
- Os campos nome, sobrenome e e-mail não pode ter mais que 255 caracteres. Caso ocorra informar que o limite de caracteres foi atingido.

**Observações:**
No item de disparo de e-mail não há necessidade de implementar o disparo em si, mas somente chamar o evento que que é responsável por esta ação.

### Feedback
Como parte da avaliação, desejamos que faça um feedback sobre seu entendimento de cada camada do projeto:
1. Qual papel da camada de Domain ?
2. Qual papel da camada de Infrastructure ?
3. Qual papel da camada de WebApp ?
4. Faça um resumo de como funciona a comunicação entre as camadas e qual é suas hierarquias.
5. Aponte um item negativo sobre o projeto que julgar necessário.

### Regras:
O desenvolvedor irá contar com **5 horas** para desenvolver e fazer os feedbacks.

**Boa Prova!**

**Sucesso!** pode ser aquilo que você é grato por ter conquistado ! :smile:

Powered by **INSPAND Developers**