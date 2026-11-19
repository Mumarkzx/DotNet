# Roteiro da apresentação online — 23 a 27/11

1. `README.md`: problema CitizenTech, ODS e desenho 1:N.
2. `Models/Cidadao.cs` e `Models/Cuidador.cs`: obrigatórios, tamanhos, e-mail, prioridade e FK.
3. `Controllers/CuidadoresController.cs`, método `Criar`: a rota recebe JSON, o `[ApiController]` valida e o método verifica a existência do cidadão.
4. `Data/AmparoDbContext.cs`: o EF traduz a operação para Oracle, com tabela, PK, índice e FK mapeados.
5. `Controllers/CuidadoresController.cs`, `CreatedAtAction`: o banco devolve o ID e a API responde `201` com `Location`.
6. `Migrations/*_InitialCreate.cs`: explique `Up`, `Down`, snapshot e a diferença entre migration gerada e aplicada.
7. Postman: demonstre CRUD, um `400`, um `404` e a restrição `409`.
8. SQL Developer: execute o JOIN antes das exclusões.
9. Integrações: Mobile poderá consumir os endpoints; DevOps deverá fornecer a connection string como segredo. Diferencie possibilidade arquitetural do que o grupo efetivamente implementou.

Frase para explicar o caminho: “A URL seleciona o método no controller; o controller valida a entrada e chama o DbContext; o EF Core transforma isso em SQL para o Oracle; o resultado retorna pelo controller com o status HTTP adequado.”
