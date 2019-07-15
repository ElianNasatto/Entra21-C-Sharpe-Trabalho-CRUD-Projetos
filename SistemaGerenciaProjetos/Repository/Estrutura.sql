
CREATE TABLE estados(
id INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(50),
sigla VARCHAR(2)
);

CREATE TABLE cidades(
id INT PRIMARY KEY IDENTITY(1,1),
id_estado INT, FOREIGN KEY(id_estado) REFERENCES estados(id),
nome VARCHAR(50),
numero_habitantes INT
);

CREATE TABLE clientes(
id INT PRIMARY KEY IDENTITY(1,1),
id_cidade INT, FOREIGN KEY(id_cidade) REFERENCES cidades(id),
nome VARCHAR(50),
cpf VARCHAR(50),
data_nascimento DATETIME2,
numero INT,
complemento NCHAR(10),
logradouro NCHAR(10),
cep NCHAR(10)
);


CREATE TABLE usuarios(
id INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(50),
login VARCHAR(50),
senha VARCHAR(50)
);

CREATE TABLE categorias(
id INT PRIMARY KEY IDENTITY(1,1),
nome VARCHAR(50)
);


CREATE TABLE projetos(
id INT PRIMARY KEY IDENTITY(1,1),
id_cliente INT, FOREIGN KEY(id_cliente) REFERENCES clientes(id),
nome VARCHAR(50),
data_criacao DATETIME2,
data_finalizacao DATETIME2
);

CREATE TABLE tarefas(
id INT PRIMARY KEY IDENTITY(1,1),
id_usuario_responsavel INT, FOREIGN KEY(id_usuario_responsavel) REFERENCES usuarios(id),
id_projeto INT, FOREIGN KEY(id_projeto) REFERENCES projetos(id),
id_categoria INT, FOREIGN KEY(id_categoria) REFERENCES categorias(id),
titulo VARCHAR(50),
descricao TEXT,
duracao DATETIME2
);




