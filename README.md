# Desafio técnico crud curso de idomas

## Sobre
Esse é uma api em C# usando Asp.Net e  Entity framework, esta api foi criada no contexto de curso de idiomas e permite as operações de criar,deletar,listar todas e listar por id as turmas, e alunos, criar um novo aluno,buscar todos, buscar por id
matricular um aluno, desmatricular e deletar, e também atualizar o aluno.A api está documentada no Swagger

#### Requisitos bonus aplicados
- Esta api também reestringe a criação de alunos com mesmo cpf, oque se aplica na hora de atualizar o cpf
- Garante que ao criar um aluno ele esteja sendo matriculado em algum curso
- um Aluno pode estar matriculado em varias turmas, mas uma turma só pode ter um mesmo aluno
- Uma turma tem um número maximo de 5 alunos, e apartir disso não é possivel matricular alunos nessa mesma turma
- Só é possivel excluir uma turma se ela não possuir alunos
