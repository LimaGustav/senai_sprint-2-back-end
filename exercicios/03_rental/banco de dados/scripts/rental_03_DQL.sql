USE RENTAL
GO

SELECT * FROM EMPRESA
SELECT * FROM MARCA
SELECT * FROM CLIENTE
SELECT * FROM MODELO
SELECT * FROM VEICULO
SELECT * FROM ALUGUEL
GO

SELECT idVeiculo, ISNULL(V.idEmpresa, 0) idEmpresa, ISNULL(V.idModelo, 0) idModelo, placa, ISNULL(nomeEmpresa,'Não cadastradado') nomeEmpresa, ISNULL(nomeModelo,'Não cadastradado') nomeModelo, ISNULL(M.idMarca,0)idMarca, ISNULL(nomeMarca,'Não cadastradado') nomeMarca FROM VEICULO V
LEFT JOIN EMPRESA E
ON V.idEmpresa = E.idEmpresa
LEFT JOIN MODELO M
ON V.idModelo = M.idModelo
LEFT JOIN MARCA MAR
ON M.idMarca = MAR.idMarca

/*
 listar todos os alugueis mostrando as datas de início e fim,
 o nome do cliente que alugou e nome do modelo do carro
*/

SELECT nome, sobreNome,cnh, nomeModelo, dataRetirada, dataDevolucao FROM ALUGUEL
LEFT JOIN CLIENTE
ON ALUGUEL.idCliente = CLIENTE.idCliente
LEFT JOIN VEICULO
ON VEICULO.idVeiculo = ALUGUEL.idVeiculo
LEFT JOIN MODELO
ON MODELO.idModelo = VEICULO.idModelo
GO

/*
listar os alugueis de um determinado cliente mostrando seu nome,
as datas de início e fim e o nome do modelo do carro
*/

SELECT nome, sobreNome,cnh, nomeModelo, dataRetirada, dataDevolucao FROM ALUGUEL
RIGHT JOIN CLIENTE
ON ALUGUEL.idCliente = CLIENTE.idCliente
LEFT JOIN VEICULO
ON VEICULO.idVeiculo = ALUGUEL.idVeiculo
LEFT JOIN MODELO
ON MODELO.idModelo = VEICULO.idModelo
GO

SELECT	idAluguel, ISNULL(A.idVeiculo,0) idVeiculo, ISNULL(A.idCliente,0) idCliente,
		ISNULL(A.dataRetirada,0) dataRetirada,
		ISNULL(A.dataDevolucao,0)dataDevolucao,
		ISNULL(V.idEmpresa,0) idEmpresa,
		ISNULL(V.idModelo,0) idModelo,
		ISNULL(V.placa,'Não cadastrado!') placa,
		ISNULL(E.nomeEmpresa,'Não cadastrado!') nomeEmpresa,
		ISNULL(M.idMarca,0) idMarca,
		ISNULL(M.nomeModelo,'Não cadastrado!') nomeModelo,
		ISNULL(MAR.nomeMarca,'Não cadastrado!') nomeMarca,
		ISNULL (C.idCliente,0) idCliente,
		ISNULL (C.nome,'Não cadastrado!') nome,
		ISNULL (C.sobreNome, 'Não cadastrado!') sobreNome,
		ISNULL (cnh, 'Não cadastrado!') cnh
		FROM ALUGUEL A
LEFT JOIN VEICULO V
ON A.idVeiculo = V.idVeiculo
LEFT JOIN EMPRESA E
ON V.idEmpresa = E.idEmpresa
LEFT JOIN MODELO M
ON V.idModelo = M.idModelo
LEFT JOIN MARCA MAR
ON M.idMarca = MAR.idMarca
LEFT JOIN CLIENTE C
ON A.idCliente = C.idCliente
