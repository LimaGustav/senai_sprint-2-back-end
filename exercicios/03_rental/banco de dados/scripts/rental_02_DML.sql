USE RENTAL
GO

INSERT INTO EMPRESA (nomeEmpresa)
VALUES ('LocadoraCars'),('LocadoraAuto')
GO

INSERT INTO MARCA (nomeMarca)
VALUES ('Fiat'),('Volkswagen')
GO

INSERT INTO CLIENTE (nome,sobreNome,cnh)
VALUES ('Vera','Honorio','37885501250'),('Gustavo','Lima','24363255904'),('Amanda','Albuquerque','88952115583')
GO

INSERT INTO MODELO (idMarca, nomeModelo)
VALUES (1,'Uno'),(2,'Gol'),(2,'Golf')
GO

INSERT INTO VEICULO (idEmpresa,idModelo,placa)
VALUES (2,1,'JNP-4579'),(1,2,'EZP-4541')
GO

INSERT INTO ALUGUEL (idVeiculo,idCliente,dataRetirada, dataDevolucao)
VALUES	(1,2,'2021-01-02 12:35:00','2021-01-04 12:45:00'),
		(2,3,'2021-02-02 17:50:00','2021-02-05 19:50:00'),
		(1,3,'2021-03-02 18:37:36','2021-04-08 12:50:00')
GO
