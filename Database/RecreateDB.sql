/* Remove all tables from the database */
DROP TABLE Race;
DROP TABLE HairColor;
DROP TABLE EyeColor;
DROP TABLE SkinColor;

/* Create tables and relationships */
CREATE TABLE Race(
    rID int primary key not null,
    rName text not null,
    rMBHeight int not null,
    rFBHeight int not null,
    hcID int not null,
    ecID int not null,
    scID int not null,
    rStr int not null,
    rInt int not null,
    rDex int not null,
    rCon int not null,
    rWis int not null,
    rLuk int not null,
    rCha int not null
);

CREATE TABLE HairColor(
    hcID int primary key not null,
    hcColor1 text not null,
    hcColor2 text not null,
    hcColor3 text not null,
    hcColor4 text not null,
    hcColor5 text not null,
    hcColor6 text not null
);

CREATE TABLE EyeColor(
    ecID int primary key not null,
    ecColor1 text not null,
    ecColor2 text not null,
    ecColor3 text not null,
    ecColor4 text not null,
    ecColor5 text not null,
    ecColor6 text not null
);

CREATE TABLE SkinColor(
    scID int primary key not null,
    scColor1 text not null,
    scColor2 text not null,
    scColor3 text not null,
    scColor4 text not null,
    scColor5 text not null,
    scColor6 text not null
);

/* Insert Values */
/* rID,rName,rMBHeight,rFBHeight,hcID,ecID,scID,rStr,rinteger,rDex,rCon,rWis,rLuk,rWis */
INSERT INTO Race VALUES (1,'Human',50,47,0,0,0,0,0,0,0,0,0,0);
INSERT INTO Race VALUES (2,'Orc',60,60,0,0,1,1,0,0,0,0,0,0);
INSERT INTO Race VALUES (3,'Elf',47,51,1,1,0,0,1,0,0,0,0,0);
INSERT INTO Race VALUES (4,'Gnome',30,28,0,0,0,0,0,1,0,0,0,0);
INSERT INTO Race VALUES (5,'Dwarf',38,34,0,0,0,0,0,0,1,0,0,0);
INSERT INTO Race VALUES (6,'Dragonkin',72,62,2,0,2,0,0,0,0,1,0,0);
INSERT INTO Race VALUES (7,'Teifling',48,50,3,2,3,0,0,0,0,0,1,0);
INSERT INTO Race VALUES (8,'Halfling',35,36,0,0,0,0,0,0,0,0,0,1);
INSERT INTO Race VALUES (9,'Dead',0,0,1,1,1,-100,-100,-100,-100,-100,-100,-100);

//* hcID,hcColor1,hcColor2,hcColor3,hcColor4,hcColor5,hcColor6 */
INSERT INTO HairColor VALUES (0,'Red','Grey','Blonde','Auburn','Brown','Black');
INSERT INTO HairColor VALUES (1,'Green','Auburn','Brown','Blonde','Black','Grey');
INSERT INTO HairColor VALUES (2,'None','None','None','None','None','Mone');
INSERT INTO HairColor VALUES (3,'Green','Blue','Pink','Purple','Black','Brown');

/* ecID,ecColor1,ecColor2,ecColor3,ecColor4,ecColor5,ecColor6 */
INSERT INTO EyeColor VALUES (0,'Grey','Amber','Blue','Green','Hazel','Brown');
INSERT INTO EyeColor VALUES (1,'Red','Blue','Gold','Grey','Green','Hazel');
INSERT INTO EyeColor VALUES (2,'Red','Black','Gold','White','Silver','None');
INSERT INTO EyeColor VALUES (3,'None','None','None','None','None','Mone');

/* scID,scColor1,scColor2,scColor3,scColor4,scColor5,scColor6 */
INSERT INTO SkinColor VALUES (0,'Albino','Pale','White','Tan','Brown','Black');
INSERT INTO SkinColor VALUES (1,'Grey','Ashen','White','Green','Red','Brown');
INSERT INTO SkinColor VALUES (2,'Black','Silver','Blue','Green','Gold','Bronze');
INSERT INTO SkinColor VALUES (3,'Purple','Red','Brick','Tan','Brown','Black');
INSERT INTO SkinColor VALUES (4,'None','None','None','None','None','Mone');