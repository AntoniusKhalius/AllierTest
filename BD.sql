-- Cr�ation des tables
DROP TABLE IF EXISTS Reponses;
DROP TABLE IF EXISTS Questions;
DROP TABLE IF EXISTS Questionnaire;

CREATE TABLE Questionnaire
(
	cle VARCHAR(12),
	name VARCHAR(40) NOT NULL,
	displayName VARCHAR(60) NOT NULL,
	description VARCHAR(124) NOT NULL,
	CONSTRAINT pk_questionnaire PRIMARY KEY(cle)
) ENGINE=INNODB;

CREATE TABLE Questions
(
	cle VARCHAR(12),
	rang VARCHAR(6),
	typeQ VARCHAR(5),
	name VARCHAR(20),
	text VARCHAR(60),
	reponse1 VARCHAR(31),
	reponse2 VARCHAR(31),
	reponse3 VARCHAR(31),
	reponse4 VARCHAR(31),
	reponse5 VARCHAR(31),
	defaut VARCHAR(1),
	CONSTRAINT pk_questions PRIMARY KEY(cle,rang),
	CONSTRAINT fk_questions_questionnaire FOREIGN KEY(cle) REFERENCES Questionnaire(cle) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=INNODB;

-- Table REPONSES � ECRIRE
CREATE TABLE Reponses
(
	id VARCHAR(6),
	cle_questionnaire VARCHAR(12),
	utilisateur VARCHAR(40),
	rang VARCHAR(6),
	date_creation DATETIME,
	reponse VARCHAR(80),
	CONSTRAINT pk_reponses PRIMARY KEY(id),
	CONSTRAINT fk_reponses_questionnaire FOREIGN KEY(cle_questionnaire) REFERENCES Questionnaire(cle) ON DELETE CASCADE ON UPDATE CASCADE,
	CONStraint fk_reponses_questions FOREIGN KEY(rang) REFERENCES Questions(rang) ON ON DELETE CASCADE ON ON UPDATE CASCADE
) ENGINE=INNODB;

DESC Questionnaire;
DESC Questions;
DESC Reponses;


