CREATE TABLE produto(
  cod int NOT NULL IDENTITY,
  descricao varchar(100) NOT NULL,
  grupo varchar(20) NOT NULL,
  codBarra varchar(100) DEFAULT NULL,
  unidadeMedida varchar(20)NOT NULL,
  precoCusto decimal(11,3) NOT NULL,
  precoVenda decimal(11,3) NOT NULL,
  dataHoraCadastro datetime NOT NULL,
  ativo tinyint NOT NULL,
  CONSTRAINT PK_cod PRIMARY KEY (cod));


CREATE TABLE venda (
  cod INT NOT NULL IDENTITY,
  clienteDocumento varchar(18) DEFAULT NULL,
  clienteNome varchar(50) DEFAULT NULL,
  obs varchar(300) DEFAULT NULL,
  total decimal(11,2) NOT NULL,
  dataHora datetime NOT NULL,
  PRIMARY KEY (cod));
  alter table  venda drop column total

CREATE TABLE venda_produto(
  cod INT NOT NULL IDENTITY,
  codVenda INT NOT NULL,
  codProduto INT NOT NULL,
  precoVenda decimal(11,3) NOT NULL,
  quantidade decimal(11,2) NOT NULL,
  PRIMARY KEY (cod),
  CONSTRAINT fk_codProduto FOREIGN KEY (codProduto) REFERENCES produto (cod),
  CONSTRAINT fk_codVenda FOREIGN KEY (codVenda) REFERENCES venda (cod));

  alter table venda_produto add total decimal(11,2);

  CREATE TABLE CADASTRO
(CODCADASTRO INT IDENTITY,
NOME VARCHAR(40)NOT NULL,
SEXO VARCHAR(20)NOT NULL,
EMAIL VARCHAR(40)NOT NULL,
LOGIN VARCHAR(15)NOT NULL,
SENHA VARCHAR(20)NOT NULL,
CONSTRAINT PK_CODCADASTRO PRIMARY KEY(CODCADASTRO));

SELECT*FROM CADASTRO
SELECT*FROM venda_produto
SELECT*FROM venda
SELECT*FROM produto


--Realizar um select que retorne as informações dos produtos ativos:  cod,  descricao, nomeGrupo, precoCusto, precoVenda (produto_grupo e produto)
SELECT produto.cod, produto.descricao,produto.grupo, produto.precoCusto, produto.precoVenda, produto.ativo FROM produto WHERE produto.ativo='Ativado';

--Um select que retorne o faturamento bruto total de cada dia de vendas de um determinado período.Deve conter os campos: data, valor.Utilizar a tabela: venda. 
SELECT top 31 venda.cod, venda.dataHora, venda_produto.total from venda inner join venda_produto on venda.cod = venda_produto.cod

--Deve conter os campos: nomeProduto, qtdTotal (quantidade total vendida), custoTotal (custo total das vendas do produto),
-- valorTotal (valor total das vendas do produto). ii Utilizar as tabelas: produto, venda e venda_produto.  

 SELECT top 31 produto.descricao, venda_produto.quantidade, venda_produto.total, produto.precoCusto from produto inner join venda_produto on produto.cod = venda_produto.cod
 
 SELECT top 31 COUNT(*)'Quantidade total de vendas'FROM venda_produto
 SELECT top 31 SUM(total)AS'Venda Total'FROM venda_produto
 SELECT TOP 31 SUM(precoCusto)AS 'Preço de Custo Total'FROM produto


