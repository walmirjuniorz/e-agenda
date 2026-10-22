# e-Agenda

## Projeto

O e-Agenda é um sistema web desenvolvido para auxiliar no gerenciamento de contatos, compromissos, categorias, despesas e tarefas. O sistema centraliza informações e permite organizar diferentes atividades do dia a dia em um único ambiente.

A aplicação possui módulos para gerenciamento de contatos, compromissos, categorias, despesas e tarefas, permitindo cadastrar, visualizar, editar e excluir registros. Também conta com recursos para organização de tarefas por prioridade e status, além do gerenciamento de itens pertencentes a cada tarefa.

O projeto possui regras de negócio para garantir a integridade das informações, como a validação de dados obrigatórios, identificação de registros duplicados, controle de conflitos de horários entre compromissos e validação de relacionamentos entre os diferentes módulos.

## Funcionalidades

### Contatos

- Cadastro, visualização, edição e exclusão de contatos
- Validação de nome, email e telefone
- Validação de email e telefone únicos
- Cadastro opcional de cargo e empresa
- Impedimento da exclusão de contatos vinculados a compromissos

### Compromissos

- Cadastro, visualização, edição e exclusão de compromissos
- Cadastro de assunto, data e horários
- Definição do tipo de compromisso como presencial ou remoto
- Cadastro de local para compromissos presenciais
- Cadastro de link para compromissos remotos
- Associação opcional com contatos
- Validação de conflitos de horários

### Categorias

- Cadastro, visualização, edição e exclusão de categorias
- Validação de título único
- Associação de despesas às categorias
- Visualização das despesas pertencentes a uma categoria
- Impedimento da exclusão de categorias relacionadas a despesas

### Despesas

- Cadastro, visualização, edição e exclusão de despesas
- Cadastro de descrição, data e valor
- Definição da forma de pagamento
- Associação de uma ou mais categorias
- Data de ocorrência opcional, utilizando a data de cadastro como padrão

### Tarefas

- Cadastro, visualização, edição e exclusão de tarefas
- Controle de prioridade
- Controle de status de conclusão
- Controle do percentual de conclusão
- Visualização de tarefas pendentes e concluídas
- Visualização de tarefas agrupadas por prioridade
- Cadastro opcional de itens de tarefa

### Itens de Tarefas

- Adição e remoção de itens em tarefas
- Controle do status de conclusão dos itens
- Atualização do percentual de conclusão da tarefa conforme os itens concluídos

### Tecnologias utilizadas

- C#
- ASP.NET Core MVC
- CSS / CSHTML
- Bootstrap
- SQL Server
- Entity Framework Core

Desenvolvido durante o curso Backend da [Academia do Programador](https://www.academiadoprogramador.net/) 2026
