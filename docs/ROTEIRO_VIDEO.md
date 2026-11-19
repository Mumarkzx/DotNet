# Roteiro do vídeo — até 5 minutos

## 0:00–0:40 — problema e solução

Apresente o AMPARO como rede de cuidado para idosos e PcD. Explique que a API cadastra o cidadão e seus cuidadores, base para o dispositivo e o aplicativo acionarem a rede de apoio.

## 0:40–1:30 — código e banco

Abra `Models/Cidadao.cs`, `Models/Cuidador.cs` e `Data/AmparoDbContext.cs`. Mostre a FK, o 1:N, as validações e `DeleteBehavior.Restrict`. Abra `Migrations/*_InitialCreate.cs` e mostre que as tabelas são criadas por migration com prefixo próprio.

## 1:30–3:50 — demonstração

No Postman: GET vazio; POST cidadão; POST cuidador usando o ID recebido; GET por ID; PUT; POST inválido (`400`); GET inexistente (`404`). Tente apagar o cidadão primeiro e mostre `409`.

Antes de excluir, vá ao SQL Developer e execute o SELECT com JOIN do README. Mostre os dois registros relacionados. Volte ao Postman, exclua primeiro o cuidador e depois o cidadão (`204`).

## 3:50–4:30 — fluxo da requisição

Mostre: rota HTTP → método do controller → `AmparoDbContext`/EF Core → Oracle → resposta e status HTTP.

## 4:30–5:00 — integração e encerramento

Explique somente o que existe: esta API oferece a persistência central que o Mobile pode consumir e cuja conexão/execução pode ser configurada no pipeline de DevOps por variáveis secretas. Não afirme que deploy ou integração já foram feitos se não houver evidência no repositório.
