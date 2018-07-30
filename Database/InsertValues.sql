/* rID,rName,rMBHeight,rFBHeight,hcID,ecID,scID,rStr,rInt,rDex,rCon,rWis,rLuk,rWis */
INSERT INTO Race VALUES (1,'Human',50,47,1,1,1,0,0,0,0,0,0,0);
INSERT INTO Race VALUES (2,'Orc',60,60,1,1,1,1,0,0,0,0,0,0);
INSERT INTO Race VALUES (3,'Elf',47,51,1,1,1,0,1,0,0,0,0,0);
INSERT INTO Race VALUES (4,'Gnome',30,28,1,1,1,0,0,1,0,0,0,0);
INSERT INTO Race VALUES (5,'Dwarf',38,34,1,1,1,0,0,0,1,0,0,0);
INSERT INTO Race VALUES (6,'Dragonkin',72,62,1,1,1,0,0,0,0,1,0,0);
INSERT INTO Race VALUES (7,'Teifling',48,50,1,1,1,0,0,0,0,0,1,0);
INSERT INTO Race VALUES (8,'Halfling',35,36,1,1,1,0,0,0,0,0,0,1);
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