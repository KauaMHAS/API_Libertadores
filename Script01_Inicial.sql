use master

go
drop DATABASE DB_PAMII_LUIZSOUZA_LIBERTADORES

go
CREATE DATABASE DB_PAMII_LUIZSOUZA_LIBERTADORES

go
use DB_PAMII_LUIZSOUZA_LIBERTADORES

go
CREATE TABLE TB_POSICOES
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(50) NOT NULL
);

CREATE TABLE TB_TIMES
(
    Id INT IDENTITY(1,1) CONSTRAINT [PK_TB_TIMES] PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Cidade VARCHAR(100) NOT NULL,
    Pais VARCHAR(50) NOT NULL,
    AnoFundacao INT NOT NULL,
    TitulosLibertadores INT NOT NULL,
    Escudo VARCHAR(255) NULL,
    EstadioId INT NULL
);

CREATE TABLE TB_JOGADORES
(
    Id INT IDENTITY(1,1) CONSTRAINT [PK_TB_JOGADORES] PRIMARY KEY,
    Nome VARCHAR(120) NOT NULL,
    Numero INT NOT NULL,
    DataNascimento DATE NOT NULL,
    TimeId INT NOT NULL,
    PosicaoId INT NOT NULL,
    CONSTRAINT FK_JOGADORES_TIMES
        FOREIGN KEY(TimeId)
        REFERENCES TB_TIMES(Id),

    CONSTRAINT FK_JOGADORES_POSICOES
        FOREIGN KEY(PosicaoId)
        REFERENCES TB_POSICOES(Id)
);

CREATE TABLE TB_ESTADIOS
(
    Id INT IDENTITY(1,1) CONSTRAINT [PK_TB_ESTADIOS] PRIMARY KEY,
    Nome VARCHAR(120) NOT NULL,
    Cidade VARCHAR(100) NOT NULL,
    Pais VARCHAR(50) NOT NULL,
    Capacidade INT NOT NULL
);

CREATE TABLE TB_RODADAS
(
    Id INT IDENTITY(1,1) CONSTRAINT [PK_TB_RODADAS] PRIMARY KEY,
    Nome VARCHAR(50) NOT NULL
);

CREATE TABLE TB_PARTIDAS
(
    Id INT IDENTITY(1,1) CONSTRAINT [PK_TB_PARTIDAS] PRIMARY KEY,
    DataHora DATETIME NOT NULL,
    EstadioId INT NOT NULL,
    RodadaId INT NOT NULL,
    CONSTRAINT FK_PARTIDAS_ESTADIOS
        FOREIGN KEY(EstadioId)
        REFERENCES TB_ESTADIOS(Id),
    CONSTRAINT FK_PARTIDAS_RODADAS
        FOREIGN KEY(RodadaId)
        REFERENCES TB_RODADAS(Id)
);

CREATE TABLE TB_PARTIDAS_TIMES
(
    PartidaId INT NOT NULL,
    TimeId INT NOT NULL,
    Gols INT NOT NULL,
    GolsDecisaoPenaltis INT NOT NULL CONSTRAINT DF_PARTIDAS_TIMES_GOLS_PENALTIS DEFAULT(0),
    Mandante BIT NOT NULL,
    Pontos INT NOT NULL,

    CONSTRAINT PK_PARTIDAS_TIMES
        PRIMARY KEY(PartidaId, TimeId),

    CONSTRAINT FK_PARTIDAS_TIMES_PARTIDAS
        FOREIGN KEY(PartidaId)
        REFERENCES TB_PARTIDAS(Id),

    CONSTRAINT FK_PARTIDAS_TIMES_TIMES
        FOREIGN KEY(TimeId)
        REFERENCES TB_TIMES(Id)
);


INSERT INTO TB_POSICOES (Nome)
VALUES
('Goleiro'),
('Zagueiro'),
('Lateral Direito'),
('Lateral Esquerdo'),
('Volante'),
('Meio-Campista'),
('Meia Atacante'),
('Ponta Direita'),
('Ponta Esquerda'),
('Centroavante');

INSERT INTO TB_RODADAS (Nome)
VALUES
('Fase de Grupos - Rodada 1'),
('Fase de Grupos - Rodada 2'),
('Fase de Grupos - Rodada 3'),
('Fase de Grupos - Rodada 4'),
('Fase de Grupos - Rodada 5'),
('Fase de Grupos - Rodada 6'),
('Oitavas de Final'),
('Quartas de Final'),
('Semifinal'),
('Final');

INSERT INTO TB_TIMES
(
    Nome,
    Cidade,
    Pais,
    AnoFundacao,
    TitulosLibertadores,
    Escudo
)
VALUES
-- BRASIL
('Corinthians','São Paulo','Brasil',1910,1,NULL),
('Cruzeiro','Belo Horizonte','Brasil',1921,2,NULL),
('Flamengo','Rio de Janeiro','Brasil',1895,4,NULL),
('Fluminense','Rio de Janeiro','Brasil',1902,1,NULL),
('Mirassol','Mirassol','Brasil',1925,0,NULL),
('Palmeiras','São Paulo','Brasil',1914,3,NULL),

-- ARGENTINA
('Boca Juniors','Buenos Aires','Argentina',1905,6,NULL),
('Estudiantes','La Plata','Argentina',1905,4,NULL),
('Independiente Rivadavia','Mendoza','Argentina',1913,0,NULL),
('Lanús','Lanús','Argentina',1915,0,NULL),
('Platense','Buenos Aires','Argentina',1905,0,NULL),
('Rosario Central','Rosário','Argentina',1889,1,NULL),

-- URUGUAI
('Nacional','Montevidéu','Uruguai',1899,3,NULL),
('Peñarol','Montevidéu','Uruguai',1891,5,NULL),

-- PARAGUAI
('Libertad','Assunção','Paraguai',1905,0,NULL),

-- EQUADOR
('Barcelona SC','Guayaquil','Equador',1925,0,NULL),
('Independiente del Valle','Sangolquí','Equador',1958,0,NULL),
('LDU Quito','Quito','Equador',1930,1,NULL),

-- CHILE
('Coquimbo Unido','Coquimbo','Chile',1958,0,NULL),
('Universidad Católica','Santiago','Chile',1937,0,NULL),

-- BOLÍVIA
('Bolívar','La Paz','Bolívia',1925,0,NULL),

-- PERU
('Cusco FC','Cusco','Peru',2009,0,NULL),
('Universitario','Lima','Peru',1924,0,NULL),

-- COLÔMBIA
('Deportes Tolima','Ibagué','Colômbia',1954,0,NULL),
('Independiente Medellín','Medellín','Colômbia',1913,0,NULL),

-- VENEZUELA
('Deportivo La Guaira','La Guaira','Venezuela',2013,0,NULL),

-- DEMAIS CLASSIFICADOS
('Atlético Nacional','Medellín','Colômbia',1947,2,NULL),
('Olimpia','Assunção','Paraguai',1902,3,NULL),
('Cerro Porteño','Assunção','Paraguai',1912,0,NULL),
('Universidad de Chile','Santiago','Chile',1927,0,NULL),
('Sporting Cristal','Lima','Peru',1955,0,NULL),
('The Strongest','La Paz','Bolívia',1908,0,NULL);



INSERT INTO TB_ESTADIOS
(
    Nome,
    Cidade,
    Pais,
    Capacidade
)
VALUES

-- BRASIL
('Neo Química Arena','São Paulo','Brasil',49205),
('Mineirão','Belo Horizonte','Brasil',61927),
('Maracanã','Rio de Janeiro','Brasil',78838),
('Allianz Parque','São Paulo','Brasil',43713),
('José Maria de Campos Maia','Mirassol','Brasil',15000),

-- ARGENTINA
('La Bombonera','Buenos Aires','Argentina',54000),
('Jorge Luis Hirschi','La Plata','Argentina',30500),
('Ciudad de Lanús','Lanús','Argentina',47000),
('Ciudad de Vicente López','Buenos Aires','Argentina',31000),
('Gigante de Arroyito','Rosário','Argentina',47000),

-- URUGUAI
('Gran Parque Central','Montevidéu','Uruguai',38000),
('Campeón del Siglo','Montevidéu','Uruguai',40000),

-- PARAGUAI
('La Huerta','Assunção','Paraguai',15000),
('General Pablo Rojas','Assunção','Paraguai',45000),

-- EQUADOR
('Monumental Isidro Romero Carbo','Guayaquil','Equador',59283),
('Banco Guayaquil','Sangolquí','Equador',12000),
('Rodrigo Paz Delgado','Quito','Equador',41575),

-- CHILE
('Francisco Sánchez Rumoroso','Coquimbo','Chile',18000),
('Claro Arena','Santiago','Chile',20000),

-- BOLÍVIA
('Hernando Siles','La Paz','Bolívia',41000),

-- PERU
('Monumental U','Lima','Peru',80093),
('Inca Garcilaso de la Vega','Cusco','Peru',42000),

-- COLÔMBIA
('Atanasio Girardot','Medellín','Colômbia',45943),

-- VENEZUELA
('Olímpico de la UCV','Caracas','Venezuela',24000);

INSERT INTO TB_JOGADORES
(
    Nome,
    Numero,
    DataNascimento,
    TimeId,
    PosicaoId
)
VALUES

-- ==========================
-- CORINTHIANS
-- ==========================
('Hugo Souza',1,'1999-01-31',1,1),
('Matheuzinho',2,'2000-09-08',1,3),
('André Ramalho',4,'1992-02-16',1,2),
('Matheus Bidu',21,'1999-05-08',1,4),
('Raniele',14,'1996-06-09',1,5),
('Rodrigo Garro',8,'1998-01-04',1,7),
('Memphis Depay',10,'1994-02-13',1,8),
('Yuri Alberto',9,'2001-03-18',1,10),

-- ==========================
-- CRUZEIRO
-- ==========================
('Cássio',1,'1987-06-06',2,1),
('William',12,'1995-04-02',2,3),
('Fabrício Bruno',15,'1996-02-12',2,2),
('Lucas Villalba',25,'1994-08-19',2,2),
('Kaiki',6,'2003-03-08',2,4),
('Lucas Romero',29,'1994-04-18',2,5),
('Lucas Silva',16,'1993-02-16',2,5),
('Matheus Pereira',10,'1996-05-05',2,7),
('Christian',88,'2000-12-19',2,6),
('Wanderson',94,'1994-10-07',2,8),
('Kaio Jorge',19,'2002-01-24',2,10),

-- ==========================
-- FLAMENGO
-- ==========================
('Agustín Rossi',1,'1995-08-21',3,1),
('Varela',2,'1993-03-24',3,3),
('Léo Ortiz',3,'1996-01-03',3,2),
('Léo Pereira',4,'1996-01-31',3,2),
('Alex Sandro',26,'1991-01-26',3,4),
('Erick Pulgar',5,'1994-01-15',3,5),
('Jorginho',21,'1991-12-20',3,5),
('Giorgian De Arrascaeta',10,'1994-06-01',3,7),
('Luiz Araújo',7,'1996-06-02',3,8),
('Bruno Henrique',27,'1990-12-30',3,9),
('Pedro',9,'1997-06-20',3,10),

-- ==========================
-- FLUMINENSE
-- ==========================
('Fábio',1,'1980-09-30',4,1),
('Samuel Xavier',2,'1990-06-06',4,3),
('Thiago Silva',3,'1984-09-22',4,2),
('Ignácio',4,'1996-12-01',4,2),
('Renê',6,'1992-09-14',4,4),
('Martinelli',8,'2001-04-05',4,5),
('Hércules',35,'2000-09-16',4,5),
('Paulo Henrique Ganso',10,'1989-10-12',4,7),
('Kevin Serna',90,'1997-12-31',4,9),
('Germán Cano',14,'1988-01-02',4,10),

-- ==========================
-- MIRASSOL
-- ==========================
('Walter',1,'1988-11-18',5,1),
('Lucas Ramon',2,'1994-04-10',5,3),
('João Victor',3,'1998-03-07',5,2),
('Luiz Otávio',4,'1992-09-09',5,2),
('Reinaldo',6,'1989-09-28',5,4),
('Neto Moura',25,'1996-08-12',5,5),
('Gabriel',10,'1999-03-18',5,7),

-- ==========================
-- PALMEIRAS
-- ==========================
('Weverton',21,'1987-12-13',6,1),
('Giay',4,'2004-01-16',6,3),
('Gustavo Gómez',15,'1993-05-06',6,2),
('Murilo',26,'1997-03-27',6,2),
('Piquerez',22,'1998-08-24',6,4),
('Emiliano Martínez',5,'1999-08-17',6,5),
('Felipe Anderson',7,'1993-04-15',6,8),
('Facundo Torres',17,'2000-04-13',6,9),
('Vitor Roque',9,'2005-02-28',6,10);

INSERT INTO TB_PARTIDAS
(
    DataHora,
    EstadioId,
    RodadaId
)
VALUES
('2026-04-07 18:00:00',24,1), -- Deportivo La Guaira x Fluminense
('2026-04-07 19:00:00',15,1), -- Barcelona SC x Cruzeiro
('2026-04-07 20:30:00',19,1), -- Universidad Católica x Boca Juniors
('2026-04-07 21:00:00',23,1), -- Deportes Tolima x Universitario

('2026-04-08 18:00:00',18,1), -- Coquimbo Unido x Nacional
('2026-04-08 19:00:00',5,1),  -- Mirassol x Lanús
('2026-04-08 19:00:00',23,1), -- Independiente Medellín x Estudiantes
('2026-04-08 19:30:00',22,1), -- Cusco FC x Flamengo
('2026-04-08 21:30:00',21,1), -- Junior x Palmeiras
('2026-04-08 21:00:00',21,1), -- Sporting Cristal x Cerro Porteño

('2026-04-09 18:00:00',24,1), -- UCV FC x Libertad
('2026-04-09 19:00:00',10,1), -- Rosario Central x Independiente del Valle
('2026-04-09 21:00:00',9,1),  -- Platense x Corinthians
('2026-04-09 21:00:00',23,1), -- Santa Fe x Peñarol

('2026-04-07 19:00:00',20,1), -- Independiente Rivadavia x Bolívar
('2026-04-07 20:00:00',20,1); -- Always Ready x LDU Quito

INSERT INTO TB_PARTIDAS_TIMES
(
    PartidaId,
    TimeId,
    Mandante,
    Gols,
    Pontos
)
VALUES

-- Partida 1
(1,26,1,1,1),
(1,4,0,1,1),

-- Partida 2
(2,16,1,2,3),
(2,2,0,1,0),

-- Partida 3
(3,20,1,0,0),
(3,7,0,2,3),

-- Partida 4
(4,24,1,1,1),
(4,23,0,1,1),

-- Partida 5
(5,19,1,0,0),
(5,13,0,1,3),

-- Partida 6
(6,5,1,2,3),
(6,10,0,1,0),

-- Partida 7
(7,25,1,2,3),
(7,8,0,0,0),

-- Partida 8
(8,22,1,1,0),
(8,3,0,3,3),

-- Partida 9
(9,6,1,2,3),
(9,27,0,0,0),

-- Partida 10
(10,31,1,2,1),
(10,29,0,2,1),

-- Partida 11
(11,15,1,1,3),
(11,28,0,0,0),

-- Partida 12
(12,12,1,1,1),
(12,17,0,1,1),

-- Partida 13
(13,11,1,0,0),
(13,1,0,2,3),

-- Partida 14
(14,14,1,2,3),
(14,30,0,1,0),

-- Partida 15
(15,9,1,1,1),
(15,21,0,1,1),

-- Partida 16
(16,18,1,3,3),
(16,32,0,1,0);


SELECT
    P.Id AS IdPartida,
    P.DataHora,
    E.Nome AS Estadio,
    E.Cidade,

    TM.Nome AS TimeMandante,
    PTM.Gols AS GolsMandante,
    PTM.GolsDecisaoPenaltis AS GolsDecisaoPenaltisMandante,

    TV.Nome AS TimeVisitante,
    PTV.Gols AS GolsVisitante,
    PTV.GolsDecisaoPenaltis AS GolsDecisaoPenaltisVisitante

FROM TB_PARTIDAS P
INNER JOIN TB_ESTADIOS E ON E.Id = P.EstadioId 
INNER JOIN TB_PARTIDAS_TIMES PTM ON PTM.PartidaId = P.Id AND PTM.Mandante = 1
INNER JOIN TB_TIMES TM ON TM.Id = PTM.TimeId
INNER JOIN TB_PARTIDAS_TIMES PTV ON PTV.PartidaId = P.Id AND PTV.Mandante = 0
INNER JOIN TB_TIMES TV ON TV.Id = PTV.TimeId
ORDER BY P.DataHora